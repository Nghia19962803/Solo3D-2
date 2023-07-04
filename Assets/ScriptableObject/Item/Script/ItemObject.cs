using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Food, Weapon, Armor, Default
}
public enum Attributes
{
    Health,
    Damage,
    Armor,
    None
}
public abstract class ItemObject : ScriptableObject
{
    public Sprite uiDisplay;
    public ItemType type;
    public string Name;
    public bool stackable;
    public GameObject prefab;
    public ItemBuff buff;
}

[System.Serializable]
public class ItemBuff
{
    public Attributes attribute;
    public int value;
}
