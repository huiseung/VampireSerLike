using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Spawner : MonoBehaviour
{
    private float _timer;
    private int _level;
    [SerializeField] private SpawnData[] _spawnDatas;
    [SerializeField] private Transform[] _points;


    private void Awake()
    {
        _points = GetComponentsInChildren<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _level = Mathf.Min(_spawnDatas.Length-1,  Mathf.FloorToInt(GameManager.Instance.GameTime / 10f));
        _timer += Time.deltaTime;
        if(_timer > _spawnDatas[_level].SpawnTime)
        {
            _timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.Instance.Pool.Get(0);
        enemy.transform.position = _points[UnityEngine.Random.Range(0, _points.Length)].position;
        enemy.GetComponent<Enemy>().Init(_spawnDatas[_level]);
    }
}


[System.Serializable]
public class SpawnData
{
    public int SpriteType;
    public float SpawnTime;
    public int Health;
    public float Speed;
}