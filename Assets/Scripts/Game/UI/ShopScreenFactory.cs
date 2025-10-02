using System.Collections.Generic;
using Shop;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class ShopScreenFactory : MonoBehaviour
    {
        [SerializeField] private FridgesConfig _fridgesConfig;
        [SerializeField] private BackgroundsConfig _backgroundsConfig;
        [SerializeField] private EggsConfig _eggsConfig;

        [SerializeField] private GameObject _skinItemPrefab;
        [SerializeField] private GameObject _eggItemPrefab;
        
        [Inject]
        private GameObjectFactory _factory;
        
        [Inject]
        private InventoryService _inventoryService;

        public List<ShopEggDisplay> GetEggItems()
        {
            List<ShopEggDisplay> result = new List<ShopEggDisplay>();

            var purchasedEggs = _inventoryService.GetPurchasedEggs();
            
            for (int i = 0; i < _eggsConfig.Items.Count; i++)
            {
                if(purchasedEggs.Contains(i))
                    continue;

                var display = _factory.Create<ShopEggDisplay>(_eggItemPrefab);
                display.Initialize(_eggsConfig.Items[i], i);
                result.Add(display);
            }
            
            return result;
        }
        
        public List<ShopSkinDisplay> GetFridgeItems()
        {
            List<ShopSkinDisplay> result = new List<ShopSkinDisplay>();

            for (int i = 0; i < _fridgesConfig.Items.Count; i++)
            {
                var display = _factory.Create<ShopSkinDisplay>(_skinItemPrefab);
                display.Initialize(_fridgesConfig.Items[i], i, GetFridgeItemStatus(i));
                result.Add(display);
            }
            
            return result;
        }
        
        public List<ShopSkinDisplay> GetBackgroundItems()
        {
            List<ShopSkinDisplay> result = new List<ShopSkinDisplay>();

            for (int i = 0; i < _backgroundsConfig.Items.Count; i++)
            {
                var display = _factory.Create<ShopSkinDisplay>(_skinItemPrefab);
                display.Initialize(_backgroundsConfig.Items[i], i, GetBackgroundItemStatus(i));
                result.Add(display);
            }
            
            return result;
        }

        public void UpdateFridgeStatuses(List<ShopSkinDisplay> fridgeItems)
        {
            for(int i = 0; i < fridgeItems.Count; i++)
                fridgeItems[i].SetStatus(GetFridgeItemStatus(i));
        }

        public void UpdateBackgroundStatuses(List<ShopSkinDisplay> backgroundItems)
        {
            for(int i = 0; i < backgroundItems.Count; i++)
                backgroundItems[i].SetStatus(GetBackgroundItemStatus(i));
        }
        
        private SkinStatus GetFridgeItemStatus(int id)
        {
            if (id == _inventoryService.GetUsedFridgeId())
                return SkinStatus.Used;
            
            if(_inventoryService.GetPurchasedFridges().Contains(id))
                return SkinStatus.Purchased;
            
            return SkinStatus.NotPurchased;
        }
        
        private SkinStatus GetBackgroundItemStatus(int id)
        {
            if (id == _inventoryService.GetUsedBackgroundId())
                return SkinStatus.Used;
            
            if(_inventoryService.GetPurchasedBackgrounds().Contains(id))
                return SkinStatus.Purchased;
            
            return SkinStatus.NotPurchased;
        }
    }
}