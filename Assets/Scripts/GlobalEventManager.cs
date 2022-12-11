using System;

public static class GlobalEventManager
{
        public static event Action<int> OnPlayerMovementStart;
        public static event Action OnPlayerStop;
        
        public static void SendOnPlayerMovementStart(int stepsCount) => 
                OnPlayerMovementStart?.Invoke(stepsCount);

        public static void SendOnPlayerStop() => OnPlayerStop?.Invoke();
}