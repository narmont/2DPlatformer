using System.Collections;
using UnityEngine;

public class SpawnerGem : MonoBehaviour
{
    [SerializeField] private Gem _gem;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;

    private Coroutine _coroutine;

    private void Start()
    {
        InstantiateGem();
    }

    private void InstantiateGem()
    {
        Gem gem = Instantiate(_gem, _spawnPoint.position, Quaternion.identity);

        gem.Destroyed += OnGemSpawn;
    }

    private void OnGemSpawn(Gem gem)
    {
        gem.Destroyed -= OnGemSpawn;

        _player.AddGems(gem.Reward);

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        Restart();
    }

    private void Restart()
    {
        _coroutine = StartCoroutine(ResetGem());
    }

    private IEnumerator ResetGem()
    {
        float delay = 5f;

        WaitForSeconds wait = new WaitForSeconds(delay);
        
        yield return wait;

        InstantiateGem();  
    }
}
