using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerPush : MonoBehaviour
{
    public GameObject textDisplay;
    public int secondsLeft = 30;
    public int minutesLeft = 1;

    private bool startTick = true;

    void Start()
    {
        if(minutesLeft > 9)
        {
            textDisplay.GetComponent<Text>().text = minutesLeft + ":" + secondsLeft;
        }
        else
        {
            textDisplay.GetComponent<Text>().text = "0" + minutesLeft + ":" + secondsLeft;
        }

    }

    void Update()
    {
        if (startTick && (minutesLeft > 0 || secondsLeft > 0))
        {
            StartCoroutine(TimerTake());
        }
    }

    IEnumerator TimerTake()
    {
        startTick = false;

        yield return new WaitForSeconds(1);
        secondsLeft -= 1;

        if(secondsLeft < 0)
        {
            secondsLeft = 59;
            minutesLeft -= 1;
        }

        string minutesString = "";

        if(minutesLeft < 10)
        {
            minutesString = "0" + minutesLeft;
        }
        else
        {
            minutesString = minutesLeft.ToString();
        }

        if(secondsLeft < 10)
        {
            textDisplay.GetComponent<Text>().text = minutesString + ":0" + secondsLeft;
        }
        else
        {
            textDisplay.GetComponent<Text>().text = minutesString + ":" + secondsLeft;
        }

        startTick = true;
    }
}
