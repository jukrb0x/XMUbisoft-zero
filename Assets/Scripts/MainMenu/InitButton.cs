using UnityEngine;
using UnityEngine.EventSystems;

public class InitButton : MonoBehaviour
{
    private GameObject lastSelect;


    private void Start()
    {
        lastSelect = new GameObject();
    }

    // Update is called once per frame
    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
            EventSystem.current.SetSelectedGameObject(lastSelect);
        else
            lastSelect = EventSystem.current.currentSelectedGameObject;
    }
}