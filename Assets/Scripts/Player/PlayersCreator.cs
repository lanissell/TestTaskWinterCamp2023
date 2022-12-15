using System.Collections.Generic;
using Plates;
using UnityEngine;
namespace Player
{
    public class PlayersCreator : MonoBehaviour
    {
        [HideInInspector]
        public List<PlayerStats> Players;
        [SerializeField]
        private Plate _startPlate;
        [SerializeField]
        private PlayerStats _playerPrefab;
        [SerializeField]
        private int _playersCount;
        [SerializeField]
        private Color[] _newPlayerColors;

        private void Start()
        {
            Players = new List<PlayerStats>();
            CreatePlayers();
            Players[0].CanPlay = true;
        }

        private void CreatePlayers()
        {
            int colorIndex = 0;
            for (int i = 0; i < _playersCount; i++)
            {
                Transform emptyPoint = _startPlate.GetEmptyPosition();
                PlayerStats newPlayer = Instantiate(_playerPrefab, emptyPoint.position, 
                    Quaternion.identity);
                newPlayer.transform.parent = emptyPoint;
                if (newPlayer.TryGetComponent(out Renderer playerRenderer)) SetColor(ref colorIndex, playerRenderer);
                Players.Add(newPlayer);
            }
        }

        private void SetColor(ref int colorIndex, Renderer playerRenderer)
        {
            playerRenderer.material.color = _newPlayerColors[colorIndex];
            colorIndex ++;
            if (colorIndex > _newPlayerColors.Length - 1) colorIndex = 0;
        }

    }
}
