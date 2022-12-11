using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayersCreator))]
public class PlayersChanger : MonoBehaviour
{
    private PlayersCreator _playersCreator;
    private int _currentPlayerIndex = 0;

    private void Start()
    {
        GlobalEventManager.OnPlayerStop += ChangeCurrentPlayer;
        _playersCreator = GetComponent<PlayersCreator>();
    }

    private void ChangeCurrentPlayer()
    {
        var players = _playersCreator.Players;
        players[_currentPlayerIndex].CanPlay = false;
        _currentPlayerIndex++;
        if (_currentPlayerIndex > players.Count - 1) _currentPlayerIndex = 0;
        players[_currentPlayerIndex].CanPlay = true;
    }
}
