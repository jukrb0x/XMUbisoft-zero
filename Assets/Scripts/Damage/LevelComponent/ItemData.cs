using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon", fileName = "Item Weapon")]
public class ItemData : ScriptableObject
{
    public Weapon WeaponToEquip;
    public Sprite WeaponSprite;
}
