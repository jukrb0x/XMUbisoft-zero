using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "AI/Actions/Wander", fileName = "ActionWander")]
public class ActionWander : AIAction
{
    [Header("Wander Settings")]
    public float wanderArea = 3f;
    public float wanderTime = 2f;

    [Header("Obstacle Settings")]
    public Vector2 obstacleBoxCheckSize = new Vector2(2,2);
    public LayerMask obstacleMask;

    private Vector2 wanderDirection;
    private float wanderCheckTime;

    public override void Act(StateController controller)
    {
        EvaluateObstacles(controller);
        Wander(controller);
    }

    private void Wander(StateController controller)
    {
        if (Time.time > wanderCheckTime)
        {
            // Pick random location
            wanderDirection.x = Random.Range(-wanderArea, wanderArea);
            wanderDirection.y = Random.Range(-wanderArea, wanderArea);
            
            // Move our enemy
            controller.CharacterMovement.SetHorizontal(wanderDirection.x);
            controller.CharacterMovement.SetVertical(wanderDirection.y);

            // Update wander time
            wanderCheckTime = Time.time + wanderTime;
        }
    }

    private void EvaluateObstacles(StateController controller)
    {
        RaycastHit2D hit = Physics2D.BoxCast(controller.Collider2D.bounds.center, obstacleBoxCheckSize, 0f,
            wanderDirection, wanderDirection.magnitude, obstacleMask);

        if (hit)
        {
            // Pick random location
            wanderDirection.x = Random.Range(-wanderArea, wanderArea);
            wanderDirection.y = Random.Range(-wanderArea, wanderArea);

            // Update wander time
            wanderCheckTime = Time.time;
        }
    }
    
    private void OnEnable()  //We need to use wanderCheckTime everytime we use Scriptable Object
    {
        wanderCheckTime = 0;
    }
}