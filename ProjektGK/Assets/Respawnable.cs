//author: Adrian Skutela

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawnable : MonoBehaviour
{
    public ObjectSpawner objectSpawner {get; set; }

    public void Respawn()
    {
        objectSpawner.Respawn();
    }
}
