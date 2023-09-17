using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _damage;
    [SerializeField] private Transform _checkGround;
    [SerializeField] private Transform _checkWall;
    [SerializeField] private Player _target;
    [SerializeField] private LayerMask _layerMask;

    private Rigidbody2D _rigidbody;
    private bool _isGrounded = false;
    private bool _isWall = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (IsFacingRight())       
            _rigidbody.velocity = new Vector2(_moveSpeed, 0f);       
        else        
            _rigidbody.velocity = new Vector2(-_moveSpeed, 0f);        
    }

    private void FixedUpdate()
    {
        CheckWall();
        CheckGround();
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       _target.ApplyDamage(_damage);
       
       if (collision.gameObject.tag == "Player")
       {
            collision.rigidbody.AddForce(-transform.right * 2, ForceMode2D.Impulse);
       }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
        {          
            Destroy(this.gameObject);
           // player.Jump();
        }
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(_checkGround.transform.position, 0.1f, _layerMask);
        _isGrounded = collider.Length < 1;

        if (_isGrounded == false)
        {
            transform.localScale = new Vector2(-(Mathf.Sign(_rigidbody.velocity.x)), transform.localScale.y);
        }
    }

    private void CheckWall()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(_checkWall.transform.position, 0.1f, _layerMask);
        _isWall = collider.Length > 0;

        if (_isWall == true)
        {
            transform.localScale = new Vector2(-(Mathf.Sign(_rigidbody.velocity.x)), transform.localScale.y);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_checkGround.position, 0.1f);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_checkWall.position, 0.1f);
    }
}
