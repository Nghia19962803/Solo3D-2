using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject InventoryPanel;
    [SerializeField] private GameObject displayPanel;
    [SerializeField] private GameObject doll;   // just decorate inventory
    private List<Transform> slotUI = new List<Transform> ();

    private void Start()
    {
        int maxSlot = 10;
        for (int i = 0; i < maxSlot; i++)
        {
            slotUI.Add(InventoryPanel.transform.GetChild(i));

            RegisterSlotHandle(i);
        }
        displayPanel.SetActive(false);
        doll.SetActive(false);
    }

    public void DisPlayItem(List<InventorySlot> invenSlot)
    {
        for (int i = 0; i < invenSlot.Count; i++)
        {
            slotUI[i].GetChild(0).GetComponent<Image>().sprite = invenSlot[i].Iobject.uiDisplay;
            //nếu amount != 0 => ô có item , amount = 0 => ô rỗng
            if (invenSlot[i].amount > 1)
            {
                slotUI[i].GetChild(1).GetComponent<Text>().text = invenSlot[i].amount.ToString();
            }
            else
                slotUI[i].GetChild(1).GetComponent<Text>().text = " ";


        }
    }
    public void RegisterSlotHandle(int index)
    {
            var slotBtn = slotUI[index].GetChild(0).GetComponent<Button>();
            slotBtn.onClick.AddListener(() =>
            {
                ShowItemInSlot(index);
            });
        
    }

    //show infomation of one item when pressed item in inventory
    public void ShowItemInSlot(int i)
    {
        ItemManager.Instance._ItemDetail.ShowDetailItem(Inventory.instance.slots[i].Iobject);
        ItemManager.Instance._ItemDetail.SetIndexItemInInventory(i);
    }

    //attach methor to inventory show button
    public void DisplayInventory()
    {
        doll.SetActive(true);
        displayPanel.SetActive(true);
    }

    //attach methor to inventory hide button
    public void HideInventory()
    {
        doll.SetActive(false);
        displayPanel.SetActive(false);
        ItemManager.Instance._ItemDetail.HideDetailPanel();
    }
}
