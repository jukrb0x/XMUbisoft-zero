using UnityEngine;

namespace Component
{
    public class CharacterRun : CharacterComponents
    {
        [SerializeField] private float runSpeed = 10f;

        protected override void HandleInput()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Run();
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                StopRun();
            }
        }

        private void Run()
        {
            characterMovement.MoveSpeed = runSpeed;
        }
        private void StopRun()
        {
            characterMovement.ResetSpeed();
        }
    }
}
