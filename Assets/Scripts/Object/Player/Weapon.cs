using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int Id;
    public int PrefabId;
    public float Damage;
    public int Count;
    public float Speed;

    private float _timer;
    private Player _player;

    private void Awake()
    {
        _player = GameManager.Instance.Player;
    }

    // Update is called once per frame
    void Update()
    {
        switch(Id)
        {
            case 0:
                transform.Rotate(Vector3.back * Speed * Time.deltaTime);
                break;
            default:
                _timer += Time.deltaTime;
                if(_timer > Speed)
                {
                    _timer = 0f;
                    Fire();
                }
                break;
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.Damage = damage;
        this.Count += count;

        if(Id == 0)
        {
            Batch();
        }
        _player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void Init(ItemData itemData)
    {
        name = $"Weapon {itemData.ItemId}";
        transform.parent = _player.transform;
        transform.localPosition = Vector3.zero;

        Id = itemData.ItemId;
        Damage = itemData.BaseDamage;
        Count = itemData.BaseCount;
        for(int idx=0; idx < GameManager.Instance.Pool.GetPrefabsLength(); idx++)
        {
            if(itemData.Projectile == GameManager.Instance.Pool.GetPrefab(idx))
            {
                PrefabId = idx;
                break;
            }
        }

        switch (Id)
        {
            case 0:
                Speed = 150;
                Batch();
                break;
            default:
                Speed = 0.4f;
                break;
        }
        _player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void Batch()
    {
        for(int idx=0; idx < Count; idx++)
        {
            Transform bullet;
            
            if(idx < transform.childCount)
            {
                bullet = transform.GetChild(idx);
            }
            else
            {
                bullet = GameManager.Instance.Pool.Get(PrefabId).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVector = Vector3.forward * 360 * idx / Count;
            bullet.Rotate(rotVector);
            bullet.Translate(bullet.up * 1.5f, Space.World);

            bullet.GetComponent<Bullet>().Init(Damage, -1, Vector3.zero);
        }
    }

    void Fire()
    {
        if(_player.GetNearestTarget() == null)
        {
            return;
        }

        Vector3 targetPos = _player.GetNearestTarget().position;
        Vector3 dir = (targetPos  - transform.position).normalized;

        Transform bullet =  GameManager.Instance.Pool.Get(PrefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(Damage, Count, dir);
    }
}
