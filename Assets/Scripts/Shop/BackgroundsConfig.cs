using System.Collections.Generic;
using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(fileName = "BackgroundsConfig", menuName = "Configs/BackgroundsConfig")]
    public class BackgroundsConfig : ScriptableObject
    {
        public List<SkinItem> Items = new List<SkinItem>();
    }
}