using UnityEngine;
using UnityEngine.SceneManagement;

public class DefaultOperations : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
    }
    
    public void LoadScene(int n) => SceneManager.LoadScene(n);
    public void RestartScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void LoadScenesync(int n) => SceneManager.LoadSceneAsync(n);

}
