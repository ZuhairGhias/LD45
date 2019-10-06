using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSky : MonoBehaviour
{
    [SerializeField] private float initialY;
    [SerializeField] private float terminalY;

    private void Start()
    {
        UpdateSky(0);
    }

    public void UpdateSky(float progress)
    {
        transform.position = new Vector3(transform.position.x, initialY + ((1 - progress) * (terminalY - initialY)), transform.position.z);
    }
}
