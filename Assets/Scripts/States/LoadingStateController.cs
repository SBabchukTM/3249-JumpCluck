using Cysharp.Threading.Tasks;
using Game.UI;
using Game.UserData;
using States;

public class LoadingStateController : StateController
{
    private readonly SaveSystem _saveSystem;
    public LoadingStateController(GameUIContainer uiContainer, SaveSystem saveSystem) : base(uiContainer)
    {
        _saveSystem = saveSystem;
    }

    public override async UniTask EnterState()
    {
        var screen = await UiContainer.CreateScreen<LoadingScreen>();
        await screen.Load();
        
        if(!_saveSystem.GetData().TutorialData.FinishedTutorial)
            await StateMachine.EnterState<OnboardingStateController>();
        else
            await StateMachine.EnterState<IncubationStateController>();
    }

    public override async UniTask ExitState()
    {
        await UiContainer.HideScreen<LoadingScreen>();
    }
}
