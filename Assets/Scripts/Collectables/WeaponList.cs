using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/Weapon_list", fileName = "Weapon_list")]
public class WeaponList : ScriptableObject
{
    public List<ItemData> list = new List<ItemData>();
}
