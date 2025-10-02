using System;
using System.Collections.Generic;
using Game.UserData;

namespace Shop
{
    public class InventoryService
    {
        private readonly SaveSystem _saveSystem;
        
        public event Action<int> OnBalanceChanged;

        public InventoryService(SaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
        }

        public int GetBalance() => GetData().Balance;

        public void AddBalance(int amount)
        {
            GetData().Balance += amount;
            OnBalanceChanged?.Invoke(GetBalance());
        }

        public void AddPurchasedFridge(int id)
        {
            GetData().PurchasedFridgeSkins.Add(id);
            GetData().UsedFridgeSkin = id;
        }
        
        public void SetUsedFridgeSkin(int id) => GetData().UsedFridgeSkin = id;

        public void AddPurchasedBackground(int id)
        {
            GetData().PurchasedBackgroundSkins.Add(id);
            GetData().UsedBackgroundSkin = id;
        }
        
        public void SetUsedBackgroundSkin(int id) => GetData().UsedBackgroundSkin = id;

        public void AddPurchasedEgg(int id) => GetData().PurchasedEggSkins.Add(id);

        public int GetUsedFridgeId() => GetData().UsedFridgeSkin;
        public int GetUsedBackgroundId() => GetData().UsedBackgroundSkin;
        
        public List<int> GetPurchasedFridges() => GetData().PurchasedFridgeSkins;
        public List<int> GetPurchasedBackgrounds() => GetData().PurchasedBackgroundSkins;
        public List<int> GetPurchasedEggs() => GetData().PurchasedEggSkins;
        
        private UserInventoryData GetData() => _saveSystem.GetData().UserInventoryData;
    }
}