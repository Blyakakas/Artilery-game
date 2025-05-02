using UnityEngine;

public class Mobile_ArtileryController : MonoBehaviour, IArtileryControlable
{
    [Header("Sensitivity")]
    [SerializeField] private float _horizontalSensitivity = 100f;
    [SerializeField] private float _verticalSensitivity = 50f;

    [Header("Limits")]
    [SerializeField] private float _minElevation = -5f;
    [SerializeField] private float _maxElevation = 65f;
    [SerializeField] private float _maxHorizontalAngle = 30f;

    [Header("Smoothing")]
    [SerializeField] private float _turretSmoothSpeed = 5f;
    [SerializeField] private float _cannonSmoothSpeed = 5f;

    [Header("Transforms")]
    [SerializeField] private Transform _turret;
    [SerializeField] private Transform _tankCannon;

    private Vector2 _startTouch;

    private float _targetTurretAngle = 0f;
    private float _targetElevation = 0f;

    private float _currentTurretAngle = 0f;
    private float _currentElevation = 0f;

    private Quaternion _initialTurretRotation;

    private void Start()
    {
        _initialTurretRotation = _turret.localRotation;
    }

    public void Rotate()
    {
        HandleTouchInput();
        ApplySmoothRotation();
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);
        switch (touch.phase)
        {
            case TouchPhase.Began:
                _startTouch = touch.position;
                break;

            case TouchPhase.Moved:
                Vector2 delta = touch.position - _startTouch;

                float deltaX = delta.x * _horizontalSensitivity / Screen.width;
                float deltaY = delta.y * _verticalSensitivity / Screen.height;

                _targetTurretAngle = Mathf.Clamp(_targetTurretAngle + deltaX, -_maxHorizontalAngle, _maxHorizontalAngle);
                _targetElevation = Mathf.Clamp(_targetElevation - deltaY, _minElevation, _maxElevation);

                _startTouch = touch.position;
                break;
        }
    }

    private void ApplySmoothRotation()
    {
        _currentTurretAngle = Mathf.Lerp(_currentTurretAngle, _targetTurretAngle, Time.deltaTime * _turretSmoothSpeed);
        _currentElevation = Mathf.Lerp(_currentElevation, _targetElevation, Time.deltaTime * _cannonSmoothSpeed);

        _turret.localRotation = _initialTurretRotation * Quaternion.Euler(0f, _currentTurretAngle, 0f);
        _tankCannon.localRotation = Quaternion.Euler(_currentElevation, 0f, 0f);
    }
}
