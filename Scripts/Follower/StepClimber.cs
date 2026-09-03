using UnityEngine;

public class StepClimber : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Walker _walker;
    [SerializeField] private ObstacleScanner _lowerScanner;
    [SerializeField] private ObstacleScanner _upperScanner;
    [SerializeField] private float _climbSpeed = 4f;

    private void FixedUpdate()
    {
        if (_walker.WalkDirection.sqrMagnitude < float.Epsilon)
            return;

        transform.rotation = Quaternion.LookRotation(_walker.WalkDirection, Vector3.up);

        if (CanStepUp() == false)
            return;

        _rigidbody.position += Vector3.up * (_climbSpeed * Time.fixedDeltaTime);
    }

    private bool CanStepUp()
    {
        return _lowerScanner.HasObstacle() && _upperScanner.HasObstacle() == false;
    }
}