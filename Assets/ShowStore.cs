using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowStore : MonoBehaviour
{
    Store store;

    [SerializeField] ShowItem itemDetails;
    [SerializeField] GameObject storeObj;
    [SerializeField] GameObject itemListPrefab;
    [SerializeField] Transform listParent;

    List<GameObject> itemList = new List<GameObject>();

    public static ShowStore instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);

        instance = this;
    }

    public void OpenStore(Store newStore)
    {
        storeObj.SetActive(true);
        store = newStore;

        foreach (GameObject obj in itemList)
            Destroy(obj);

        itemList.Clear();

        foreach(IItem item in newStore.inventory)
        {
            itemList.Add(Instantiate(itemListPrefab, listParent));

            itemList[itemList.Count - 1].GetComponent<ShowItem>().UpdateItem(item);
            Debug.Log("Adding " + item.name + " to the shop list");
        }
    }

    public void CloseStore() => storeObj.SetActive(false);

   

    public void UpdateDetails(IItem item)
    {
        foreach (GameObject obj in itemList)
            obj.GetComponent<Image>().enabled = obj.GetComponent<ShowItem>().item == item? true : false;

       
        itemDetails.UpdateItem(item);
    }
}

