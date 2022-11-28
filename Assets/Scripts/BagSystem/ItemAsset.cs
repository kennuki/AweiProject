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
    public Sprite Kumo;
    public Sprite Teddy;

    public Transform ItemWorld;

    public GameObject HealthPotion3D;
    public GameObject ManaPotion3D;
    public GameObject Crystal3D;
    public GameObject OldLetter3D;
    public GameObject Teddy3D;

    public string HealthPotionText;
    public string ManaPotionText;
    public string CrystalText;
    public string OldLetterText;
    public string TeddyText;

    public string HealthPotionInfo;
    public string ManaPotionInfo;
    public string CrystalInfo;
    public string OldLetterInfo;
    public string TeddyInfo;

    public GameObject MemoryOldLetter3D;
    public GameObject MemoryTeddy3D;

    public GameObject MemoryOldLetterRightPlace;
    public GameObject MemoryTeddyRightPlace;

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
