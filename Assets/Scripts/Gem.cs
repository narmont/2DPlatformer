using UnityEngine;
using UnityEngine.Events;

public class Gem : MonoBehaviour
{
    [SerializeField] private int _reward;

    public int Reward => _reward;

    public event UnityAction<Gem> Destroyed;
    private CircleCollider2D _circleCollider;

    private void Start()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            Destroyed?.Invoke(this);
            Destroy(gameObject);
            _circleCollider.enabled = false;
        }

    }
}