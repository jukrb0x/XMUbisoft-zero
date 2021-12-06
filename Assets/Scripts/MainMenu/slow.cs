using UnityEngine;
using UnityEngine.UI;

public class slow : MonoBehaviour
{
    public float charsPerSecond = 0.05f;
    private int currentPos;

    private bool isActive;
    private Text myText; //get test
    private float timer; //Timer
    private string words;

    private void Awake()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    private void Update()
    {
        OnStartWriter();
    }

    private void OnEnable()
    {
        timer = 0;
        isActive = true;
        charsPerSecond = Mathf.Max(0.01f, charsPerSecond);
        myText = GetComponent<Text>();
        words = myText.text;
        myText.text = "";
    }

    // Use this for initialization
    private void OnDisable()
    {
        OnFinish();
    }

    private void OnStartWriter()
    {
        if (isActive)
        {
            timer += Time.deltaTime;
            if (timer >= charsPerSecond)
            {
                timer = 0;
                currentPos++;


                myText.text = words.Substring(0, currentPos);

                if (currentPos >= words.Length) OnFinish();
            }
        }
    }

    private void OnFinish()
    {
        isActive = false;
        timer = 0;
        currentPos = 0;
        myText.text = words;
    }
}