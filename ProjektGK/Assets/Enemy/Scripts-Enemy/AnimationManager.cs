using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField]
    public Animator Anim;

    enum animationState
    {
        Spawn, Idle, Run, RunAttack, Walk, WalkAttack, Death
    }


// Start is called before the first frame update
    void Start()
    {
        if (!Anim)
        {
            Anim = GetComponent<Animator>();
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
