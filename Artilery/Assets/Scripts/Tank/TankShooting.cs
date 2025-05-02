using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class TankShooting : MonoBehaviour
{
    [SerializeField] private PlayableDirector _playableDirector;

    public void Shoot()
    {
        Debug.Log("212");
        _playableDirector.Play();
    }
}
