using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//add item to inventory when player colision this item object
public class GroundItem : MonoBehaviour
{
    public ItemObject _itemObject;
 
    void Start()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _itemObject.uiDisplay;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // only player can trigger
        {
            if(this._itemObject.type != ItemType.Food)
            {
                Inventory.instance.AddItem(_itemObject);
            }
            else
            {
                PlayerControllerISO.Instance._stats.Regen(this._itemObject);
            }
            Destroy(gameObject);
        }
    }
}
