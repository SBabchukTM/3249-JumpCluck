using Cysharp.Threading.Tasks;
using Game.UI;

namespace States
{
    public class ShopState : StateController
    {
        public ShopState(GameUIContainer uiContainer) : base(uiContainer)
        {
            
        }

        public override async UniTask EnterState()
        {
            var screen = await UiContainer.CreateScreen<ShopScreen>();
            screen.OnBackPressed += async () => await StateMachine.EnterState<IncubationStateController>();
        }

        public override async UniTask ExitState()
        {
            await UiContainer.HideScreen<ShopScreen>();
        }
    }
}