using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public void PlayButtonFunction()
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMenuButtonFunction()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitButtonFunction()
    {
        Application.Quit();
    }
}
