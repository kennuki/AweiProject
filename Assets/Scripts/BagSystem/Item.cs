using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
        Kumo,
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
            case ItemType.Kumo:         return ItemAsset.Instance.Kumo;
        }
    }
    public string GetName()
    {
        switch (itemType)
        {
            default:
            case ItemType.HealthPotion: return "ê∂ñΩÂZêÖ";
            case ItemType.ManaPotion:   return "ñÇóÕÂZêÖ";
            case ItemType.Crystal:      return "ê∂ñΩêÖèª";
            case ItemType.OldLetter:    return "Á±êøîü";
        }
    }
    public Vector4 GetNameColor()
    {
        switch (itemType)
        {
            default:
            case ItemType.HealthPotion: return new Vector4(0.7f,1f,0.7f,1);
            case ItemType.ManaPotion:   return new Vector4(0.75f, 0.97f, 1f, 1);
            case ItemType.Crystal:      return new Vector4(1, 0.72f, 0.75f, 1);
            case ItemType.OldLetter:    return new Vector4(1, 0.98f, 0.83f, 1);
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
