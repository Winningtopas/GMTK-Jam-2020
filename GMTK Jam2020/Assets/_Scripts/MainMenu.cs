using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject levelLoader;

    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //levelLoader.LoadNextLevel();
        levelLoader.GetComponent<LevelLoader>().LoadNextLevel();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
