using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class ShowItem : MonoBehaviour
{
    public IItem item;

    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI cost;

    public void UpdateItem(IItem newItem, Store store = null)
    {
        item = newItem;

        if (icon)
            icon.sprite = item.icon;
        if (itemName)
            itemName.text = item.name;
        if (description)
            description.text = item.description;
        if (cost)
            cost.text = store == null ? item.basePrice.ToString() : ((float)item.basePrice * store.multiply).ToString();
    }

}