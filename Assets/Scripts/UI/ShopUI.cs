using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{

    public bool boots = false;
    public Button bootButton;
    public bool gloves = false;
    public Button glovesButton;
    public bool badge = false;
    public Button badgeButton;
    public bool gun = false;
    public Button gunButton;

    public void PurchaseBoots()
    {
        Debug.Log("You are buying boots");
        bootButton.interactable = false;
        boots = true;
    }

    public void PurchaseGloves()
    {
        Debug.Log("You are buying gloves");
        glovesButton.interactable = false;
        gloves = true;
    }

    public void PurchaseBadge()
    {
        Debug.Log("You are buying badge");
        badgeButton.interactable = false;
        badge = true;
    }

    public void PurchaseGun()
    {
        Debug.Log("You are buying revolver");
        gunButton.interactable = false;
        gun = true;
    }


}
