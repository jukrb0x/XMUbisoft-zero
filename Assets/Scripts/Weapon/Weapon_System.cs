using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_System : MonoBehaviour
{
    Weaponlist weaponlist;
    void Start()
    {
        weaponlist = Resources.Load<Weaponlist>(typeof(Weaponlist).Name);
        Debug.Log(weaponlist);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
