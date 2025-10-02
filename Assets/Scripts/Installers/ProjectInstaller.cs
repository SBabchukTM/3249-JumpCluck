using Game;
using Game.Audio;
using Game.UI;
using Game.UserData;
using Habits;
using Shop;
using States;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private GameUIContainer _gameUIContainer;
        [SerializeField] private AudioPlayer _audioPlayer;
        
        public override void InstallBindings()
        {
            Container.Bind<GameUIContainer>().FromComponentInNewPrefab(_gameUIContainer).AsSingle();
            Container.Bind<AudioPlayer>().FromComponentInNewPrefab(_audioPlayer).AsSingle();

            Container.Bind<SaveSystem>().AsSingle();
            
            BindStates();
            BindServices();
            BindStateListener();
        }

        private void BindStates()
        {
            Container.Bind<StateMachine>().AsSingle();
            Container.Bind<StateController>().To<LoadingStateController>().AsSingle();
            Container.Bind<StateController>().To<OnboardingStateController>().AsSingle();
            Container.Bind<StateController>().To<IncubationStateController>().AsSingle();
            Container.Bind<StateController>().To<FarmStateController>().AsSingle();
            Container.Bind<StateController>().To<ShopState>().AsSingle();
            Container.BindInterfacesAndSelfTo<BootstrapController>().AsSingle();
            Container.Bind<InventoryService>().AsSingle();
            Container.Bind<HabitsService>().AsSingle();
        }

        private void BindServices()
        {
            Container.Bind<GameObjectFactory>().AsSingle();
        }

        private void BindStateListener()
        {
            Container.InstantiateComponentOnNewGameObject<ApplicationStateListener>("ApplicationStateListener");
        }
    }
}
