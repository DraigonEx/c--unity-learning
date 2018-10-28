﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStatus : MonoBehaviour {

    //config params
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed = 10;
    [SerializeField] TextMeshProUGUI scoreText;

    //state variables
    [SerializeField] int currentScore;

    private static GameStatus _instance = null;

    public static GameStatus Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        scoreText.text = currentScore.ToString();
    }

    // Update is called once per frame
    void Update () {
        Time.timeScale = gameSpeed;
	}

    public void AddScore()
    {
        currentScore += pointsPerBlockDestroyed;
        scoreText.text = currentScore.ToString();
    }

    internal void Reset()
    {
        _instance = null;
        Destroy(gameObject);
    }

}
