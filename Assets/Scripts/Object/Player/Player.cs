using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 InputVector;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Animator _animator;
    private NearScanner _nearScanner;

    [SerializeField] private float _speed;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _nearScanner = GetComponent<NearScanner>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        Vector2 nextVector = InputVector * _speed * Time.fixedDeltaTime;
        _rb.MovePosition(_rb.position + nextVector);
    }

    void OnMove(InputValue inputValue)
    {
        InputVector = inputValue.Get<Vector2>();
    }

    void LateUpdate()
    {
        if (InputVector.x != 0)
        {
            _sr.flipX = InputVector.x < 0;
        }
        _animator.SetFloat("Speed", InputVector.magnitude);
    }

    public Transform GetNearestTarget()
    {
        return _nearScanner.NearsTarget;
    }

    public void PlusSpeed(float Rate)
    {
        _speed = _speed + _speed*Rate;
    }
}
