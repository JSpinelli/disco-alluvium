using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject pauseMenu;

    private bool _paused;


    private void Start()
    {
        pauseMenu.SetActive(false);
        _paused = false;
        Time.timeScale = 1;
    }

    public void Resume()
    {
        
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        _paused = false;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_paused)
            {
                _paused = false;
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                _paused = true;
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}