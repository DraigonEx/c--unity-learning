using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NumberWizard : MonoBehaviour {

    int max;
    int min;
    int guess;

    public Text text;

    public int maxGuessesAllowed = 5;

	// Use this for initialization
	void Start () {
        StartGame();
	}

    void StartGame ()
    {
        max = 1000;
        min = 1;
        guess = Random.Range(min, max);
        text.text = guess.ToString();

        max++;
    }

    public void GuessHigher()
    {
        min = guess;
        NextGuess();
    }

    public void GuessLower()
    {
            max = guess;
            NextGuess();
    }

    void NextGuess ()
    {
        guess = Random.Range(min, max);
        maxGuessesAllowed--;
        if(maxGuessesAllowed <= 0)
        {
            SceneManager.LoadScene("Win");
        }
        text.text = guess.ToString();
    }

}
