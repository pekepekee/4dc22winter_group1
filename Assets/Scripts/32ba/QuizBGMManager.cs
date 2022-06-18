using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizBGMManager : MonoBehaviour
{
    private static QuizBGMManager Instance {
        get;
        set;
    }

    private void Awake(){
        if (Instance != null) {
            Destroy (gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad (gameObject);
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene next, LoadSceneMode mode)
    {
        if(next.name == "quiz") return;
        Destroy(Instance.gameObject);
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
