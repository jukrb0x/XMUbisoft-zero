using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToNextLevel : MonoBehaviour
{
    [SerializeField] private LayerMask PassMask;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CheckLayer(other.gameObject.layer, PassMask))
        { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private bool CheckLayer(int layer,LayerMask objectMask)
    {
        return ((1 << layer) & objectMask )!= 0;
    }
}
