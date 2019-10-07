﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemCostText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    [Header("Item Settings")]
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private int itemCost;
    [SerializeField] private string itemDescription;

    [Header("Color Settings")]
    [SerializeField] private Color affordableTextColor;
    [SerializeField] private Color unaffordableTextColor;
    [SerializeField] private Color unaffordableItemColor;
    
    private GameManager gm;

    private void Start()
    {
        itemNameText.text = itemName;
        itemImage.sprite = itemSprite;
        itemCostText.text = "$" + itemCost.ToString();

        gm = FindObjectOfType<GameManager>();

        StartCoroutine(UpdateItem());
    }

    IEnumerator UpdateItem()
    {
        while(true)
        {
            if (Inventory.Money >= itemCost)
            {
                itemImage.color = Color.white;
                itemCostText.color = affordableTextColor;
            }
            else
            {
                itemImage.color = unaffordableItemColor;
                itemCostText.color = unaffordableTextColor;
            }
            
            yield return new WaitForSeconds(1f);
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionText.text = itemDescription;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionText.text = "";
    }

    public void BuyItem()
    {
        if (Inventory.Money >= itemCost)
        {
            // Terrible, terrible hack
            switch(itemName)
            {
                case "Boots":
                    Inventory.HasBoots = true;
                    break;

                case "Gloves":
                    Inventory.HasGloves = true;
                    break;

                case "Fake Badge":
                    Inventory.HasBadge = true;
                    break;

                case "Hoodie":
                    Inventory.HasHoodie = true;
                    break;

                case "Pickpocket Guide":
                    Inventory.HasGuide = true;
                    break;

                case "Police Scanner":
                    Inventory.HasScanner = true;
                    break;

                case "Sign":
                    Inventory.HasSign = true;
                    break;

                case "Revolver":
                    Inventory.HasRevolver = true;
                    gm.RevolverBought();
                    break;
            }

            Debug.Log("[Shop] Purchased " + itemName);

            Inventory.Money -= itemCost;
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("[Shop] Insufficient funds to purchase " + itemName);
        }
    }
}
