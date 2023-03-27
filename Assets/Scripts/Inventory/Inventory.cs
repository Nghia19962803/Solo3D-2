using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance { get { return s_Instance; } }
    private static Inventory s_Instance;

    private InventoryUI invenUI;
    public InventoryUI _InvenUI { get { return invenUI; } }

    public List<InventorySlot> slots = new List<InventorySlot>();
    private int maxSlot;

    public bool check;
    private void Start()
    {
        s_Instance = this;
        invenUI = GetComponent<InventoryUI>();
        maxSlot = 10;
        for (int i = 0; i < maxSlot; i++)
        {
            InventorySlot _invenSlot = new InventorySlot(i,ItemManager.Instance.GetItem(0), 0);
            slots.Add(_invenSlot);
        }
        AddItem(ItemManager.Instance.GetItem("Armor_1"));
        AddItem(ItemManager.Instance.GetItem("Weapon_1"));
        invenUI.DisPlayItem(slots);
    }
    private void Update()
    {
        if(check)
        {
            RefreshInventory();
            check = false;
        }
    }
    public void AddItem(ItemObject _item)
    {
        if (_item.stackable)
        {
            //check item exist inventory or not
            foreach (var item in slots)
            {
                if(item.Iobject.name == _item.name)
                {
                    item.amount += 1;   //if have => inscreate amount by 1
                    invenUI.DisPlayItem(slots); // reset inventory UI
                    return;
                }
            }
        }
        //if item is not exist in inventory => create one
        int slotIndex = FindEmptySlot();    
        if (slotIndex >= 0)
        {
            slots[slotIndex] = new InventorySlot(slotIndex, _item, 1);
        }
        invenUI.DisPlayItem(slots);
        return;
    }
    public int FindEmptySlot()
    {
        foreach (var slot in slots)
        {
            if(slot.Iobject.Name == "Default")
            {
                return slot.ID;
            }
        }
        return -1;
    }
    public void RefreshInventory()
    {
        invenUI.DisPlayItem(slots);
    }
    public void ChangeItemInSlots(int index, ItemObject _ioject)
    {
        InventorySlot _invenSlot = new InventorySlot(index, _ioject, 1);
        slots[index].Iobject = _invenSlot.Iobject;
    }

}

// this class help manager inventory slot
public class InventorySlot
{
    public int ID;
    public ItemObject Iobject;
    public int amount;
    public InventorySlot(int _id , ItemObject _iobject, int _amount)
    {
        ID = _id;
        Iobject = _iobject;
        amount = _amount;
    }
}
