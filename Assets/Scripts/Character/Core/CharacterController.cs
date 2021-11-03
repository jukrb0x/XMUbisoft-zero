using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    // Controls the current movement of this character    
    public Vector2 CurrentMovement { get; set; }

    // Returns if this character can move normally (When dashing we can't)
    public bool NormalMovement { get; set; }
	
    // Internal
    private Rigidbody2D myRigidbody2D;
    private Vector2 recoilMovement;
	
    // Start is called before the first frame update
    void Start()
    {
        NormalMovement = true;
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Recoil();

        if (NormalMovement)
        {
            MoveCharacter();
        }
    }
	
    private void MoveCharacter()
    {    
        Vector2 currentMovePosition = myRigidbody2D.position + CurrentMovement * Time.fixedDeltaTime;
        myRigidbody2D.MovePosition(currentMovePosition);
    }

    public void ApplyRecoil(Vector2 recoilDirection, float recoilForce)
    {
        recoilMovement = recoilDirection.normalized * recoilForce;
    }

    // Extra Move in case we need it    
    public void MovePosition(Vector2 newPosition)
    {
        myRigidbody2D.MovePosition(newPosition);
    }

    // Sets the current movement of our character
    public void SetMovement(Vector2 newPosition)
    {
        CurrentMovement = newPosition;
    }

    private void Recoil()
    {
        if (recoilMovement.magnitude > 0.1f)
        {
            myRigidbody2D.AddForce(recoilMovement);
        }
    }
}