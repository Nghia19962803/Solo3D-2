using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ItemManager : MonoBehaviour
{
    private static ItemManager instance;
    public static ItemManager Instance { get { return instance; } }

    private ItemDetail itemDetail;
    public ItemDetail _ItemDetail { get { return itemDetail; } }

    private ItemEquipt itemEquipt;
    public ItemEquipt _itemEquipt { get { return itemEquipt; } }

    public ItemObject defaultItem;  // make sure item is default Item. because it relate to inventoryUI

    private ItemObject currentWeapon;
    private ItemObject currentArmor;
    private ItemObject currentRing;
    private ItemObject currentAmulet;

    public bool save;
    public bool load;
    public bool check;
    [Header("WEAPON ITEM")]
    public ItemObject[] weapons;



    void Awake()
    {
        instance = this;
        itemDetail = GetComponent<ItemDetail>();  
        itemEquipt = GetComponent<ItemEquipt>();
        LoadEquipmentData();
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        if (save)
        {
            save = false;
            SaveCurrentEquiptmentData();
        }
        if (load)
        {
            load = false;
            LoadEquipmentData();
        }
        //if (check)
        //{
        //    check = false;
        //    GetPrefabItem();
        //}
    }
    public ItemObject GetDefaultItem()
    {
        return defaultItem;
    }

    #region ========================== SAVE AND LOAD =============================
    public void SaveCurrentEquiptmentData()     //========= SAVE CURRENT EQUIPMENT =========//
    {
        ListCurrentItemEquipt data = new ListCurrentItemEquipt();
        data.dataCurrentWeapon = _itemEquipt.GetCurrentWeaponEquipment();       //weapon
        data.dataCurrentArmor = _itemEquipt.GetCurrentArmorEquipment();         //armor 
        data.dataCurrentRing = _itemEquipt.GetCurrentRingEquipment();           //ring
        data.dataCurrentAmulet = _itemEquipt.GetCurrentAmuletEquipment();       //amulet
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/CurrentEquipmentData.json", json);
        Debug.Log("save");
    }
    public void LoadEquipmentData()              //========= LOAD CURRENT EQUIPMENT =========//
    {
        ListCurrentItemEquipt data = new ListCurrentItemEquipt();
        string json = File.ReadAllText(Application.persistentDataPath + "/CurrentEquipmentData.json");
        data = JsonUtility.FromJson<ListCurrentItemEquipt>(json);

        currentWeapon = data.dataCurrentWeapon;
        currentArmor = data.dataCurrentArmor;
        currentRing = data.dataCurrentRing; 
        currentAmulet = data.dataCurrentAmulet;

        _itemEquipt.SetEquipmentSlot(currentWeapon, currentArmor, currentRing, currentAmulet);
    }
    #endregion
    public void GetPrefabItem(ItemObject _itemOject)
    {
        Transform tran = PlayerControllerISO.Instance.GetWeaponHolder();
        foreach (var item in weapons)
        {
            if(_itemOject == item)
            {
                Instantiate(item.prefab, tran.position, tran.rotation, tran);
                return;
            }
        }
    }

}
[System.Serializable]
public class ListCurrentItemEquipt
{
    public ItemObject dataCurrentWeapon;
    public ItemObject dataCurrentArmor;
    public ItemObject dataCurrentRing;
    public ItemObject dataCurrentAmulet;
}

