using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;

public class TankDestroyer : MonoBehaviour
{
    [SerializeField] private Rigidbody _turret;
    [SerializeField] private ParticleSystem _fireAfterDestroy;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _timeToStopBlaming = 15;
    [SerializeField] private float _timeToStopPhysics;
    [SerializeField] private float _timeToDestroy = 30;
            

    private Rigidbody _mainRigidBody;
    private int _health;

    public bool isAlive { get; private set; } = true;

    private void Start()
    {
        _mainRigidBody = GetComponent<Rigidbody>();
        _health = Random.Range(0, 2);
    }

    private void DestroyTank()
    {
        _agent.Stop();
        _mainRigidBody.useGravity = true;
        Destroy(_agent);
        _turret.isKinematic = false;
        _turret.useGravity = true;
        _turret.AddForce(Vector3.up * 10f, ForceMode.Impulse);
        _turret.AddForce(Vector3.forward * 4f, ForceMode.Impulse);
        _turret.transform.parent = null;
        _fireAfterDestroy.Play();
        _audioSource.Stop();
        isAlive = false;
        Destroy(_fireAfterDestroy.gameObject, _timeToStopBlaming);
        Destroy(_turret, _timeToStopPhysics);
        Destroy(_mainRigidBody, _timeToStopPhysics);
        Destroy(gameObject, _timeToDestroy);
    }

    public void TakeDamage()
    {
        if (_health <= 0) DestroyTank();
        else _health -= 1;
    }
}
