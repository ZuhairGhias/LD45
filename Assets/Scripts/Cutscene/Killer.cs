using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void PlayAnimation()
    {
        animator.Play("Kill");
    }
}
