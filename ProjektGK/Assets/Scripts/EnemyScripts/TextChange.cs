using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// author: Paweł Salicki

public class TextChange : MonoBehaviour
{
    private string[] faces = { ":)", ":O", "OwO", "(° ͜ʖ°)", "UwU", "xD", ";d", "◉_◉", "'ㅅ'", " (▀̿̿Ĺ̯̿▀̿ ̿)" };

    private float timeLeft = 5.0f;

    public Text faceText;

    void Start()
    {
        GetRandomFace();
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            GetRandomFace();

            timeLeft = 5.0f;
        }
    }

    private void GetRandomFace()
    {
        int index = Random.Range(0, faces.Length - 1);

        faceText.GetComponent<Text>().text = faces[index];
    }
}
