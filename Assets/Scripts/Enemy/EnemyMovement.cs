using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _checkGround;
    [SerializeField] private Transform _checkWall;
    [SerializeField] private LayerMask _layerMask;

    private Rigidbody2D _rigidbody;
    private bool _isGrounded = false;
    private bool _isWall = false;

    private float _checkWallRadius = 0.1f;
    private float _checkGroundRadius = 0.1f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        TurnAround();
    }

    private void FixedUpdate()
    {
        CheckWall();
        CheckGround();
    }

    private void TurnAround()
    {
        if (IsFacingRight())
            _rigidbody.velocity = new Vector2(_moveSpeed, 0f);
        else
            _rigidbody.velocity = new Vector2(-_moveSpeed, 0f);
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    private void CheckGround()
    {
        int quantitySprites = 1;

        Collider2D[] collider = Physics2D.OverlapCircleAll(_checkGround.transform.position, _checkGroundRadius, _layerMask);
        _isGrounded = collider.Length < quantitySprites;

        if (_isGrounded == false)
        {
            transform.localScale = new Vector2(-(Mathf.Sign(_rigidbody.velocity.x)), transform.localScale.y);
        }
    }

    private void CheckWall()
    {
        int quantitySprites = 0;

        Collider2D[] collider = Physics2D.OverlapCircleAll(_checkWall.transform.position, _checkWallRadius, _layerMask);
        _isWall = collider.Length > quantitySprites;

        if (_isWall == true)
        {
            transform.localScale = new Vector2(-(Mathf.Sign(_rigidbody.velocity.x)), transform.localScale.y);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_checkGround.position, _checkGroundRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_checkWall.position, _checkWallRadius);
    }
}
