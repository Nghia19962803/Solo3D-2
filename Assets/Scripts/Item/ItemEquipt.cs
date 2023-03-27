using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemEquipt : MonoBehaviour
{
    private ItemObject _weaponItemObject;
    private ItemObject _armorItemObject;

    public Transform weaponPanel;
    public Transform armorPanel;

    private void Start()
    {
        _weaponItemObject = ItemManager.Instance.GetItem(0);    //item 0 is default item
        _armorItemObject = ItemManager.Instance.GetItem(0);     //item 0 is default item
    }

    public string GetItemFromDetail()
    {
        // check type of i_detail
        if(ItemManager.Instance._ItemDetail.GetItem().type.ToString() == "Weapon")
        {
            _weaponItemObject = ItemManager.Instance._ItemDetail.GetItem();
            PlayerControllerISO.Instance._stats.SetDamagePoint(_weaponItemObject);
            return "Weapon";
        }
        if(ItemManager.Instance._ItemDetail.GetItem().type.ToString() == "Armor")
        {
            _armorItemObject = ItemManager.Instance._ItemDetail.GetItem();
            PlayerControllerISO.Instance._stats.SetArmorPoint(_armorItemObject);
            return "Armor";
        }
        if (ItemManager.Instance._ItemDetail.GetItem().type.ToString() == "Food")
        {
            return "Food";
        }
        return null;
    }
    public void EquiptItem()
    {
        if(GetItemType() != null)
        {
            string type = GetItemType();
            if (type == "Weapon")
            {
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
            switch (s)
            {
                case "Weapon":
                    weaponPanel.GetChild(0).GetComponent<Image>().sprite = _weaponItemObject.uiDisplay;
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
                        Inventory.instance.slots[index].Iobject = ItemManager.Instance.GetItem(0);
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
        if (ItemManager.Instance._ItemDetail.GetItem().type.ToString() == "Weapon")
        {

            return "Weapon";
        }
        if (ItemManager.Instance._ItemDetail.GetItem().type.ToString() == "Armor")
        {

            return "Armor";
        }
        if (ItemManager.Instance._ItemDetail.GetItem().type.ToString() == "Food")
        {

            return "Food";
        }

        return null;
    }

    public ItemObject GetItem()
    {
        return _weaponItemObject;
    }
}
