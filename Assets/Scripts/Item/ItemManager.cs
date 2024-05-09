using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private static ItemManager instance;
    public static ItemManager Instance { get { return instance; } }

    private ItemDetail itemDetail;
    public ItemDetail _ItemDetail { get { return itemDetail; } }

    private ItemEquipt itemEquipt;
    public ItemEquipt _itemEquipt { get { return itemEquipt; } }

    public ItemObject[] items;  // make sure items[0] is default Item. because it relate to inventoryUI
    void Awake()
    {
        instance = this;
        itemDetail = GetComponent<ItemDetail>();  
        itemEquipt = GetComponent<ItemEquipt>();
    }

    public ItemObject GetItem(int i)
    {
        return items[i];
    }
    public ItemObject GetItem(string s)
    {
        foreach (var item in items)
        {
            if(item.name == s)
                return item;
        }
        return null;
    }
}
