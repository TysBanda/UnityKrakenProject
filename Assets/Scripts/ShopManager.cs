using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public Transform itemListParent;
    public GameObject itemGroupPrefab;
    public TMP_Text itemNameText;
    public TMP_Text itemPriceText;

    private Dictionary<string, int> itemPrices = new Dictionary<string, int>();

    public void AddItem(string itemName, int itemPrice)
    {
        if (!itemPrices.ContainsKey(itemName))
        {
            itemPrices.Add(itemName, itemPrice);
            CreateGroupForItem(itemName, itemPrice);
        }
    }

    public void RemoveItem(string itemName)
    {
        if (itemPrices.ContainsKey(itemName))
        {
            itemPrices.Remove(itemName);
            Destroy(itemListParent.Find(itemName).gameObject);
        }
    }

    public void UpdateItemPrice(string itemName, int newPrice)
    {
        if (itemPrices.ContainsKey(itemName))
        {
            itemPrices[itemName] = newPrice;
            UpdateGroupPrice(itemName, newPrice);
        }
    }

    public Dictionary<string, int> GetItemPrices()
    {
        return itemPrices;
    }

    public void CreateGroupForItem(string itemName, int itemPrice)
    {
        GameObject group = Instantiate(itemGroupPrefab, itemListParent);
        group.name = itemName;
        TMP_Text[] texts = group.GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text text in texts)
        {
            if (text.name == "ItemName")
            {
                text.text = itemName;
            }
            else if (text.name == "ItemPrice")
            {
                text.text = itemPrice.ToString();
            }
        }
    }

    public void UpdateGroupPrice(string itemName, int newPrice)
    {
        GameObject group = itemListParent.Find(itemName).gameObject;
        TMP_Text priceText = group.transform.Find("ItemPrice").GetComponent<TMP_Text>();
        priceText.text = newPrice.ToString();
    }
}
