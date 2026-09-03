using UnityEngine;

public class SphereObstacleScanner : ObstacleScanner
{
    [SerializeField] private float _sphereRadius = 0.2f;

    public float SphereRadius => _sphereRadius;

    protected override bool Cast(out RaycastHit obstacleHit)
    {
        return Physics.SphereCast(
            GetScanOrigin(),
            _sphereRadius,
            AbsoluteDirection,
            out obstacleHit,
            ScanDistance,
            LayerMask,
            QueryTriggerInteraction);
    }

#if UNITY_EDITOR
    protected override void DrawDebug(bool hasObstacle)
    {
        Debug.DrawRay(GetScanOrigin(), AbsoluteDirection * ScanDistance, hasObstacle ? Color.red : Color.green);
    }
# endif
}