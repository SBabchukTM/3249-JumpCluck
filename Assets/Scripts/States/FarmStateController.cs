using Cysharp.Threading.Tasks;

namespace Game.UI
{
    public class FarmStateController : StateController
    {
        public FarmStateController(GameUIContainer uiContainer) : base(uiContainer)
        {
            
        }

        public override async UniTask EnterState()
        {
            var screen = await UiContainer.CreateScreen<FarmScreen>();
            screen.OnBackButtonPressed += async () => await StateMachine.EnterState<IncubationStateController>();
        }

        public override async UniTask ExitState()
        {
            await UiContainer.HideScreen<FarmScreen>();
        }
    }
}