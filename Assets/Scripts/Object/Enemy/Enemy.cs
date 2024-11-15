using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    [SerializeField] private RuntimeAnimatorController[] _ac; 
    [SerializeField] private Rigidbody2D _target;
    private WaitForFixedUpdate _wait;

    private Rigidbody2D _rb;
    private Collider2D _coll;
    private SpriteRenderer _sr;
    private Animator _anima;
    private bool _isLive;
    private float knockbackForce = 3f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _coll = GetComponent<Collider2D>();
        _sr = GetComponent<SpriteRenderer>();
        _anima = GetComponent<Animator>();
        _wait = new WaitForFixedUpdate();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!_isLive || _anima.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            return;
        }
        Vector2 dir = _target.position - _rb.position;
        Vector2 nextVector = dir.normalized * _speed * Time.fixedDeltaTime;
        _rb.MovePosition(_rb.position + nextVector);
        _rb.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!_isLive)
        {
            return;
        }
        _sr.flipX = _target.position.x < _rb.position.x;
    }

    private void OnEnable()
    {
        _target = GameManager.Instance.Player.GetComponent<Rigidbody2D>();
        _health = _maxHealth;

        _isLive = true;
        _coll.enabled = true;
        _rb.simulated = true;
        _sr.sortingOrder = 2;
    }

    public void Init(SpawnData data)
    {
        _anima.runtimeAnimatorController = _ac[data.SpriteType];
        _speed = data.Speed;
        _health = data.Health;
        _maxHealth = data.Health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !_isLive)
        {
            return;
        }
        _health -= collision.GetComponent<Bullet>().Damage;
        StartCoroutine(KnockBack());

        if(_health > 0)
        {
            _anima.SetTrigger("Hit");

        }
        else
        {
            _isLive = false;
            _coll.enabled = false;
            _rb.simulated = false;
            _sr.sortingOrder = 1;
            _anima.SetBool("Dead", true);
            GameManager.Instance.PlusKill();
            GameManager.Instance.GetExp();
        }
    }

    public void Dead()
    {
        gameObject.SetActive(false);
    }

    IEnumerator KnockBack()
    {
        yield return _wait; // 1 프레임 후 응답
        Vector3 playerPos = GameManager.Instance.Player.transform.position;
        Vector3 dir = (transform.position - playerPos).normalized;
        _rb.AddForce(dir* knockbackForce, ForceMode2D.Impulse);
    }

}
