using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAsset : MonoBehaviour
{
    public static ItemAsset Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    public Sprite HealthPotion;
    public Sprite ManaPotion;
    public Sprite Crystal;
    public Sprite OldLetter;
    public Sprite Teddy;
    public Sprite Hari;
    public Sprite Invitation;
    public Sprite Candy1;
    public Sprite Candy3;
    public Sprite PeaCandy;
    public Sprite HamburgerCandy;

    public Transform ItemWorld;

    public GameObject HealthPotion3D;
    public GameObject ManaPotion3D;
    public GameObject Crystal3D;
    public GameObject OldLetter3D;
    public GameObject Teddy3D;
    public GameObject Hari3D;
    public GameObject Invitation3D;
    public GameObject Candy13D;
    public GameObject Candy33D;
    public GameObject PeaCandy3D;
    public GameObject HamburgerCandy3D;

    public string HealthPotionText;
    public string ManaPotionText;
    public string CrystalText;
    public string OldLetterText;
    public string TeddyText;
    public string HariText;
    public string InvitationText;
    public string Candy1Text;
    public string Candy3Text;
    public string PeaCandyText;
    public string HamburgerCandyText;

    public string HealthPotionInfo;
    public string ManaPotionInfo;
    public string CrystalInfo;
    public string OldLetterInfo;
    public string TeddyInfo;
    public string HariInfo;
    public string InvitationInfo;
    public string Candy1Info;
    public string Candy3Info;
    public string PeaCandyInfo;
    public string HamburgerCandyInfo;

    public GameObject MemoryOldLetter3D;
    public GameObject MemoryTeddy3D;
    public GameObject MemoryHari3D;
    public GameObject MemoryInvitation3D;
    public GameObject MemoryCandy13D;
    public GameObject MemoryCandy33D;
    public GameObject MemoryPeaCandy3D;
    public GameObject MemoryHamburgerCandy3D;

    public GameObject MemoryOldLetterRightPlace;
    public GameObject MemoryTeddyRightPlace;
    public GameObject MemoryHariRightPlace;
    public GameObject MemoryInvitationRightPlace;
    public GameObject MemoryCandy1RightPlace;
    public GameObject MemoryCandy3RightPlace;
    public GameObject MemoryPeaCandyRightPlace;
    public GameObject MemoryHamburgerCandyRightPlace;

    public GameObject DashMask1;
    public GameObject DashMask2;
    public GameObject KickMask1;
    public GameObject KickMask2;
    public GameObject SpeedMask1;
    public GameObject SpeedMask2;
    public GameObject ShootSkill1Mask1;
    public GameObject ShootSkill1Mask2;

    public GameObject UpgradeKick;
    public GameObject UpgradeAccelerate;
    public GameObject UpgradeSkill1;

    public GameObject KickIcon;
    public GameObject DashIcon;
    public GameObject Skill1Icon;
    public GameObject AccelerateIcon;
}
