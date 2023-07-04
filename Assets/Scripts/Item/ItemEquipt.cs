using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemEquipt : MonoBehaviour
{
    public ItemObject _weaponItemObject;
    private ItemObject _armorItemObject;
    private ItemObject _ringItemObject;
    private ItemObject _amuletItemObject;

    public Transform weaponPanel;
    public Transform armorPanel;
    public Transform ringPanel;
    public Transform amuletPanel;

    private void Start()
    {
        _weaponItemObject = ItemManager.Instance.GetDefaultItem();    //item 0 is default item
        _armorItemObject = ItemManager.Instance.GetDefaultItem();     //item 0 is default item
        _ringItemObject = ItemManager.Instance.GetDefaultItem();    //item 0 is default item
        _amuletItemObject = ItemManager.Instance.GetDefaultItem();     //item 0 is default item
    }

    public string GetItemFromDetail()
    {
        // check type of i_detail
        if(ItemManager.Instance._ItemDetail.GetDetailItemObject().type.ToString() == "Weapon")
        {
            _weaponItemObject = ItemManager.Instance._ItemDetail.GetDetailItemObject();
            PlayerControllerISO.Instance._stats.SetDamagePoint(_weaponItemObject);

            return "Weapon";
        }
        if(ItemManager.Instance._ItemDetail.GetDetailItemObject().type.ToString() == "Armor")
        {
            _armorItemObject = ItemManager.Instance._ItemDetail.GetDetailItemObject();
            PlayerControllerISO.Instance._stats.SetArmorPoint(_armorItemObject);
            return "Armor";
        }
        if (ItemManager.Instance._ItemDetail.GetDetailItemObject().type.ToString() == "Food")
        {
            return "Food";
        }
        return null;
    }
    public void OnEquiptItemButtonClick()
    {
        /*
         kiểm tra xem ở ô detail có item ko. nếu có check xem item đó type là gì. nếu ko có return
         */
        if(GetItemType() != null)
        {
            //check index của item trong inventory. sáu đó swap item đó với item hiện có trong equip
            string type = GetItemType();
            if (type == "Weapon")
            {
                Transform tran = PlayerControllerISO.Instance.GetWeaponHolder();
                int index = ItemManager.Instance._ItemDetail.GetIndexItemInInventory();
                Inventory.instance.ChangeItemInSlots(index, _weaponItemObject);
                

            }
            if (type == "Armor")
            {
                int index = ItemManager.Instance._ItemDetail.GetIndexItemInInventory();
                Inventory.instance.ChangeItemInSlots(index, _armorItemObject);
            }

            
            Inventory.instance.RefreshInventory();
            string s = GetItemFromDetail();

            //tùy vào type của item mà sẻ lựa chọn các trường hợp sau:
            // nếu là weapon thì change ảnh của ô equit sang ô item
            // nếu là food thì sử dụng luôn
            switch (s)
            {
                case "Weapon":
                    weaponPanel.GetChild(0).GetComponent<Image>().sprite = _weaponItemObject.uiDisplay;
                    ItemManager.Instance.GetPrefabItem(_weaponItemObject);
                    ItemManager.Instance._ItemDetail.SetDefaultItem();
                    break;
                case "Armor":
                    armorPanel.GetChild(0).GetComponent<Image>().sprite = _armorItemObject.uiDisplay;
                    ItemManager.Instance._ItemDetail.SetDefaultItem();
                    break;
                case "Food":

                    int index = ItemManager.Instance._ItemDetail.GetIndexItemInInventory();

                    PlayerControllerISO.Instance._stats.Regen(Inventory.instance.slots[index].Iobject);

                    int _amount = Inventory.instance.slots[index].amount;
                    // Th1: còn hơn 1 bình máu, sử dụng xong -amount đi 1
                    if (_amount > 1)
                    {
                        Inventory.instance.slots[index].amount = _amount - 1;
                    }
                    // Th còn lại : còn 1 bình máu duy nhất và sử dụng xong nó biến mất
                    else
                    {
                        Inventory.instance.slots[index].amount = 0;
                        Inventory.instance.slots[index].Iobject = ItemManager.Instance.GetDefaultItem();
                        ItemManager.Instance._ItemDetail.SetDefaultItem();
                    }
                    Inventory.instance.RefreshInventory();
                    Debug.Log("bottle");
                    break;
                default:

                    break;
            }
        }


    }
    public string GetItemType()
    {
        // check type of i_detail
        if (ItemManager.Instance._ItemDetail.GetDetailItemObject().type.ToString() == "Weapon")
        {

            return "Weapon";
        }
        if (ItemManager.Instance._ItemDetail.GetDetailItemObject().type.ToString() == "Armor")
        {

            return "Armor";
        }
        if (ItemManager.Instance._ItemDetail.GetDetailItemObject().type.ToString() == "Food")
        {

            return "Food";
        }

        return null;
    }
    public void SetEquipmentSlot(ItemObject weapon, ItemObject armor, ItemObject ring, ItemObject amulet)
    {
        PlayerControllerISO.Instance._stats.SetDamagePoint(weapon);
        weaponPanel.GetChild(0).GetComponent<Image>().sprite = weapon.uiDisplay;

        PlayerControllerISO.Instance._stats.SetArmorPoint(armor);
        armorPanel.GetChild(0).GetComponent<Image>().sprite = armor.uiDisplay;

        //set Ring

        //set Amulet
    }
    public ItemObject GetCurrentWeaponEquipment()
    {
        return _weaponItemObject;
    }
    public ItemObject GetCurrentArmorEquipment()
    {
        return _armorItemObject;
    }
    public ItemObject GetCurrentRingEquipment()
    {
        return _ringItemObject;
    }
    public ItemObject GetCurrentAmuletEquipment()
    {
        return _amuletItemObject;
    }
}

