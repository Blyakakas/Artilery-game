using UnityEngine;

public class PC_ArtileryController : MonoBehaviour, IArtileryControlable
{
    [SerializeField] private float _verticalSensitivity = 50f;
    [SerializeField] private float _horizontalSensitivity = 100f;
    [SerializeField] private Transform _turret;
    [SerializeField] private Transform _tankCannon;

    [SerializeField] private float _minElevation = -5f;
    [SerializeField] private float _maxElevation = 30f;

    private float _currentElevation = 0f;

    public void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * _horizontalSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _verticalSensitivity * Time.deltaTime;

        _turret.Rotate(Vector3.up * mouseX);

        _currentElevation = Mathf.Clamp(_currentElevation - mouseY, _minElevation, _maxElevation);
        _tankCannon.localEulerAngles = new Vector3(_currentElevation, 0f, 0f);
    }
}
