using System.Collections.Generic;
using Plates;
using UnityEngine;

public class PlayersCreator : MonoBehaviour
{
    [HideInInspector]
    public List<Player> Players;
    [SerializeField]
    private Plate _startPlate;
    [SerializeField]
    private Player _playerPrefab;
    [SerializeField]
    private int _playersCount;
    [SerializeField]
    private Color[] _newPlayerColors;

    private void Start()
    {
        Players = new List<Player>();
        CreatePlayers();
        Players[0].CanPlay = true;
    }

    private void CreatePlayers()
    {
        int colorIndex = 0;
        for (int i = 0; i < _playersCount; i++)
        {
            Transform emptyPosition = _startPlate.GetEmptyPosition();
            Player newPlayer = Instantiate(_playerPrefab, emptyPosition.position, 
            Quaternion.identity);
            newPlayer.transform.parent = emptyPosition;
            if (newPlayer.TryGetComponent(out Renderer playeRenderer)) 
                playeRenderer.material.color = _newPlayerColors[colorIndex];
            Players.Add(newPlayer);
            colorIndex ++;
            if (colorIndex > _newPlayerColors.Length - 1) colorIndex = 0;
        }
    }

}
