namespace overhealer.Core
{
    public class LoadLevelState :
            State,
            IPayloadState<LevelPayload>
    {
        private UpdateStateMachine gameStateMachine;

        public LoadLevelState(UpdateStateMachine stateMachine)
        {
            gameStateMachine = stateMachine;
        }

        public void Enter(LevelPayload payload)
        {
            var loadService = ServiceLocator.Instance.Get<SceneLoadService>();

            loadService.LoadScene(payload);
        }

        public void Exit()
        {
        }
    }
}