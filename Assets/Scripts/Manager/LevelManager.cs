using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Character playableCharacter;
    [SerializeField] private Transform spawnPosition;

    // TODO: remove revive
    // TODO： useless code
    // Update is called once per frame
    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.P))
    //     {
    //         ReviveCharacter();
    //     }
    // }
    //
    // private void ReviveCharacter()
    // {
    //     if (playableCharacter.GetComponent<PlayerHealth>().HealthPoint <= 0)
    //     {
    //         playableCharacter.GetComponent<PlayerHealth>().Revive();
    //         playableCharacter.transform.position = spawnPosition.position;
    //     }
    // }
}