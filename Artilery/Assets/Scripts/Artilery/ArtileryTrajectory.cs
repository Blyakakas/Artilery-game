using System;
using System.Collections;
using UnityEngine;

public class ArtileryTrajectory : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _firePoint;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private GameObject _projectilePrefab;

    [Header("Trajectory Settings")]
    [SerializeField] private float _displayedVelocity = 50f;
    [SerializeField] private int _trajectorySteps = 60;

    [Header("Firing Settings")]
    [SerializeField] private float _flightDuration = 0.25f;
    [SerializeField] private float _reloadTime = 1f;

    [Header("Impact Marker")]
    [SerializeField] private GameObject _impactMarkerPrefab;
    [SerializeField] private LayerMask _groundMask;

    private GameObject _impactMarker;
    private Vector3 _initialVelocity;
    private float _shapeDuration;
    private bool _isReloading;

    public Action OnFired;
    public Action OnReloaded;

    private void Awake()
    {
        if (_impactMarkerPrefab != null)
        {
            _impactMarker = Instantiate(_impactMarkerPrefab);
            _impactMarker.SetActive(false);
        }
    }

    private void Update()
    {
        DrawTrajectory();

        // Editor click or button call
    }

    private void DrawTrajectory()
    {
        Vector3 p0 = _firePoint.position;
        _initialVelocity = _firePoint.forward * _displayedVelocity;

        float y0 = p0.y;
        float vY = _initialVelocity.y;
        float g = Physics.gravity.y * 0.5f;
        float D = vY * vY - 4f * g * y0;
        _shapeDuration = D < 0 ? 0f
            : Mathf.Max(
                (-vY + Mathf.Sqrt(D)) / (2f * g),
                (-vY - Mathf.Sqrt(D)) / (2f * g),
                0f);

        _lineRenderer.positionCount = _trajectorySteps;
        Vector3 lastPos = p0;
        for (int i = 0; i < _trajectorySteps; i++)
        {
            float t = (_shapeDuration * i) / (_trajectorySteps - 1);
            Vector3 pos = p0 + _initialVelocity * t + 0.5f * Physics.gravity * t * t;
            _lineRenderer.SetPosition(i, pos);
            lastPos = pos;
        }

        if (_impactMarker)
        {
            RaycastHit hit;
            Vector3 origin = lastPos + Vector3.up * 0.5f;
            if (Physics.Raycast(origin, Vector3.down, out hit, 10f, _groundMask))
            {
                _impactMarker.SetActive(true);
                _impactMarker.transform.position = hit.point;
                _impactMarker.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            }
            else _impactMarker.SetActive(false);
        }
    }

    public void ButtonFire()
    {
        if (_isReloading) return;
        Fire();
    }

    private void Fire()
    {
        _isReloading = true;
        StartCoroutine(ReloadRoutine());

        OnFired?.Invoke();
        var projGO = Instantiate(_projectilePrefab, _firePoint.position, Quaternion.identity);
        if (projGO.TryGetComponent<Projectile>(out var proj))
        {
            proj.Initialize(
                startPos: _firePoint.position,
                initVelocity: _initialVelocity,
                shapeDuration: _shapeDuration,
                flightDuration: _flightDuration);
        }
    }

    private IEnumerator ReloadRoutine()
    {
        yield return new WaitForSeconds(_reloadTime);
        _isReloading = false;
        OnReloaded?.Invoke();
    }
}
