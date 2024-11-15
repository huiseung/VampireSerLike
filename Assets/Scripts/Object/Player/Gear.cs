using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemType Type;
    public float Rate;

    public void Init(ItemData data)
    {
        name = $"Gear {data.ItemId}";
        transform.parent = GameManager.Instance.Player.transform;
        transform.localPosition = Vector3.zero;

        Type = data.ItemType;
        Rate = data.Damages[0];
        ApplyGear();
    }

    public void LevelUp(float rate)
    {
        this.Rate = rate;
        ApplyGear();
    }

    void ApplyGear()
    {
        switch(Type)
        {
            case ItemType.Glove:
                RateUp();
                break;
            case ItemType.Shoe:
                SpeedUp();
                break;
        }
    }

    private void RateUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();
        foreach(Weapon weapon in weapons)
        {
            switch(weapon.Id)
            {
                case 0:
                    weapon.Speed = 150 + (150 * Rate);
                    break;
                default:
                    weapon.Speed = 0.5f * (1f-Rate);
                    break;
            }
        }
    }

    private void SpeedUp()
    {
        GameManager.Instance.Player.PlusSpeed(Rate);
    }
}
