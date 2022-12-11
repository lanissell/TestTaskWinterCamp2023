using UnityEngine;

namespace Plates
{
    public class PositionOnPlate : MonoBehaviour
    {
        public Transform Transform;

        private void Awake()
        {
            Transform = transform;
        }
    }
}
