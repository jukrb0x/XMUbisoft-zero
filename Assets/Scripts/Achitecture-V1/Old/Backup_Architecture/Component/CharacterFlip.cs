using UnityEngine;

namespace Component
{
    public class CharacterFlip : CharacterComponents
    {
        private enum FlipMode
        {
            MovementDirection,
            WeaponDirection
        }

        [SerializeField] private FlipMode flipMode = FlipMode.MovementDirection;
        [SerializeField] private float threshold = 0.1f;

        protected override void HandleAbility()
        {
            base.HandleAbility();

            if (flipMode == FlipMode.MovementDirection)
            {
                FlipToMoveDirection();
            }
            else
            {
                FlipToWeaponDirection();
            }
        }

        //Flips our character by the direction we are moving
        private void FlipToMoveDirection()
        {
            if (!(controller.CurrentMovement.normalized.magnitude > threshold)) return;
            if (controller.CurrentMovement.normalized.x > 0)
            {
                FaceDirection(1);
            }
            else if (controller.CurrentMovement.normalized.x < 0)
            {
                FaceDirection(-1);
            }
            else
            {
                FaceDirection((int) transform.localScale.x); //For Vertical movement
            }
        }

        //Flips our character by our Weapon Aiming
        private void FlipToWeaponDirection()
        {
        }

        //Make our character face the direction in which is moving
        private void FaceDirection(int newDirection)
        {
            //Get player size
            float OriginalX = System.Math.Abs(transform.localScale.x);
            float OriginalY = System.Math.Abs(transform.localScale.y);

            if (newDirection > 0)
            {
                transform.localScale = new Vector3(OriginalX, OriginalY, 1);
            }
            else
            {
                transform.localScale = new Vector3(-OriginalX, OriginalY, 1);
            }
        }
    }
}