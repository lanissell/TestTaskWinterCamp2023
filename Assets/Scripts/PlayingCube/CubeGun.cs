using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
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
        [FormerlySerializedAs("_cubeSpawnPoint")] [SerializeField] 
        private Transform[] _cubeSpawnPoints;
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
            if (!((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && _isCanShoot)) return;
            if (!_cube.IsUnityNull()) Destroy(_cube.gameObject);
            _isCanShoot = false;
            var spawnPoint = _cubeSpawnPoints[Random.Range(0, _cubeSpawnPoints.Length)];
            _cube = Instantiate(_cubePrefab, spawnPoint.position, spawnPoint.rotation);
            PushCube(spawnPoint.forward);
        }

        private void PushCube(Vector3 direction)
        {
            if (!_cube.TryGetComponent(out Rigidbody cubeRigidbody)) return;
            float force = Random.Range(_minForce, _maxForce);
            cubeRigidbody.AddForce(direction * force, ForceMode.VelocityChange);
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
