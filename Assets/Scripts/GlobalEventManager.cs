using System;

public static class GlobalEventManager
{
        public static event Action<int> OnPlayerMovementStart;
        public static event Action OnPlayerStop;
        public static event Action OnAddingStepActive;
        public static event Action OnMovingBackActive;

        public static void SendOnPlayerMovementStart(int stepsCount) => 
                OnPlayerMovementStart?.Invoke(stepsCount);

        public static void SendOnPlayerStop() => 
                OnPlayerStop?.Invoke();
        
        public static void SendOnAddingStepActive() => 
                OnAddingStepActive?.Invoke();
        
        public static void SendOnMovingBackActive() => 
                OnMovingBackActive?.Invoke();
}