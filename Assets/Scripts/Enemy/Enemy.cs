using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private Player _target;

    private void OnCollisionEnter2D(Collision2D collision)
    {
       int push = 2;

       _target.ApplyDamage(_damage);
       
       if (collision.gameObject.TryGetComponent<Player>(out Player player))
       {
            collision.rigidbody.AddForce(-transform.right * push, ForceMode2D.Impulse);
       }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
        {          
            Destroy(this.gameObject);
        }
    }
}
