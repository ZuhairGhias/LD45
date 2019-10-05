using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    private PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        pc.Move(Input.GetAxis("Horizontal"));

        if (Input.GetKeyDown("e")) pc.Steal();
        if (Input.GetKeyDown("space")) pc.Hide();
        if (Input.GetKeyUp("space")) pc.Unhide();
    }
}
