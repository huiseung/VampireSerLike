using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{

    [Header("# Main Info")]
    public ItemType ItemType;
    public int ItemId;
    public string ItemName;
    public string ItemDesc;
    public Sprite ItemIcon;

    [Header("# Level Data")]
    public float BaseDamage;
    public int BaseCount;
    public float[] Damages;
    public int[] Counts;

    [Header("# Weapon")]
    public GameObject Projectile;
}
