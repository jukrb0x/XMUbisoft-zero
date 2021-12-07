using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnderAttacked : MonoBehaviour
{
    private Image img;
    [SerializeField] private float time;
    [SerializeField] private Color flashColor;
    private Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        img = GameObject.Find("UnderAttackedRedScreenFlash").GetComponent<Image>();
        originalColor = img.color;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void FlashScreen()
    {
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        img.color = flashColor;
        yield return new WaitForSeconds(time);
        img.color = originalColor;
    }
}