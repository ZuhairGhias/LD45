using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutsceneDoorTrigger : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private UnityEvent onDoorOpen;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        onDoorOpen.Invoke();
        doorAnimator.Play("DoorOpen");
    }
}
