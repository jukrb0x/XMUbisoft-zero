// using System.Collections;
// using System.Collections.Generic;
// //using System.Xml.Serialization;
// using UnityEngine;
//
// [CreateAssetMenu(menuName = "AI/Decisions/Can See Player", fileName = "DecisionCanSeePlayer")]
// public class DecisionCanSeePlayer : AIDecision
// {
//     public LayerMask obstacleMask;
//     
//     private float viewDistance;
//     private float viewAngle;
//     
//     public override bool Decide(StateController controller)
//     {
//         EvaluateWeaponDirection(controller);
//         return CanSeePlayer(controller);
//     }
//
//     private bool CanSeePlayer(StateController controller)
//     {
//         if (controller.FieldOfView != null)
//         {
//             // Get view Distance
//             viewDistance = controller.FieldOfView.pointLightInnerRadius;
//         
//             // Get the angle
//             viewAngle = controller.FieldOfView.pointLightInnerAngle;
//         }
//         
//         // Check the distance to the player
//         float distanceToPlayer = (controller.Player.position - controller.transform.position).sqrMagnitude;
//         if (distanceToPlayer < Mathf.Pow(viewDistance, 2))
//         {
//             // Check if the player is inside the view angle
//             Vector2 directionToPlayer = (controller.Player.position - controller.transform.position).normalized;
//             Vector2 faceDirection = controller.CharacterFlip.FacingRight ? Vector2.right : Vector2.left;
//             float angleBetweenEnemyAndPlayer = Vector2.Angle(faceDirection, directionToPlayer);
//
//             if (angleBetweenEnemyAndPlayer < viewAngle / 2f)
//             {
//                 if (!Physics2D.Linecast(controller.transform.position, controller.Player.position, obstacleMask))
//                 {
//                     controller.Target = controller.Player;
//                     return true;
//                 }
//             }
//         }
//
//         controller.Target = null;
//         return false;
//     }
//
//     private void EvaluateWeaponDirection(StateController controller)
//     {
//         if (controller.Target == null)
//         {
//             if (controller.CharacterWeapon.CurrentWeapon.WeaponOwner.GetComponent<CharacterFlip>().FacingRight)
//             {
//                 controller.CharacterWeapon.CurrentWeapon.WeaponAim.SetAim(Vector2.right);
//             }
//             else
//             {
//                 controller.CharacterWeapon.CurrentWeapon.WeaponAim.SetAim(Vector2.left);
//             }
//         }
//     }
// } 