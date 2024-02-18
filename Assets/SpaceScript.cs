using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SpaceScript : MonoBehaviour
{
    public Vector2Int loc;
    public bool available { get; private set; }
    [SerializeField] string shopName;

    public Store store;


    public List<IItem> items = new List<IItem>();

    public void SteppedOn(Player player)
    {
        foreach (IItem item in items)
            item.SteppedOn(player);
    }

    private void Start()
    {
        if (shopName != "")
            store = ItemList.GetItem(shopName)as Store;
        BoardScript.instance.board.Add(loc, this);
        available = true;
    }

    private void Update()
    {
        Debug.Log(items.Count);
    }
}
