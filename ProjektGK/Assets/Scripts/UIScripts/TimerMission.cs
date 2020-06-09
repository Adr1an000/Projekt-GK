using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// author: Paweł Salicki

public class TimerMission : MonoBehaviour
{
    public int countdownTime;
    public Text countdownDisplay;

    private void Start()
    {
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        while(countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1);

            countdownTime--;
        }

        countdownDisplay.text = "Save the world (▀̿Ĺ̯▀̿ ̿)";

        yield return new WaitForSeconds(2);

        countdownDisplay.gameObject.SetActive(false);
    }
}
