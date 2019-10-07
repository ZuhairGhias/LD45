using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip reload;

    public void PlayAnimation()
    {
        animator.Play("Kill");
    }

    public void PlayReloadSound()
    {
        audioSource.PlayOneShot(reload);
    }
}
