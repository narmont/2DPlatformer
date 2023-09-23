using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _checkGround;
    [SerializeField] private LayerMask _layerMask;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;

    private readonly int _isJumping = Animator.StringToHash("IsJumping");
    private readonly int _moving = Animator.StringToHash("Moving");

    private bool _isGrounded = false;
    private float _checkGroundRadius = 0.1f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();

        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    private void FixedUpdate()
    {
        JumpAnimationOfGround();
    }

    private void Jump()
    {
        _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
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

        _animator.SetFloat(_moving, Mathf.Abs(horizontalMove.x));
    }

    private void JumpAnimationOfGround()
    {
        int quantitySprites = 1;

        Collider2D[] collider = Physics2D.OverlapCircleAll(_checkGround.transform.position, _checkGroundRadius);
        _isGrounded = collider.Length > quantitySprites;

        _animator.SetBool(_isJumping, false);

        if (_isGrounded == false)
            _animator.SetBool(_isJumping, true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_checkGround.position, _checkGroundRadius);
    }
}