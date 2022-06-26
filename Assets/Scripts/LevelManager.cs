using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }   
    
    public void OpenMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenVsLocalScene()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenVsIAScene()
    {
        //SceneManager.LoadScene(2);
    }
}
