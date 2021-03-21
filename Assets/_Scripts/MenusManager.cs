using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenusManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1); // load second scene in build, Game Scene
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0); // load first scene in build, Main Menu
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
