using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Component
{
    public class CharacterDash : CharacterComponents
    {
        [SerializeField] private float dashDistance = 3f;
        [SerializeField] private float dashDuration = 0.1f;

        private bool isDashing;
        private float dashTimer;
        private Vector2 dashOrigin;
        private Vector2 dashDestination;
        private Vector2 newPosition;

        protected override void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Dash();
            }
        }

        protected override void HandleAbility()
        {
            base.HandleAbility();

            if (isDashing)
            {
                if (dashTimer < dashDuration)
                {
                    newPosition = Vector2.Lerp(dashOrigin, dashDestination, dashTimer / dashDuration);
                    controller.MovePosition(newPosition);
                    dashTimer += Time.deltaTime;
                }
                else
                {
                    StopDash();
                }
            }
        }

        private void Dash()
        {
            isDashing = true;
            dashTimer = 0f;
            controller.NormalMovement = false;
            dashOrigin = transform.position;

            dashDestination = transform.position + (Vector3)controller.CurrentMovement.normalized * dashDistance;
        }
        private void StopDash()
        {
            isDashing = false;
            controller.NormalMovement = true;
        }
    }
}
