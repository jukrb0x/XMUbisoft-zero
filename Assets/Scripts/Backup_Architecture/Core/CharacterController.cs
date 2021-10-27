using UnityEngine;

namespace Core
{
    public class CharacterController : MonoBehaviour
    {
        //[SerializeField] private float speed = 20f;  //REMOVE it, because we no longer use it anymore

        // Controls the current movement of this character    
        public Vector2 CurrentMovement { get; set; }

        // Returns if this character can move normally (When dashing we can't)
        public bool NormalMovement { get; set; }


        // Internal
        private Rigidbody2D _rigidbody2D;

        // Start is called before the first frame update
        void Start()
        {
            NormalMovement = true;
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (NormalMovement)
            {
                MoveCharacter();
            }
        }

        private void MoveCharacter()
        {
            Vector2 currentMovePosition = _rigidbody2D.position + CurrentMovement * Time.fixedDeltaTime;
            _rigidbody2D.MovePosition(currentMovePosition);
        }

        //Extra Move in case we need it
        public void MovePosition(Vector2 newPosition)
        {
            _rigidbody2D.MovePosition(newPosition);
        }

        //Sets the current movement of our character
        public void SetMovement(Vector2 newPosition)
        {
            CurrentMovement = newPosition;
        }
    }
}