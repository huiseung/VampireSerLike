using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage;
    public float Per;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>(); 
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        this.Damage = damage;
        this.Per = per;

        if(Per > -1)
        {
            _rb.velocity = dir * 15f;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Per == -1 || !collision.CompareTag("Enemy"))
        {
            return;
        }

        Per -= 1;

        if(Per == -1)
        {
            _rb.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
