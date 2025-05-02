using System.Collections;
using UnityEngine;

public class FoneMusic : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _audioCLips;
    [SerializeField] private float _silenceTime;

    private int _currentAudioIndex;

    private void Start()
    {
        SetNewAudio();
        StartCoroutine(LoopMusicPlay());
    }

    private IEnumerator LoopMusicPlay()
    {
        _audioSource.PlayOneShot(_audioCLips[_currentAudioIndex]);
        float audioLength = _audioCLips[_currentAudioIndex].length;
        yield return new WaitForSeconds(audioLength + _silenceTime);
        SetNewAudio();
        StartCoroutine(LoopMusicPlay());
    }

    private void SetNewAudio() => _currentAudioIndex = Random.Range(0, _audioCLips.Length);
}
