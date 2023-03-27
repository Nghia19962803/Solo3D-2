using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetail : MonoBehaviour
{
    [SerializeField] GameObject detailPanel;
    private Image detailImage;
    private Text Name_Text, attibute_text, type_text;

    private ItemObject itemReserve;
    private int indexItemInInventory;
    void Start()
    {
        detailImage = detailPanel.transform.GetChild(0).GetComponent<Image>();  //image
        Name_Text = detailPanel.transform.GetChild(1).GetComponent<Text>();     //name item
        attibute_text = detailPanel.transform.GetChild(2).GetComponent<Text>(); //chỉ số    
        type_text = detailPanel.transform.GetChild(3).GetComponent<Text>();     //type item

        SetDefaultItem();
        detailPanel.SetActive(false);
    }

    // Receive item infomation and show it in detail panel
    public void ShowDetailItem(ItemObject _item)
    {
        DisplayDetailPanel();

        detailImage.sprite = _item.uiDisplay;
        Name_Text.text = _item.Name;
        attibute_text.text = _item.buff.attribute.ToString()+" +"+_item.buff.value.ToString();
        type_text.text= _item.type.ToString();
        SetItem(_item); // save item infomation to swap item from equip panel to inventory panel

    }
    public void SetItem(ItemObject item)
    {
        itemReserve = item;
    }
    public ItemObject GetItem()
    {
        return itemReserve;
    }
    public void SetIndexItemInInventory(int i)
    {
        indexItemInInventory = i;
    }
    public int GetIndexItemInInventory()
    {
        return indexItemInInventory;
    }
    public void SetDefaultItem()
    {
        detailImage.sprite = null;
        Name_Text.text = "";
        attibute_text.text = "";
        type_text.text = "";
        itemReserve = ItemManager.Instance.GetItem(0);
    }
    public void DisplayDetailPanel()
    {
        detailPanel.SetActive(true);
    }
    public void HideDetailPanel()
    {
        detailPanel.SetActive(false);
    }
}
