using UnityEngine;

public abstract class ObstacleScanner : MonoBehaviour
{
    [SerializeField] private float _scanDistance = 0.1f;
    [SerializeField] private Vector3 _scanDirection = Vector3.forward;
    [SerializeField] private LayerMask _layerMask = ~0;
    [SerializeField] private QueryTriggerInteraction _queryTriggerInteraction = QueryTriggerInteraction.Ignore;

    private float _lastScannedTime = float.NegativeInfinity;
    private RaycastHit _lastObstacleHit;

    protected float ScanDistance => _scanDistance;

    protected LayerMask LayerMask => _layerMask;

    protected QueryTriggerInteraction QueryTriggerInteraction => _queryTriggerInteraction;

    protected Vector3 AbsoluteDirection => transform.TransformDirection(_scanDirection);

    private void OnValidate()
    {
        _scanDirection = _scanDirection.normalized;
    }

    public RaycastHit GetObstacleHit()
    {
        Scan();
        return _lastObstacleHit;
    }

    public bool HasObstacle()
    {
        Scan();
        return _lastObstacleHit.collider != null;
    }

    protected virtual Vector3 GetScanOrigin()
    {
        return transform.position;
    }

    protected abstract bool Cast(out RaycastHit obstacleHit);


#if UNITY_EDITOR
    protected virtual void DrawDebug(bool hasObstacle)
    {
        Debug.DrawRay(GetScanOrigin(), AbsoluteDirection * _scanDistance, hasObstacle ? Color.red : Color.green);
    }
#endif

    private void Scan()
    {
        if (_lastScannedTime == Time.fixedTime)
            return;

        bool hasObstacle = Cast(out _lastObstacleHit);
#if UNITY_EDITOR
        DrawDebug(hasObstacle);
#endif
        _lastScannedTime = Time.fixedTime;
    }
}