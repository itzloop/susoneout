using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] private Button playButton;

    private void Awake()
    {
        // load last high score
        // load coin
        
        playButton.onClick.AddListener(Play);
    }

    private void Play()
    {
        SceneManager.LoadScene("Scenes/Game");
    }
}
