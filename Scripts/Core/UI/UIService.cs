using System;
using System.Collections.Generic;
using UnityEngine;

namespace overhealer.Core
{
    public class UIService :
            MonoBehaviour,
            IService
    {
        public Type State
        {
            get => currentState.GetType();
        }

        [SerializeField]
        private List<UIState> statePrefabs;

        [SerializeField]
        private Canvas mainCanvas;

        private UIState currentState;
        private Dictionary<Type, UIState> statesDictionary = new Dictionary<Type, UIState>();

        public void InitUI()
        {
            ServiceLocator.Instance.Add(typeof(UIService), this);

            foreach (var prefab in statePrefabs)
            {
                statesDictionary.Add(prefab.GetType(), prefab);
            }
        }

        public void EnableState(Type state)
        {
            if (currentState != null)
            {
                currentState.Disable();
                GameInstance.DestoyObject(currentState.gameObject);
            }

            var newState = GameInstance.CreateObject(GetStatePrefab(state).gameObject, Vector3.zero, Vector3.zero);
            DontDestroyOnLoad(newState);
            newState.transform.SetParent(mainCanvas.gameObject.transform, false);
            currentState = newState.GetComponent<UIState>();

            currentState.Enable();
        }

        private UIState GetStatePrefab(Type state)
        {
            return statesDictionary[state];
        }
    }
}