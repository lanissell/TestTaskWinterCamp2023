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
        public Color Color;

        private void Awake()
        {
            GlobalEventManager.OnAddingStepActive += AddBonusCount;
            GlobalEventManager.OnMovingBackActive += AddFineCount;
            GlobalEventManager.OnPlayerFinished += UnsubscribeOnEvents;
        }
        
        public void AddMovesCount() => MovesCount++;

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

        private void UnsubscribeOnEvents(PlayerStats stats)
        {
            if (stats != this) return;
            GlobalEventManager.OnAddingStepActive -= AddBonusCount;
            GlobalEventManager.OnMovingBackActive -= AddFineCount;
            GlobalEventManager.OnPlayerFinished -= UnsubscribeOnEvents;
        }

    }
}
