using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public Canvas menu;
    bool isPaused;

    [SerializeField]
    private Toggle musicToggle;
    [SerializeField]
    private AudioSource myAudio;

    public GameSceneManager gamescenemanager;


    public void Awake()
    {
        //Pause();
        Unpause();
//        musicToggle.enabled = false;
        if (!PlayerPrefs.HasKey("music"))
        {
            PlayerPrefs.SetInt("music", 1);
            musicToggle.isOn = true;
            myAudio.enabled = true;
            PlayerPrefs.Save();
        }
        else
        {
            if (PlayerPrefs.GetInt("music") == 0)
            {
                myAudio.enabled = false;
                musicToggle.isOn = false;
            }
            else
            {
                myAudio.enabled = true;
                musicToggle.isOn = true;
            }
        }
    }

    public void ToggleMusic()
    {
        if (musicToggle.isOn)
        {
            PlayerPrefs.SetInt("music", 1);
            myAudio.enabled = true;
        }
        else
        {
            PlayerPrefs.SetInt("music", 0);
            myAudio.enabled = false;
        }
        PlayerPrefs.Save();
    }


    public void Pause()
    {
        menu.enabled = true;
        Time.timeScale = 0;
        isPaused = true;
    }

    public void Unpause()
    {
        menu.enabled = false;
        Time.timeScale = 1;
        isPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isPaused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();

        save.name = gamescenemanager.playerName.text;
        save.score = gamescenemanager.score;
        save.lives = gamescenemanager.lives;
        save.clock = gamescenemanager.clock;

        return save;
    }

    public void SaveGame()
    {
        Save save = CreateSaveGameObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            gamescenemanager.playerName.text = save.name;
            gamescenemanager.score = save.score;
            gamescenemanager.scoreText.text = save.score.ToString();
            gamescenemanager.lives = save.lives;
            gamescenemanager.livesText.text = save.lives.ToString();
            gamescenemanager.clock = save.clock;
            gamescenemanager.clockText.text = "(" + ((int)save.clock).ToString() + " seconds left)";
            Debug.Log("Game Loaded");

            Unpause();
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }
    public void SaveAsJSON()
    {
        Save save = CreateSaveGameObject();
        string json = JsonUtility.ToJson(save);

        Debug.Log("Saving as JSON: " + json);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }
}
