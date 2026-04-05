using System;
using System.Collections.Generic;
using UnityEngine;

namespace overhealer.Core
{
    public abstract class GameInstance
    {
        public static Action<GameObject> OnObjectCreate;
        public static Action<GameObject> OnObjectDelete;
        public static Action<IUpdatable> OnUpdatableCreate;
        public static Action<ILateUpdatable> OnLateUpdatableCreate;
        public static Action<IFixedUpdatable> OnFixedUpdatableCreate;

        protected UpdateStateMachine gameStateMachine;

        protected List<IUpdatable> updatables = new List<IUpdatable>();
        protected List<ILateUpdatable> lateUpdatables = new List<ILateUpdatable>();
        protected List<IFixedUpdatable> fixedUpdatables = new List<IFixedUpdatable>();

        public GameInstance()
        {
            OnObjectCreate = (newObject) =>
            {
                IInitialisable[] inits = newObject.GetComponentsInChildren<IInitialisable>();
                for (int i = 0; i < inits.Length; i++)
                {
                    inits[i].Init();
                }

                IUpdatable[] updatables = newObject.GetComponentsInChildren<IUpdatable>();
                for (int i = 0; i < updatables.Length; i++)
                {
                    this.updatables.Add(updatables[i]);
                }

                ILateUpdatable[] lateUpdatables = newObject.GetComponentsInChildren<ILateUpdatable>();
                for (int i = 0; i < lateUpdatables.Length; i++)
                {
                    this.lateUpdatables.Add(lateUpdatables[i]);
                }

                IFixedUpdatable[] fixedUpdatables = newObject.GetComponentsInChildren<IFixedUpdatable>();
                for (int i = 0; i < fixedUpdatables.Length; i++)
                {
                    this.fixedUpdatables.Add(fixedUpdatables[i]);
                }
            };

            OnObjectDelete = (objectToDelete) =>
            {
                IUpdatable[] updatables = objectToDelete.GetComponentsInChildren<IUpdatable>();
                for (int i = 0; i < updatables.Length; i++)
                {
                    this.updatables.Remove(updatables[i]);
                }

                ILateUpdatable[] lateUpdatables = objectToDelete.GetComponentsInChildren<ILateUpdatable>();
                for (int i = 0; i < lateUpdatables.Length; i++)
                {
                    this.lateUpdatables.Remove(lateUpdatables[i]);
                }

                IFixedUpdatable[] fixedUpdatables = objectToDelete.GetComponentsInChildren<IFixedUpdatable>();
                for (int i = 0; i < fixedUpdatables.Length; i++)
                {
                    this.fixedUpdatables.Remove(fixedUpdatables[i]);
                }
            };

            OnUpdatableCreate = (updatable) =>
            {
                updatables.Add(updatable);
            };

            OnLateUpdatableCreate = (lateUpdatable) =>
            {
                lateUpdatables.Add(lateUpdatable);
            };

            OnFixedUpdatableCreate = (fixedUpdatable) =>
            {
                fixedUpdatables.Add(fixedUpdatable);
            };
        }

        public abstract void StartStateMachine();

        public void OnUpdate()
        {
            foreach (var updatable in updatables)
            {
                updatable.OnUpdate();
            }

            gameStateMachine.UpdateState();
        }

        public void OnLateUpdate()
        {
            foreach (var lateUpdatable in lateUpdatables)
            {
                lateUpdatable.OnLateUpdate();
            }

            gameStateMachine.LateUpdateState();
        }

        public void OnFixedUpdate()
        {
            foreach (var fixedUpdatable in fixedUpdatables)
            {
                fixedUpdatable.OnFixedUpdate();
            }

            gameStateMachine.FixedUpdateState();
        }

        public static GameObject CreateObject(GameObject prefab, Vector3 pos, Vector3 rot, Transform parent = null)
        {
            GameObject newObject = UnityEngine.Object.Instantiate(prefab, pos, Quaternion.Euler(rot), parent);

            RegisterObject(newObject);

            return newObject;
        }

        public static void RegisterObject(GameObject newObject)
        {
            OnObjectCreate?.Invoke(newObject);
        }

        public static void UnregisterObject(GameObject objectToDelete)
        {
            OnObjectDelete?.Invoke(objectToDelete);
        }

        public static void RegisterUpdatable(IUpdatable updatable)
        {
            OnUpdatableCreate?.Invoke(updatable);
        }

        public static void RegisterLateUpdatable(ILateUpdatable updatable)
        {
            OnLateUpdatableCreate?.Invoke(updatable);
        }

        public static void RegisterFixedUpdatable(IFixedUpdatable updatable)
        {
            OnFixedUpdatableCreate?.Invoke(updatable);
        }

        public static void DestoyObject(GameObject objectToDelete)
        {
            UnregisterObject(objectToDelete);

            UnityEngine.Object.Destroy(objectToDelete);
        }
    }
}