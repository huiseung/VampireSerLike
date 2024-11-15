using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData ItemData;
    public int Level;
    public Weapon Weapon;
    private Image _icon;
    private Text _levelText;
    public Gear Gear;


    private void Awake()
    {
        _icon = GetComponentsInChildren<Image>()[1];
        _icon.sprite = ItemData.ItemIcon;
        _levelText = GetComponentsInChildren<Text>()[0];
    }

    private void LateUpdate()
    {
        _levelText.text = $"Lv. {Level+1}";
    }

    public void OnClick()
    {
        switch(ItemData.ItemType)
        {
            case ItemType.Melee:
            case ItemType.Range:
                if(Level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    Weapon = newWeapon.AddComponent<Weapon>();
                    Weapon.Init(ItemData);
                }
                else
                {
                    float nextDamage = ItemData.BaseDamage;
                    int nextCount = 0;
                    nextDamage += ItemData.BaseDamage * ItemData.Damages[Level];
                    nextCount += ItemData.Counts[Level];
                    Weapon.LevelUp(nextDamage, nextCount);
                }
                Level += 1;
                break;
            case ItemType.Glove:
            case ItemType.Shoe:
                if(Level == 0)
                {
                    GameObject newGear = new GameObject();
                    Gear = newGear.AddComponent<Gear>();
                    Gear.Init(ItemData);
                }
                else
                {
                    float nextRate = ItemData.Damages[Level];
                    Gear.LevelUp(nextRate);
                }
                Level += 1;
                break;
            case ItemType.Heal:
                GameManager.Instance.Health = GameManager.Instance.MaxHealth;
                break;
        }
        
        if(Level == ItemData.Damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
