using Sounds;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PlayingCube
{
    [RequireComponent(typeof(RandomSoundPlayer))]
    public class CubeGun : MonoBehaviour
    {
        [SerializeField] 
        private float _maxForce;
        [SerializeField] 
        private float _minForce;
        [SerializeField] 
        private float _rotationForce;
        [SerializeField] 
        private Transform[] _cubeSpawnPoints;
        [SerializeField] 
        private GameObject _cubePrefab;
        private GameObject _cube;
        private RandomSoundPlayer _soundPlayer;
        private bool _isCanShoot = true;
   
        private void Start()
        {
            _soundPlayer = GetComponent<RandomSoundPlayer>();
            GlobalEventManager.OnAddingStepActive += SetCanShootTrue;
            GlobalEventManager.OnPlayerStop += SetCanShootTrue;
            GlobalEventManager.OnCubeTouchNegativeZone += SetCanShootTrue;
            GlobalEventManager.OnAllPlayersFinished += DestroyGun;
        }

        private void Update()
        {
            if (!(Input.GetAxis("ThrowCube") == 1 && _isCanShoot)) return;
            var spawnPoint = _cubeSpawnPoints[Random.Range(0, _cubeSpawnPoints.Length)];
            SpawnCube(spawnPoint);
            PushCube(spawnPoint.forward);
        }

        private void SpawnCube(Transform spawnPoint)
        {
            if (!_cube.IsUnityNull()) Destroy(_cube);
            _isCanShoot = false;
            _cube = Instantiate(_cubePrefab, spawnPoint.position, spawnPoint.rotation);
        }

        private void PushCube(Vector3 direction)
        {
            _soundPlayer.PlayRandomSound();
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
            GlobalEventManager.OnCubeTouchNegativeZone -= SetCanShootTrue;
            GlobalEventManager.OnAllPlayersFinished -= DestroyGun;
            Destroy(gameObject);
        }

    }
}
