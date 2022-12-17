using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayingCube
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayingCube : MonoBehaviour
    {
        [SerializeField]
        private float _checkCubeVelocityInterval;
        [SerializeField]
        private float _velocityThreshold;
        [SerializeField]
        private List<CubeSide> _cubeSides;
        private Rigidbody _rigidbody;
        private Transform _transform;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transform = transform;
        }

        private IEnumerator WaitCubeStop()
        {
            while (_rigidbody.velocity.magnitude > _velocityThreshold)
            {
                yield return new WaitForSeconds(_checkCubeVelocityInterval);
            }
            CheckCubeSide();
        }

        private void CheckCubeSide()
        {
            foreach (var side in _cubeSides)
            {
                RaycastHit hit;
                if (!Physics.Raycast(_transform.position, 
                    side.transform.forward, out hit)) continue;
                if (!hit.transform.TryGetComponent(out CubeSidesCheckPoint _)) continue;
                GlobalEventManager.SendOnPlayerMovementStart(side.SideNumber);
                return;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Land _)) return;
            StartCoroutine(WaitCubeStop());
        }
    }
}
