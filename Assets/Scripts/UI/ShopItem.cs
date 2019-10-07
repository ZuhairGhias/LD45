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
    [SerializeField] private string itemDescription;

    private GameManager gm;

    private void Start()
    {
        itemNameText.text = itemName;
        itemImage.sprite = itemSprite;
        itemCostText.text = "$" + itemCost.ToString();
        gm = FindObjectOfType<GameManager>();
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

                case "Coffee":
                    Inventory.HasCoffee = true;
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
