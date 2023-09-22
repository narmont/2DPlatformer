using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;

    public event UnityAction<int> GemChanged;
    public event UnityAction<int> HealthChange;
    public event UnityAction Died;

    public int Gems { get; private set; }

    private void Start()
    {
        HealthChange?.Invoke(_health);
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;

        HealthChange?.Invoke(_health);

        if (_health < 0)
            Die();
    }

    public void AddGems(int gem)
    {
        Gems += gem;
        GemChanged?.Invoke(Gems);
    }

    private void Die()
    {
        Died?.Invoke();
    }
}
