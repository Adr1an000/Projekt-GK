//author: Dawid Musialik
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

// author Dawid Musialik

public class UpdateStatsScript : MonoBehaviour
{
    public Text killsAmmount;
    public Text deathsAmmount;
    public Text timeAmmount;

    // Start is called before the first frame update
    void Start()
    {
        Stats s=GameObject.Find("Stats").GetComponent<Stats>();//new
        killsAmmount.text = s.Kills.ToString();
        deathsAmmount.text = s.Deaths.ToString();
        timeAmmount.text = s.getTime();
    }

} 
