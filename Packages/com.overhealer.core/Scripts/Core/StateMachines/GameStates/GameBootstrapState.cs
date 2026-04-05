using System;
using System.Reflection;
using UnityEngine;

namespace overhealer.Core
{
    public class GameBootstrapState : State, IState
    {
        private UpdateStateMachine gameStateMachine;
        private UIService uiService;
        private Action afterBootstrapAction;

        public GameBootstrapState(UIService ui, UpdateStateMachine stateMachine, Action afterBootstrap)
        {
            gameStateMachine = stateMachine;
            uiService = ui;
            afterBootstrapAction = afterBootstrap;
        }

        public void Enter()
        {
            CreateServices(uiService);
            Debug.Log("Loading game scene...");
            LevelPayload payload = new LevelPayload("Game", afterBootstrapAction);
            gameStateMachine.Enter<LoadLevelState, LevelPayload>(payload);
        }

        public void Exit()
        {
        }

        public void CreateServices(UIService uiService)
        {
            Debug.Log("Create services...");
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.GetCustomAttribute<ServiceAttribute>(true) != null)
                    {
                        ServiceLocator.Instance.Add(type, (IService)Activator.CreateInstance(type));
                    }
                }
            }

            foreach (var service in ServiceLocator.Instance.Services.Values)
            {
                if (service is IInitialisable)
                {
                    (service as IInitialisable).Init();
                }

                if (service is IUpdatable)
                {
                    GameInstance.RegisterUpdatable(service as IUpdatable);
                }

                if (service is ILateUpdatable)
                {
                    GameInstance.RegisterLateUpdatable(service as ILateUpdatable);
                }

                if (service is IFixedUpdatable)
                {
                    GameInstance.RegisterFixedUpdatable(service as IFixedUpdatable);
                }
            }

            uiService.InitUI();
        }
    }
}