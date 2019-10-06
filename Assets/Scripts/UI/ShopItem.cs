using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemCostText;

    [Header("Item Settings")]
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private int itemCost;

    private void Start()
    {
        itemNameText.text = itemName;
        itemImage.sprite = itemSprite;
        itemCostText.text = "$" + itemCost.ToString();
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

                case "Badge":
                    Inventory.HasBadge = true;
                    break;

                case "Newspaper":
                    Inventory.HasNewspaper = true;
                    break;

                case "Guide":
                    Inventory.HasGuide = true;
                    break;

                case "Alcohol":
                    Inventory.HasAlcohol = true;
                    break;

                case "Coffee":
                    Inventory.HasCoffee = true;
                    break;

                case "Revolver":
                    Inventory.HasRevolver = true;
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
