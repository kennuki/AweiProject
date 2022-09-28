using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Item
{
    
    public enum ItemType
    {
        HealthPotion,
        ManaPotion,
        Crystal,
        OldLetter,
        Coin,
    }
    public ItemType itemType;
    public int amount;



    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.HealthPotion: return ItemAsset.Instance.HealthPotion;
            case ItemType.ManaPotion:   return ItemAsset.Instance.ManaPotion;
            case ItemType.Crystal:      return ItemAsset.Instance.Crystal;
            case ItemType.OldLetter:    return ItemAsset.Instance.OldLetter;
        }
    }

    public GameObject GetGameObject()
    {
        switch (itemType)
        {
            default:
            case ItemType.HealthPotion:  return ItemAsset.Instance.HealthPotion3D;
            case ItemType.ManaPotion:    return ItemAsset.Instance.ManaPotion3D;
            case ItemType.Crystal:       return ItemAsset.Instance.Crystal3D;
            case ItemType.OldLetter:     return ItemAsset.Instance.OldLetter3D;
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            
            case ItemType.ManaPotion:
            case ItemType.Crystal:
            case ItemType.HealthPotion:
                return true;
            case ItemType.OldLetter:
                return false;

        }
    }
}
