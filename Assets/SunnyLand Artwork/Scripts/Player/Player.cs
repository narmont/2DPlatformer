using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _checkGround;
    [SerializeField] private LayerMask _layerMask;

    public event UnityAction<int> GemChanged;
    public event UnityAction<int> HealthChange;
    public event UnityAction Died;

    public int Gems { get; private set; }

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;
    private bool _isGrounded = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        HealthChange?.Invoke(_health);
    }

    private void Update()
    {
        Move();

        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;

        HealthChange?.Invoke(_health);

        if (_health < 0)
            Die();
    }

    public void Jump()
    {
        _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
    }

    public void AddGems(int gem)
    {
        Gems += gem;
        GemChanged?.Invoke(Gems);
    }

    private void Move()
    {
        Vector3 horizontalMove = transform.right * Input.GetAxisRaw("Horizontal");

        if (horizontalMove.x > 0)
        {
            _sprite.flipX = false;
        }
        else if (horizontalMove.x < 0)
        {
            _sprite.flipX = true;
        }

        transform.position = Vector3.MoveTowards(transform.position, transform.position + horizontalMove, _speed * Time.deltaTime);

        _animator.SetFloat("HorizontalMove", Mathf.Abs(horizontalMove.x));
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(_checkGround.transform.position, 0.1f);
        _isGrounded = collider.Length > 1;

        if (_isGrounded == true)
            _animator.SetBool("IsJumping", false);
        else
            _animator.SetBool("IsJumping", true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_checkGround.position, 0.1f);
    }

    private void Die()
    {
        Died?.Invoke();
    }
}
