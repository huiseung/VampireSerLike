using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _prefabs;
    private List<GameObject>[] _pools;

    private void Awake()
    {
        _pools = new List<GameObject>[_prefabs.Length];
        for (int idx=0; idx < _pools.Length; idx++)
        {
            _pools[idx] = new List<GameObject>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject Get(int idx)
    {
        GameObject go = null;
        foreach(GameObject item in _pools[idx])
        {
            if(!item.activeSelf)
            {
                go = item;
                go.SetActive(true);
                break;
            }
        }

        if (go == null)
        {
            go = Instantiate(_prefabs[idx], transform);
            _pools[idx].Add(go);
        }
        return go;
    }

    public int GetPrefabsLength()
    {
        return _prefabs.Length;
    }

    public GameObject GetPrefab(int idx)
    {
        return _prefabs[idx];
    }
}
