using UnityEngine;

public class WayGenerator : MonoBehaviour
{
    [SerializeField]
    private Plate _gameObject;

    private void Start()
    {
        Transform[] children = transform.GetComponentsInChildren<Transform>();
        for (int i = 1; i < children.Length; i++)
        {
            var plate = Instantiate(_gameObject, children[i].position, Quaternion.identity);
            plate.PlateNum = i;
            Destroy(children[i].gameObject);
        }
    }
}
