using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShopItem : ScriptableObject
{
    public Sprite shopImage;
    public List<InventorySlot> cost;
    public bool bought = false;
    public int lightlifeBuff;
    public int pickaxeBuff;
    public string text;
    
}
