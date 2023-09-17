using UnityEngine;
using TMPro;

public class GemDisplay : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _gemDisplay;

    private void OnEnable()
    {
        _gemDisplay.text = _player.Gems.ToString();
        _player.GemChanged += OnGemsChanged;
    }

    private void OnDisable()
    {
        _player.GemChanged -= OnGemsChanged;
    }

    private void OnGemsChanged(int gems)
    {
        _gemDisplay.text = gems.ToString();
    }
}
