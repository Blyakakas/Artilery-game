using UnityEngine;

public class DeviceData : MonoBehaviour
{
    [SerializeField] private ArtileryRotation _artileryRotation;
    [SerializeField] private DeviceType _deviceType;
    [SerializeField] private PC_ArtileryController _pcArtileryController;
    [SerializeField] private Mobile_ArtileryController _mobileArtileryController;

    private void Start()
    {
        if (_deviceType == DeviceType.PC) _artileryRotation.SetControllerDevice(_pcArtileryController);
        else if (_deviceType == DeviceType.Mobile) _artileryRotation.SetControllerDevice(_mobileArtileryController);
    }
}

public enum DeviceType
{
    PC,
    Mobile
}
