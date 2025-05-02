using UnityEngine;

public class FireEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private ArtileryTrajectory _artileryTrajectory;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _fireAudio;

    private void Start()
    {
        _artileryTrajectory.OnFired += Fire;
    }

    private void OnDisable()
    {
        _artileryTrajectory.OnFired -= Fire;
    }

    public void Fire()
    {
        _muzzleFlash.Play();
        _audioSource.PlayOneShot(_fireAudio);
    }
}
