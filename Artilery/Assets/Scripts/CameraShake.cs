using DG.Tweening;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private ArtileryTrajectory _artileryTrajectory;

    private void Start()
    {
        _artileryTrajectory.OnFired += ShakeOnFire;
    }

    private void OnDestroy()
    {
        _artileryTrajectory.OnFired -= ShakeOnFire;
    }

    public void ShakeOnFire()
    {
        transform.DOPunchPosition(new Vector3(0, 0.05f, -0.3f), 0.1f, 10, 1f);

    }
}
