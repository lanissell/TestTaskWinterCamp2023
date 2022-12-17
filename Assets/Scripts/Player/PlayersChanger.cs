using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayersCreator))]
    public class PlayersChanger : MonoBehaviour
    {
        private PlayersCreator _playersCreator;
        private int _currentPlayerIndex = 0;
        
        private void Start()
        {
            _playersCreator = GetComponent<PlayersCreator>();
            GlobalEventManager.OnPlayerStop += ChangeCurrentPlayer;
        }

        private void ChangeCurrentPlayer()
        {
            var players = _playersCreator.Players;
            var currentPlayer = players[_currentPlayerIndex];
            currentPlayer.AddMovesCount();
            currentPlayer.CanPlay = false;
            _currentPlayerIndex++;
            if (_currentPlayerIndex > players.Count - 1) _currentPlayerIndex = 0;
            players[_currentPlayerIndex].CanPlay = true;
        }
    }
}
