using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private PlayerController controller;
    
    void Start()
    {
        controller = GetComponent<PlayerController>();
    }
    
    void Update()
    {
        controller.Move(Input.GetAxis("Horizontal"));

        if (Input.GetKeyDown("e"))
        {
            controller.Steal();
        }

        if (Input.GetKeyDown("space"))
        {
            controller.ToggleHide();
        }
    }
}
