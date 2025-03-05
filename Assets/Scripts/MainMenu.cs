using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene(4);
    }

    public void LoadLevel4()
    {
        SceneManager.LoadScene(5);
    }

    public void LoadLevel5()
    {
        SceneManager.LoadScene(6);
    }

    public void LoadLevel6()
    {
        SceneManager.LoadScene(7);
    }


    public void Quit()
    {
        Application.Quit();
    }
}
