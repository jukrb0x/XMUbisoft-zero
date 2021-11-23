using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class slow : MonoBehaviour
{

    public float charsPerSecond = 0.05f;
    private string words;

    private bool isActive = false; 
    private float timer;//Timer
    private Text myText;//get test
    private int currentPos = 0;

    // Use this for initialization
    private void OnDisable()
    {
        OnFinish();
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
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        OnStartWriter();
    }
    void OnStartWriter()
    {

        if (isActive)
        {
            timer += Time.deltaTime;
            if (timer >= charsPerSecond)
            {
                timer = 0;
                currentPos++;


                myText.text = words.Substring(0, currentPos);

                if (currentPos >= words.Length)
                {
                    OnFinish();
                }
            }

        }
    }
    void OnFinish()
    {
        isActive = false;
        timer = 0;
        currentPos = 0;
        myText.text = words;
    }




}