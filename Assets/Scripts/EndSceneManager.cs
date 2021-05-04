using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    public void PressRestart()
    {
        SceneManager.LoadScene(0);
    }

    public void PressQuit()
    {
        Application.Quit();
    }

}
