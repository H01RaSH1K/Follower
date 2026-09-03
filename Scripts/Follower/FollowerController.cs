using UnityEngine;

class FollowerController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _minDesiredDistance;
    [SerializeField] private float _maxDesiredDistance;
    [SerializeField] private Walker _walker;

    private float _minDesiredDistanceSqr;
    private float _maxDesiredDistanceSqr;

    private void Awake()
    {
        _minDesiredDistanceSqr = _minDesiredDistance * _minDesiredDistance;
        _maxDesiredDistanceSqr = _maxDesiredDistance * _maxDesiredDistance;
    }

    private void Update()
    {
        Vector3 directionToTarget = _target.position - transform.position;
        Vector2 walkDirectionToTarget = new Vector2(directionToTarget.x, directionToTarget.z);
        float directionToTargetSqrMagnitude = directionToTarget.sqrMagnitude;

        if (directionToTargetSqrMagnitude > _maxDesiredDistanceSqr)
            _walker.SetWalkDirection(walkDirectionToTarget);
        else if (directionToTargetSqrMagnitude < _minDesiredDistanceSqr)
            _walker.SetWalkDirection(-walkDirectionToTarget);
        else
            _walker.StopWalk();
    }
}
