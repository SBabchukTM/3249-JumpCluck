using System.Collections;
using System.Collections.Generic;
using Shop;
using UnityEngine;

[CreateAssetMenu(fileName = "FridgesConfig", menuName = "Configs/FridgesConfig")]
public class FridgesConfig : ScriptableObject
{
    public List<SkinItem> Items = new List<SkinItem>();
}


