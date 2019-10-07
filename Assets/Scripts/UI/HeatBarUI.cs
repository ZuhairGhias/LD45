using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatBarUI : MonoBehaviour
{
    [SerializeField] private Image heatBar;

    private void Start()
    {
        UpdateHeat(0f);
    }

    public void UpdateHeat(float amount)
    {
        //heatBar.fillAmount = amount;
    }
}
