using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Inventory : MonoBehaviour
{
    public static Inventory instance { get { return s_Instance; } }
    private static Inventory s_Instance;

    private InventoryUI invenUI;

    public List<InventorySlot> slots = new List<InventorySlot>();
    private int maxSlot;

    public bool check;
    public bool saveInven;
    public bool loadInven;
    private void Start()
    {
        s_Instance = this;
        invenUI = GetComponent<InventoryUI>();
        maxSlot = 10;
        for (int i = 0; i < maxSlot; i++)
        {
            InventorySlot _invenSlot = new InventorySlot(i,ItemManager.Instance.GetDefaultItem(), 0);
            slots.Add(_invenSlot);
        }
        //AddItem(ItemManager.Instance.GetItem("Armor_1"));
        //AddItem(ItemManager.Instance.GetItem("Weapon_1"));
        invenUI.DisPlayItem(slots);
        //StartCoroutine(LoadInven());
    }
    private void Update()
    {
        if(check)
        {
            RefreshInventory();
            check = false;
        }
        if (saveInven)
        {
            SaveInventoryData();
            saveInven = false;
        }
        if (loadInven)
        {
            LoadInventoryData();
            invenUI.DisPlayItem(slots);
            loadInven = false;
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

    #region ========================== SAVE AND LOAD =============================
    public void SaveInventoryData()     //========= SAVE INVENTORY =========//
    {
        if(slots.Count > 0)
        {
            DataInventory data = new DataInventory();
            data.listItem = slots;
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + "/InventoryData.json", json);
        }

    }
    public List<InventorySlot> LoadInventoryData()                  //========= LOAD INVENTORY =========//
    {
        if(File.Exists(Application.persistentDataPath + "/InventoryData.json"))
        {
            DataInventory data = new DataInventory();
            string json = File.ReadAllText(Application.persistentDataPath + "/InventoryData.json");
            data = JsonUtility.FromJson<DataInventory>(json);

            slots = data.listItem;

        }
        return slots;
    }
    #endregion

    IEnumerator LoadInven()
    {
        yield return new WaitForSeconds(1);
        LoadInventoryData();
        invenUI.DisPlayItem(slots);
    }
}

// this class help manager inventory slot
[System.Serializable]
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

[System.Serializable]
public class DataInventory
{
    public List<InventorySlot> listItem;
}
