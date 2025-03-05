using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    private void Start()
    {
        pauseMenuUI.SetActive(isPaused);
    }

    public virtual void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public virtual void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused); // Hiện hoặc ẩn menu
        Time.timeScale = isPaused ? 0 : 1; // Dừng hoặc tiếp tục game
    }
}
