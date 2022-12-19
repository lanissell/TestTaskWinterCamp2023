using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayersCreator))]
    public class PlayersChanger : MonoBehaviour
    {
        private PlayersCreator _playersCreator;
        private int _currentPlayerIndex;

        private void Start()
        {
            _playersCreator = GetComponent<PlayersCreator>();
            GlobalEventManager.OnPlayerStop += ChangeCurrentPlayer;
            GlobalEventManager.OnPlayerFinished += RemovePlayerFromList;
        }

        private void ChangeCurrentPlayer()
        {
            if (_playersCreator.Players.Count == 0) return;
            DeactivatePlayer(_playersCreator.Players[GetCurrentPlayerIndex()]);
            _currentPlayerIndex++;
            ActivatePlayer(_playersCreator.Players[GetCurrentPlayerIndex()]);
        }

        private int GetCurrentPlayerIndex()
        {
            if (_currentPlayerIndex > _playersCreator.Players.Count - 1 
                || _currentPlayerIndex < 0) 
                _currentPlayerIndex = 0;
            return _currentPlayerIndex;
        }

        private void DeactivatePlayer(PlayerStats player)
        {
            player.AddMovesCount();
            player.CanPlay = false;
        }

        private void ActivatePlayer(PlayerStats player)
        {
            player.CanPlay = true;
            GlobalEventManager.SendOnPlayerChanged(player);
        }

        private void RemovePlayerFromList(PlayerStats playerStats)
        {
            var players = _playersCreator.Players;  
            players.Remove(playerStats);
            _currentPlayerIndex--;
            if (players.Count != 0) return;
            IsAllPlayersFinished();
        }
        
        private void IsAllPlayersFinished()
        {
            GlobalEventManager.OnPlayerStop -= ChangeCurrentPlayer;
            GlobalEventManager.OnPlayerFinished -= RemovePlayerFromList;
            GlobalEventManager.SendOnAllPlayersFinished();
        }
    }
}
