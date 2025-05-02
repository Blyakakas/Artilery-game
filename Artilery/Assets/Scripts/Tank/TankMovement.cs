using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TankMovement : MonoBehaviour
{
    [SerializeField] private Transform _tankTurret;
    [SerializeField] private float _distanceToStop = 7f;
    [SerializeField] private float _rotationSpeed = 3f;
    [SerializeField] private float _timeToKill = 3f;

    private Transform _artilery;
    private TankShooting _tankShooting;
    private TankDestroyer _tankDestroyer;
    private NavMeshAgent _agent;
    private bool _killStarting = false;

    private void Start()
    {
        _tankDestroyer = GetComponent<TankDestroyer>();
        _tankShooting = FindFirstObjectByType<TankShooting>();
        _artilery = FindAnyObjectByType<ArtileryRotation>().transform;
        _agent = GetComponent<NavMeshAgent>();
        _agent?.SetDestination(_artilery.position);
    }

    private void Update()
    {
        if (Vector3.Distance(_tankTurret.position, _artilery.position) <= _distanceToStop)
        {
            Vector3 _directionToTarget = _artilery.position - _tankTurret.position;
            _directionToTarget.y = 0;
            if(!_killStarting) StartCoroutine(KillDelay());
            Quaternion targetRotation = Quaternion.LookRotation(_directionToTarget);
            _tankTurret.rotation = Quaternion.Lerp(_tankTurret.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
        }
    }

    private IEnumerator KillDelay()
    {
        _killStarting = true;
        Debug.Log("Kill Delay Started");
        yield return new WaitForSeconds(_timeToKill);
        if(_tankDestroyer.isAlive) _tankShooting.Shoot();
        else _killStarting = false;
    }
}
