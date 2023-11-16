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
        Hari,
        Invitation,
        Candy1,
        Candy3,
        PeaCandy,
        HamburgerCandy,
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
            case ItemType.Teddy:        return ItemAsset.Instance.Teddy;
            case ItemType.Hari:         return ItemAsset.Instance.Hari;
            case ItemType.Invitation:   return ItemAsset.Instance.Invitation;
            case ItemType.Candy1:       return ItemAsset.Instance.Candy1;
            case ItemType.Candy3:       return ItemAsset.Instance.Candy3;
            case ItemType.PeaCandy:     return ItemAsset.Instance.PeaCandy;
            case ItemType.HamburgerCandy: return ItemAsset.Instance.HamburgerCandy;
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
            case ItemType.Hari:         return "針筒";
            case ItemType.Invitation:   return "神秘邀請函";
            case ItemType.Candy1:       return "葡萄汽水糖";
            case ItemType.Candy3:       return "草莓牛奶糖";
            case ItemType.PeaCandy:     return "雷根糖";
            case ItemType.HamburgerCandy: return "漢堡糖";
        }
    }
    public GameObject Get3DMemoryObject()
    {
        switch (itemType)
        {
            default:
            case ItemType.OldLetter: return ItemAsset.Instance.MemoryOldLetter3D;
            case ItemType.Teddy: return ItemAsset.Instance.MemoryTeddy3D;
            case ItemType.Hari: return ItemAsset.Instance.MemoryHari3D;
            case ItemType.Invitation: return ItemAsset.Instance.MemoryInvitation3D;
            case ItemType.Candy1: return ItemAsset.Instance.MemoryCandy13D;
            case ItemType.Candy3: return ItemAsset.Instance.MemoryCandy33D;
            case ItemType.PeaCandy: return ItemAsset.Instance.MemoryPeaCandy3D;
            case ItemType.HamburgerCandy: return ItemAsset.Instance.MemoryHamburgerCandy3D;
        }
    }
    public string Get3DMemoryObjectName()
    {
        switch (itemType)
        {
            default: return "";
            case ItemType.OldLetter: return "InvitationCard";
            case ItemType.Teddy:     return "Teddy";
            case ItemType.Hari:     return "Hari";
            case ItemType.Invitation: return "Invitation";
            case ItemType.Candy1: return "Candy1";
            case ItemType.Candy3: return "Candy3";
            case ItemType.PeaCandy: return "PeaCandy";
            case ItemType.HamburgerCandy: return "HamburgerCandy";
        }
    }
    public string GetInfo()
    {
        switch (itemType)
        {
            default:
            case ItemType.HealthPotion:     return "      回  復  3  0  %  最  大  生  命  !  (  短  時  間  內  使  用\n      將  降  低  回  復  效  果  )";
            case ItemType.ManaPotion:       return "      回  復  3  0  %  最  大  魔  力  !";
            case ItemType.Crystal:          return "      升  級  技  能  !";
            case ItemType.OldLetter:        return "      看  起  來  平  凡  無  奇  的  卡  片  ,   拿  在  手  上  卻\n      感  到  一  絲  寒  意  .  .  .\n\n-  回  憶  道  具  -";
            case ItemType.Teddy:            return "      你  愛  小  熊  嗎  ?   我  也  愛  小  熊  哦  !\n\n-  回  憶  道  具  -";
            case ItemType.Hari:             return "      看  起  來  有  毒  的  針  筒  ,   但  其  實  是  藍  莓  口  味  的  ?\n\n-  回  憶  道  具  -";
            case ItemType.Invitation:       return "      你  怎  麼  不  理  我  了  呢  ?   之  前  明  明  是  夢  裡\n      的  快  樂  玩  伴  呀  .  .  .\n\n-  回  憶  道  具  -";
            case ItemType.Candy1:           return "      欸  !   那  條  河  川  會  不  會  也  是  汽  水  口  味  的\n      呢  ?\n\n-  回  憶  道  具  -";
            case ItemType.Candy3:           return "      古  早  味  的  草  莓  牛  奶  糖  ,   我  最  喜  歡  配  布\n      丁  一  起  吃  了  !\n\n-  回  憶  道  具  -";
            case ItemType.PeaCandy:         return "      會  留  幾  顆  埋  在  家  的  後  院  ,   相  信  自  己  能\n      像  傑  克  一  樣  種  出  巨  樹\n\n-  回  憶  道  具  -";
            case ItemType.HamburgerCandy:   return "      很  喜  歡  把  它  撕  開  一  層  一  層  的  品  嘗\n\n-  回  憶  道  具  -";
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
            case ItemType.Hari:
            case ItemType.Invitation:
            case ItemType.Candy1: 
            case ItemType.Candy3: 
            case ItemType.PeaCandy: 
            case ItemType.HamburgerCandy:
                return true;
        }
    }
    public bool GetMissionComplete()
    {
        switch (itemType)
        {
            default: return false;
            case ItemType.OldLetter:   return MemoryItemManage.OldLetterMission;
            case ItemType.Teddy:       return MemoryItemManage.TeddyMission;
            case ItemType.Hari:        return MemoryItemManage.HariMission;
            case ItemType.Invitation:  return MemoryItemManage.InvitationMission;
            case ItemType.Candy1:      return MemoryItemManage.Candy1Mission;
            case ItemType.Candy3:      return MemoryItemManage.Candy3Mission;
            case ItemType.PeaCandy:    return MemoryItemManage.PeaCandyMission;
            case ItemType.HamburgerCandy: return MemoryItemManage.HamburgerCandyMission;
        }
    }
    public string GetAdvamceInfo()
    {
        switch (itemType)
        {
            default: return "";
            case ItemType.Teddy:          return "\n    解  鎖  獎  勵  :  獲  得  衝  刺  技  能";
            case ItemType.OldLetter:      return "\n    醫  院  裡  ,  邂  逅  ?      C  l  e  a  r  !  ";
            case ItemType.Hari:           return "\n    解  鎖  獎  勵  :  飛  升  後  射  出  爆  炸  能  量  球";
            case ItemType.Invitation:     return "\n    我  的  城  堡  夢  !      C  l  e  a  r  !  ";
            case ItemType.Candy1:         return "\n    解  鎖  獎  勵  :  增  加  2  0  %  功  速  和  1  0  %  跑  速";
            case ItemType.Candy3:         return "\n    解  鎖  獎  勵  :  格  檔  1  0  點  來  自  B  O  S  S  的  傷  害";
            case ItemType.PeaCandy:       return "\n    解  鎖  獎  勵  :  過  載  持  續  時  間  增  加  五  秒";
            case ItemType.HamburgerCandy: return "      解  鎖  獎  勵  :  攻  擊  命  中  回  復  1  %  已  損  失  血\n    量  和  魔  力";
        }
    }
    public Vector4 GetNameColor()
    {
        switch (itemType)
        {
            default: return new Vector4(1, 0.98f, 0.83f, 1);
            case ItemType.HealthPotion: return new Vector4(0.7f,1f,0.7f,1);
            case ItemType.ManaPotion:   return new Vector4(0.75f, 0.97f, 1f, 1);
            case ItemType.Crystal:      return new Vector4(1, 0.72f, 0.75f, 1);
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
            case ItemType.Hari:          return ItemAsset.Instance.Hari3D;
            case ItemType.Invitation:    return ItemAsset.Instance.Invitation3D;
            case ItemType.Candy1:        return ItemAsset.Instance.Candy13D;
            case ItemType.Candy3:        return ItemAsset.Instance.Candy33D;
            case ItemType.PeaCandy:      return ItemAsset.Instance.PeaCandy3D;
            case ItemType.HamburgerCandy:return ItemAsset.Instance.HamburgerCandy3D;
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
            case ItemType.Candy1:
            case ItemType.Candy3:
            case ItemType.PeaCandy:
            case ItemType.HamburgerCandy:
            case ItemType.OldLetter:
            case ItemType.Teddy:
            case ItemType.Hari:
            case ItemType.Invitation:
                return true;
        }
    }

}
