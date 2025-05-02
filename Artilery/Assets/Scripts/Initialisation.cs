using UnityEngine;

public class Initialisation : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 80;
        QualitySettings.vSyncCount = 0;
    }
}
