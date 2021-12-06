using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    WeaponList _weaponList;
    void Start()
    {
        _weaponList = Resources.Load<WeaponList>(typeof(WeaponList).Name);
        Debug.Log(_weaponList);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
