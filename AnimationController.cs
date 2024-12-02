using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    // An highlighted block
    [Header("CharacterAnimation")]
    public Animator ator;

    void Start()
    {
        ator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    public void playMotion()
    {
        ator.SetTrigger("moveTrigger");
        
    }
}


