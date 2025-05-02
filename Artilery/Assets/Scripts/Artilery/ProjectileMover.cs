using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    private Vector3 _startPos;
    private Vector3 _initVel;
    private float _shapeDuration;
    private float _flightDuration;
    private float _lifeTime;

    private float elapsed;
    private Vector3 prevPos;

    public void Initialize(Vector3 start, Vector3 velocity, float shapeT, float flightT, float ttl)
    {
        _startPos = start;
        _initVel = velocity;
        _shapeDuration = shapeT;
        _flightDuration = flightT;
        _lifeTime = ttl;
        elapsed = 0f;
        prevPos = _startPos;
        Destroy(gameObject, _lifeTime);
    }

    void Update()
    {
        elapsed += Time.deltaTime;
        float normT = elapsed / _flightDuration;

        float t = normT * _shapeDuration;
        Vector3 newPos = _startPos + _initVel * t + 0.5f * Physics.gravity * t * t;

        Vector3 dir = (newPos - prevPos).normalized;
        if (dir.sqrMagnitude > 0.0001f)
            transform.rotation = Quaternion.LookRotation(dir);

        transform.position = newPos;
        prevPos = newPos;
    }
}
