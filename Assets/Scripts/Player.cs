using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool CanPlay = true;

    [SerializeField]
    private float _distanceThreshold;

    private int _currentPositionIndex = 0;
    
    private WayGenerator _generator;

    private Transform _transform;
    
    private void Start()
    {
        _transform = transform;
        _generator = FindObjectOfType<WayGenerator>();
        GlobalEventManager.OnPlayerMovementStart += StartMoveCoroutine;
    }
    
    private void StartMoveCoroutine(int plateCount)
    {
        if (!CanPlay) return;
        StartCoroutine(MovePlayerOnWay(plateCount));
    }

    private IEnumerator MovePlayerOnWay(int plateCount)
    {
        Transform target = _transform;
        _transform.parent = null;
        for (int i = 1; i <= Mathf.Abs(plateCount); i++)
        {
            int positionIndex = ValidatePositionIndex(_currentPositionIndex + 
                i * (plateCount / Mathf.Abs(plateCount)));
            target = _generator.Plates[positionIndex].GetEmptyPosition();
            yield return MovePlayerToTarget(target.position);
        }
        _transform.parent = target;
        _currentPositionIndex = ValidatePositionIndex(_currentPositionIndex + plateCount);
        if (_generator.Plates[_currentPositionIndex].PlateHaveEffect()) yield break;
        GlobalEventManager.SendOnPlayerStop();
    }

    private int ValidatePositionIndex(int index)
    {
        if (index > _generator.Plates.Count - 1) return _generator.Plates.Count - 1;
        if (index < 0) return 0;
        return index;
    }
    
    private IEnumerator MovePlayerToTarget(Vector3 targetPosition)
    {
        Vector3 startPosition = _transform.position;
        float startTime = Time.time;
        while (Vector3.Distance(_transform.position, targetPosition) > _distanceThreshold)
        {
            _transform.position = Vector3.Lerp(startPosition, targetPosition, 
                Time.time - startTime);
            yield return new WaitForEndOfFrame();
        }
    }

}
