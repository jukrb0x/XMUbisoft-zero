using UnityEngine;
using UnityEngine.UI;

public class LevelBGM : MonoBehaviour
{
    public Slider slider;
    public AudioSource BGsound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Volume()
    {

        BGsound.volume = slider.value;

    }
}
