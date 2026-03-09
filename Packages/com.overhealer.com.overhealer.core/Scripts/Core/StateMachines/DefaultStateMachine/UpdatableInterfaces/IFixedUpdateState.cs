namespace overhealer.Core
{
    public interface IFixedUpdateState :
        IExitableState
    {
        void FixedUpdate();
    }
}