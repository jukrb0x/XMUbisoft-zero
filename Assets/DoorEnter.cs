using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEnter : MonoBehaviour
{
    public Transform backDoor;


    private bool CanTransform = true;
    private Transform playerTransform;
    [SerializeField] private float timeBtwTransform =0.01f;

    private float nextTransformTime;





    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerCanTransform();
    }

    void EnterDoor()
    {
       
            playerTransform.position = backDoor.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")
           )
        {

            Debug.Log("´¥Åöµ½ÃÅÁË");
            if (CanTransform)
            {
                EnterDoor();
                CanTransform = false;
            }
            
        }
    }

    protected void PlayerCanTransform()
    {
        if (Time.time > nextTransformTime)
        {
            CanTransform = true;
            nextTransformTime = Time.time + timeBtwTransform;
        }
    }
    
   /* void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")
           )
        {
            Debug.Log;
            CanTransform = false;
        }
    }
   */
   
    
}
