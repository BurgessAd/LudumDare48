﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Options()
    {

    }
    
    public void Quit()
    {
        Application.Quit();
    }

    public static void startMenu()
    {
        SceneManager.LoadScene(sceneName: "StartMenu");
    }


    public void LoadGame()
    {
        SceneManager.LoadScene(sceneName: "NickScene");
    }

    public void Resume()
	{
        Time.timeScale = 1;
	}



}
