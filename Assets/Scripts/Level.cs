using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 2f;
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadMainGame()
    {
        SceneManager.LoadScene("Game");
        if (FindObjectOfType<GameSession>())
        {
            FindObjectOfType<GameSession>().ResetGame();
        }
    }

    public void LoadWinGame()
    {
        StartCoroutine(WaitAndLoadWin());
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoadLose());
    }
    IEnumerator WaitAndLoadWin()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Win Game");
    }
    IEnumerator WaitAndLoadLose()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Game Over");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
