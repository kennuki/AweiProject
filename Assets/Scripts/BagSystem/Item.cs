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
        Teddy,
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
            case ItemType.Teddy:        return ItemAsset.Instance.Teddy;
        }
    }
    public string GetName()
    {
        switch (itemType)
        {
            default:
            case ItemType.HealthPotion: return "生命藥水";
            case ItemType.ManaPotion:   return "魔力藥水";
            case ItemType.Crystal:      return "生命水晶";
            case ItemType.OldLetter:    return "邀請函";
            case ItemType.Teddy:        return "泰迪";
        }
    }
    public GameObject Get3DMemoryObject()
    {
        switch (itemType)
        {
            default:
            case ItemType.OldLetter: return ItemAsset.Instance.MemoryOldLetter3D;
            case ItemType.Teddy: return ItemAsset.Instance.MemoryTeddy3D;
        }
    }
    public string Get3DMemoryObjectName()
    {
        switch (itemType)
        {
            default: return "";
            case ItemType.OldLetter: return "InvitationCard";
            case ItemType.Teddy:     return "Teddy";
        }
    }
    public string GetInfo()
    {
        switch (itemType)
        {
            default:
            case ItemType.HealthPotion: return "      回  復  3  0  %  最  大  生  命  !";
            case ItemType.ManaPotion: return "       回  復  3  0  %  最  大  魔  力  !";
            case ItemType.Crystal: return "       升  級  技  能  !";
            case ItemType.OldLetter: return "      看  起  來  平  凡  無  奇  的  卡  片  ,  拿  在  手  上  卻\n      感  到  一  絲  寒  意  .  .  .\n\n-  回  憶  道  具  -";
            case ItemType.Teddy: return "      你  愛  小  熊  嗎  ?  我  也  愛  小  熊  哦  !\n\n-  回  憶  道  具  -";
        }
    }
    public bool MemoryItem()
    {
        switch (itemType)
        {
            default:

            case ItemType.ManaPotion:
            case ItemType.Crystal:
            case ItemType.HealthPotion:
                return false;
            case ItemType.OldLetter:
            case ItemType.Teddy:
                return true;
        }
    }
    public bool GetMissionComplete()
    {
        switch (itemType)
        {
            default: return false;
            case ItemType.OldLetter: return MemoryItemManage.OldLetterMission;
            case ItemType.Teddy: return MemoryItemManage.TeddyMission;
        }
    }
    public string GetAdvamceInfo()
    {
        switch (itemType)
        {
            default: return "";
            case ItemType.Teddy: return "\n    解  鎖  獎  勵  :  獲  得  衝  刺  技  能";
            case ItemType.OldLetter: return "\n    醫  院  裡  ,  邂  逅  ?      C  l  e  a  r  !  ";
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
            case ItemType.Teddy:        return new Vector4(1, 0.98f, 0.83f, 1);
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
            case ItemType.Teddy:         return ItemAsset.Instance.Teddy3D;
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
            case ItemType.Teddy:
                return false;
        }
    }

}
