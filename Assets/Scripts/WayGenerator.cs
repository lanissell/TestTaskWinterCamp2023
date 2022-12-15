using System.Collections.Generic;
using Plates;
using UnityEngine;

public class WayGenerator : MonoBehaviour
{
    public List<Plate> Plates;
    
    [Header("SimplePlate")]
    [SerializeField]
    private Plate _simplePlate;
    
    [Header("PlateAddingStep")]
    [SerializeField]
    private PlateAddingStep _plateAddingStep;
    [SerializeField]
    private float _plateAddingStepRate;
    
    [Header("PlateMovingBack")]
    [SerializeField]
    private PlateMovingBack _plateMovingBack;
    [SerializeField]
    private int _movingBackSteps;
    [SerializeField]
    private float _plateMovingBackRate;
    private Transform _transform;

    private void Awake()
    {
        GenerateWayByChildPositions();
        _transform = transform;
    }

    private void GenerateWayByChildPositions()
    {
        Transform[] children = transform.GetComponentsInChildren<Transform>();
        for (int i = 1; i < children.Length; i++)
        {
            int plateNum = i;
            Plate plate = Instantiate(ChosePlatePrefab(ref plateNum), 
                children[i].position, Quaternion.identity, _transform);
            plate.PlateNum = plateNum;
            Plates.Add(plate);
            Destroy(children[i].gameObject);
        }
    }

    private Plate ChosePlatePrefab(ref int plateNum)
    {
        Plate platePrefab = _simplePlate;
        if (plateNum % _plateAddingStepRate == 0)
        {
            platePrefab = _plateAddingStep;
        }
        else if (plateNum % _plateMovingBackRate == 0)
        {
            platePrefab = _plateMovingBack;
            plateNum = -_movingBackSteps;
        }
        return platePrefab;
    }
}
