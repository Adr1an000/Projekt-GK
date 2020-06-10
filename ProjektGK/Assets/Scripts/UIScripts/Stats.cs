//author: Dawid Musialik
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

// author: Dawid Musialik

public class Stats : MonoBehaviour
{
    public System.DateTime startTime;
    public int kills=0;
    public int deaths=0;
    private static bool created = false;

    public DateTime StartTime { get => startTime; set => startTime = value; }
    public int Kills { get => kills; set => kills = value; }
    public int Deaths { get => deaths; set => deaths = value; }




    // Start is called before the first frame update
    void Start()
    {
        StartTime = System.DateTime.UtcNow;
        DontDestroyOnLoad(this.gameObject);
    }

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }

        else
        {
            Destroy(this.gameObject);
        }
    }

    public void increment_deaths()
    {
        Deaths++;
    }
    public void increment_kills()
    {
        Kills++;
    }
    public void reset()
    {
        kills = 0;
        deaths = 0;
        StartTime = System.DateTime.UtcNow;
    }
    public String getTime()
    {
        System.DateTime now = System.DateTime.UtcNow;

        int mins = 0;
        int secs=0;
        secs = now.Second - startTime.Second;
        if(secs<0)
        {
            secs += 60;
            mins= now.Minute - startTime.Minute -1;
        }
        else
            mins = now.Minute - startTime.Minute;
        return mins.ToString()+ ":"+ secs.ToString();
    }
}

