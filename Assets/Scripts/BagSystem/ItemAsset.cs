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

    public Transform ItemWorld;

    public GameObject HealthPotion3D;
    public GameObject ManaPotion3D;
    public GameObject Crystal3D;
    public GameObject OldLetter3D;

    public string HealthPotionText;
    public string ManaPotionText;
    public string CrystalText;
    public string OldLetterText;
}
