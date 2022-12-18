using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PlayingCube
{
    public class CubeGun : MonoBehaviour
    {
        [SerializeField] 
        private float _maxForce;
        [SerializeField] 
        private float _minForce;
        [SerializeField] 
        private float _rotationForce;
        [SerializeField] 
        private Transform _cubeSpawnPoint;
        [SerializeField] 
        private GameObject _cubePrefab;
        private bool _isCanShoot;
        private GameObject _cube;

        private void Start()
        {
            _isCanShoot = true;
            GlobalEventManager.OnAddingStepActive += SetCanShootTrue;
            GlobalEventManager.OnPlayerStop += SetCanShootTrue;
            GlobalEventManager.OnAllPlayersFinished += DestroyGun;
        }

        private void Update()
        {
            if (!(Input.GetMouseButtonDown(0) && _isCanShoot)) return;
            if (!_cube.IsUnityNull()) Destroy(_cube.gameObject);
            _isCanShoot = false;
            _cube = Instantiate(_cubePrefab, _cubeSpawnPoint.position, _cubeSpawnPoint.rotation);
            PushCube();
        }

        private void PushCube()
        {
            if (!_cube.TryGetComponent(out Rigidbody cubeRigidbody)) return;
            float force = Random.Range(_minForce, _maxForce);
            cubeRigidbody.AddForce(_cubeSpawnPoint.forward * force, ForceMode.VelocityChange);
            var rotationDirection = Vector3.back + Vector3.up;
            cubeRigidbody.AddTorque(rotationDirection * (_rotationForce * force));
        }

        private void SetCanShootTrue() => _isCanShoot = true;

        private void DestroyGun()
        {
            GlobalEventManager.OnAddingStepActive -= SetCanShootTrue;
            GlobalEventManager.OnPlayerStop -= SetCanShootTrue;
            GlobalEventManager.OnAllPlayersFinished -= DestroyGun;
            Destroy(gameObject);
        }

    }
}
