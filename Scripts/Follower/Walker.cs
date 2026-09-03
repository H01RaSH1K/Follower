using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Walker : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _walkSmoothing = 0.05f;

    private Rigidbody _rigidbody;
    private Vector3 _walkDirection;
    private Vector3 _currentPlanarVelocity;
    private Vector3 _planarVelocitySmoothing;

    public Vector3 WalkDirection => _walkDirection;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        StopWalk();
    }

    public void SetWalkDirection(Vector2 direction)
    {
        _walkDirection = Vector3.ClampMagnitude(new Vector3(direction.x, 0, direction.y), 1f);
    }

    public void StopWalk()
    {
        _walkDirection = Vector3.zero;
    }

    private void FixedUpdate()
    {
        Vector3 targetVelocity = _walkDirection * _speed;

        _currentPlanarVelocity = Vector3.SmoothDamp(
            _currentPlanarVelocity,
            targetVelocity,
            ref _planarVelocitySmoothing,
            _walkSmoothing,
            Mathf.Infinity,
            Time.fixedDeltaTime);

        Vector3 velocity = new Vector3(_currentPlanarVelocity.x, _rigidbody.velocity.y, _currentPlanarVelocity.z);
        velocity.x = _currentPlanarVelocity.x;
        velocity.z = _currentPlanarVelocity.z;
        _rigidbody.velocity = velocity;
    }
}