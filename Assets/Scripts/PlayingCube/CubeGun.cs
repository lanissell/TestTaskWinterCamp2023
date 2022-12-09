using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PlayingCube
{
    public class CubeGun : MonoBehaviour
    {
        [SerializeField] private float _maxForce;
        [SerializeField] private float _minForce;
        [SerializeField] private Transform _cubeSpawnPoint;
        [SerializeField] private GameObject _cubePrefab;
        private GameObject _cube;
    
        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            if (!_cube.IsUnityNull()) Destroy(_cube.gameObject);  
            _cube = Instantiate(_cubePrefab, _cubeSpawnPoint.position, _cubeSpawnPoint.rotation);
            PushCube();
        }

        private void PushCube()
        {
            if (!_cube.TryGetComponent(out Rigidbody cubeRigidbody)) return;
            float force = Random.Range(_minForce, _maxForce);
            cubeRigidbody.AddForce(_cubeSpawnPoint.forward * force, ForceMode.VelocityChange);
        }
    
    }
}
