using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroSceneManager : MonoBehaviour
{
    public Dropdown livesDropDown;
    public Slider timeSlider; 
    public InputField playerNameBox;


    private void Start()
    {
        PlayerPrefs.SetInt("Lives", 1);
        PlayerPrefs.SetInt("Time", 60);
    }

    public void ChangeLives()
    {
        PlayerPrefs.SetInt("Lives", livesDropDown.value);
    }

    public void ChangeTime()
    {
        PlayerPrefs.SetInt("Time", (int)(timeSlider.value));
    }


    public void NewName()
    {
        PlayerPrefs.SetString("Name", playerNameBox.text);
    }

    public void PressStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
