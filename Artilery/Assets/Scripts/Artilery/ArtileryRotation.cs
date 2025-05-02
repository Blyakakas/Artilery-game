using UnityEngine;

public class ArtileryRotation : MonoBehaviour
{
    private IArtileryControlable _controller;

    public void SetControllerDevice(IArtileryControlable controller) => _controller = controller;

    private void Update()
    {
        _controller?.Rotate();
    }
}
