public abstract class AbstractStateBlock
{
    protected abstract void AddListeners();
    protected abstract void RemoveListeners();
    protected abstract void OnEnterState();
    protected abstract void OnExitState();
    //protected abstract void OnUpdate();
}