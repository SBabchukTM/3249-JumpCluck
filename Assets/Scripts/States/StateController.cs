using Cysharp.Threading.Tasks;
using Game.UI;

public class StateController
{
    protected StateMachine StateMachine;

    protected readonly GameUIContainer UiContainer;

    public StateController(GameUIContainer uiContainer)
    {
        UiContainer = uiContainer;
    }
    
    public void SetMachine(StateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }
    
    public virtual async UniTask EnterState()
    {

    }

    public virtual async UniTask ExitState()
    {
        
    }
}
