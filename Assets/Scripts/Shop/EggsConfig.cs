using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(fileName = "EggsConfig", menuName = "Configs/EggsConfig")]
    public class EggsConfig : ScriptableObject
    {
        public List<EggItem> Items;
    }

    [Serializable]
    public class EggItem
    {
        public Sprite EggSprite;
        public Sprite CrackedEggSprite;
        public Sprite ChickenSprite;
        public int Price;
    }
}