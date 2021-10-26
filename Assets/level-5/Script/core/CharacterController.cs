using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //[SerializeField] private float speed = 20f;  //REMOVE it, because we no longer use it anymore

    // Controls the current movement of this character    
    public Vector2 CurrentMovement { get; set; }

    // Internal
    private Rigidbody2D myRigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        Vector2 currentMovePosition = myRigidbody2D.position + CurrentMovement * Time.fixedDeltaTime;
        myRigidbody2D.MovePosition(currentMovePosition);
    }

    // Sets the current movement of our character
    public void SetMovement(Vector2 newPosition)
    {
        CurrentMovement = newPosition;
    }
}


