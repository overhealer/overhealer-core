namespace overhealer.Core
{
    public interface IState :
        IExitableState
    {
        void Enter();
    }
}