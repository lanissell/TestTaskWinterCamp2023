using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            List<(CubeSide, float)> angles = new List<(CubeSide, float)>();
            foreach (var side in _cubeSides)
            {
                Vector3 sideForward = side.transform.forward;
                if (!Physics.Raycast(_transform.position, 
                    sideForward, out RaycastHit hit)) continue;
                if (!hit.transform.TryGetComponent(out CubeSidesCheckPoint _)) continue;
                angles.Add((side, Vector3.Angle(-sideForward, hit.normal)));
            }
            var sideWithMinAngle = angles.OrderBy(a => a.Item2).ToArray()[0].Item1;
            GlobalEventManager.SendOnPlayerMovementStart(sideWithMinAngle.SideNumber);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Land _)) return;
            StartCoroutine(WaitCubeStop());
        }

    }
}
