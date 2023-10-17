using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAnimBoss : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void PlayHitAnimation()
    {
        animator.SetTrigger("HitBoss");
    }
}