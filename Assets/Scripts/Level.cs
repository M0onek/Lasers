using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 2f;
    public void LoadStartMenu()
    {
        SceneManager.LoadScene("Start Menu");
        if (FindObjectOfType<GameSession>())
        {
            FindObjectOfType<GameSession>().ResetGame();
        }
    }

    public void LoadAllLevels()
    {
        SceneManager.LoadScene("Levels");
    }

    public void LoadMainGame()
    {
        SceneManager.LoadScene("Game");
        if (FindObjectOfType<GameSession>())
        {
            FindObjectOfType<GameSession>().ResetGame();
        }
    }
    public void LoadMainGame1()
    {
        SceneManager.LoadScene("Game 1");
        if (FindObjectOfType<GameSession>())
        {
            FindObjectOfType<GameSession>().ResetGame();
        }
    }
    public void LoadMainGame2()
    {
        SceneManager.LoadScene("Game 2");
        if (FindObjectOfType<GameSession>())
        {
            FindObjectOfType<GameSession>().ResetGame();
        }
    }

    public void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().name == "Win Game")
        {
            SceneManager.LoadScene("Game 1");
            if (FindObjectOfType<GameSession>())
            {
                FindObjectOfType<GameSession>().ResetGame();
            }
        }
        if (SceneManager.GetActiveScene().name == "Win Game 1")
        {
            SceneManager.LoadScene("Game 2");
            if (FindObjectOfType<GameSession>())
            {
                FindObjectOfType<GameSession>().ResetGame();
            }
        }
    }

    public void LoadWinGame(string sceneName)
    {
        StartCoroutine(WaitAndLoadWin(sceneName));
    }

    public void LoadGameOver(string sceneName)
    {
        StartCoroutine(WaitAndLoadLose(sceneName));
    }
    IEnumerator WaitAndLoadWin(string sceneName)
    {
        yield return new WaitForSeconds(delayInSeconds);
        if (sceneName == "Game")
        {
            SceneManager.LoadScene("Win Game");
        }
        if (sceneName == "Game 1")
        {
            SceneManager.LoadScene("Win Game 1");
        }
        if (sceneName == "Game 2")
        {
            SceneManager.LoadScene("Win Game 2");
        }
    }
    IEnumerator WaitAndLoadLose(string sceneName)
    {
        yield return new WaitForSeconds(delayInSeconds);
        if (sceneName == "Game")
        {
            SceneManager.LoadScene("Game Over");
        }
        if (sceneName == "Game 1")
        {
            SceneManager.LoadScene("Game Over 1");
        }
        if (sceneName == "Game 2")
        {
            SceneManager.LoadScene("Game Over 2");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
