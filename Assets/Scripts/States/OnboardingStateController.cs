using Cysharp.Threading.Tasks;
using Game.UI;
using Game.UserData;

namespace States
{
    public class OnboardingStateController : StateController
    {
        private readonly SaveSystem _saveSystem;
        public OnboardingStateController(GameUIContainer uiContainer, SaveSystem saveSystem) : base(uiContainer)
        {
            _saveSystem = saveSystem;
        }

        public override async UniTask EnterState()
        {
            var screen = await UiContainer.CreateScreen<OnboardingScreen>();
            screen.OnTutorialFinished += async () =>
            {
                _saveSystem.GetData().TutorialData.FinishedTutorial = true;
                await StateMachine.EnterState<IncubationStateController>();
            };
        }

        public override async UniTask ExitState()
        {
            await UiContainer.HideScreen<OnboardingScreen>();
        }
    }
}