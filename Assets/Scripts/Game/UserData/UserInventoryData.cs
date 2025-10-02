using System;
using System.Collections.Generic;

namespace Game.UserData
{
    [Serializable]
    public class UserInventoryData
    {
        public int Balance = 0;
        
        public List<int> PurchasedEggSkins = new()
        {
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9
        };
        
        public List<int> PurchasedFridgeSkins = new()
        {
            0
        };
        
        public int UsedFridgeSkin = 0;

        public List<int> PurchasedBackgroundSkins = new()
        {
            0,
        };
        
        public int UsedBackgroundSkin = 0;
    }
}