using UnityEngine;

public class DoorEnter : MonoBehaviour
{
    public Transform backDoor;


    private bool CanTransform = true;
    private Transform playerTransform;
    [SerializeField] private float timeBtwTransform = 0.01f;

    private float nextTransformTime;


    // Start is called before the first frame update
    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    private void Update()
    {
        PlayerCanTransform();
    }

    private void EnterDoor()
    {
        playerTransform.position = backDoor.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")
        )
        {
            Debug.Log("触碰到门了");
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