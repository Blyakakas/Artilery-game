using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Collision")]
    [SerializeField] private float _radius = 0.2f;
    [SerializeField] private LayerMask _hitMask;

    [Header("Effects & Damage")]
    [SerializeField] private ParticleSystem _explosionVFX;
    [SerializeField] private AudioClip _explosionSFX;
    [SerializeField] private float _explosionRadius = 3f;
    [SerializeField] private float _destroyDelay = 2f;

    private Vector3 _startPos;
    private Vector3 _initVelocity;
    private float _shapeDuration;
    private float _flightDuration;

    private float _elapsed;
    private Vector3 _prevPos;
    private AudioSource _audioSource;
    private bool _hasExploded;

    public void Initialize(Vector3 startPos, Vector3 initVelocity, float shapeDuration, float flightDuration)
    {
        _startPos = startPos;
        _initVelocity = initVelocity;
        _shapeDuration = shapeDuration;
        _flightDuration = flightDuration;

        _prevPos = startPos;
        transform.position = startPos;

        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.playOnAwake = false;

        Destroy(gameObject, _destroyDelay);
    }

    private void Update()
    {
        if (_hasExploded) return;

        _elapsed += Time.deltaTime;
        float normalized = _elapsed / _flightDuration;
        if (normalized > 1f)
            return;

        float t = normalized * _shapeDuration;
        Vector3 nextPos = _startPos + _initVelocity * t + 0.5f * Physics.gravity * t * t;

        Vector3 dir = nextPos - _prevPos;
        float dist = dir.magnitude;
        if (dist > Mathf.Epsilon)
        {
            if (Physics.SphereCast(_prevPos, _radius, dir.normalized, out var hit, dist, _hitMask))
            {
                Explode(hit.point, hit.normal, hit.collider);
                return;
            }
        }

        transform.position = nextPos;
        _prevPos = nextPos;
    }

    private void Explode(Vector3 point, Vector3 normal, Collider other)
    {
        if (_hasExploded) return;
        _hasExploded = true;

        if (_explosionVFX)
            Instantiate(_explosionVFX, point, Quaternion.LookRotation(normal));

        if (_explosionSFX)
            _audioSource.PlayOneShot(_explosionSFX);

        Collider[] hits = Physics.OverlapSphere(point, _explosionRadius, _hitMask);
        foreach (var col in hits)
            if (col.TryGetComponent<TankDestroyer>(out var tank))
                tank.TakeDamage();

        Destroy(gameObject, _destroyDelay);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
