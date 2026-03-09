namespace overhealer.Core
{
    public interface ILateUpdateState :
        IExitableState
    {
        void LateUpdate();
    }
}