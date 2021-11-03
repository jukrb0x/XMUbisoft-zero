using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : AnimatableMovable2D
{
    private NPC npc;


    protected override void Awake()
    {
        base.Awake();
        npc = GetComponent<NPC>();
    }

    private void Start()
    {
        
    }
    
}
