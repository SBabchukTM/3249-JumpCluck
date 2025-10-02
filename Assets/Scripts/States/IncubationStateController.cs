using Cysharp.Threading.Tasks;
using States;

namespace Game.UI
{
    public class IncubationStateController : StateController
    {
        public IncubationStateController(GameUIContainer uiContainer) : base(uiContainer)
        {
        }

        public override async UniTask EnterState()
        {
            var screen = await UiContainer.CreateScreen<IncubationScreen>();
            screen.OnOnboardingPressed += async () => await StateMachine.EnterState<OnboardingStateController>();
            screen.OnFarmPressed += async () => await StateMachine.EnterState<FarmStateController>();
            screen.OnShopPressed += async () => await StateMachine.EnterState<ShopState>();
        }

        public override async UniTask ExitState()
        {
            await UiContainer.HideScreen<IncubationScreen>();
        }
    }
}