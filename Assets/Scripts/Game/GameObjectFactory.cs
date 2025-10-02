using UnityEngine;
using Zenject;

namespace Game
{
    public class GameObjectFactory
    {
        private readonly DiContainer _container;

        public GameObjectFactory(DiContainer container)
        {
            _container = container;
        }
        
        public T Create<T>(GameObject prefab) => _container.InstantiatePrefabForComponent<T>(prefab);
    }
}