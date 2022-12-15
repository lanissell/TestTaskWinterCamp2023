using UnityEngine;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {
        public bool CanPlay;
        public string Name;
        public int MovesCount = 0;
        public int BonusCount = 0;
        public int FineCount = 0;

        private void Awake()
        {
            GlobalEventManager.OnPlayerStop += AddMovesCount;
            GlobalEventManager.OnAddingStepActive += AddBonusCount;
            GlobalEventManager.OnMovingBackActive += AddFineCount;
        }

        private void AddMovesCount()
        {
            if (CanPlay) MovesCount++;
        }

        private void AddBonusCount()
        {
            if (!CanPlay) return;
            BonusCount++;
            MovesCount++;
        }

        private void AddFineCount()
        {
            if (CanPlay) FineCount++;
        } 

    }
}
