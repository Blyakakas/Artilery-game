using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ArtileryAnimation : MonoBehaviour
{
    [SerializeField] private ArtileryTrajectory _artileryTrajectory;

    private Animator _animator;

    private void Start()
    {
        _artileryTrajectory.OnFired += PlayFireAnimation;
        _animator = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        _artileryTrajectory.OnFired -= PlayFireAnimation;
    }

    private void PlayFireAnimation() => _animator.SetTrigger("Fire");
}
