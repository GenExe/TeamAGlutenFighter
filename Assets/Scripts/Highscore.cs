using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Highscore : MonoBehaviour
{
    public Text highscore;
    public ScoreCalculator scoreCalculator;
    public GameObject highscoreInput;
    public InputField inputField;
    public GameController gameController;
    public bool newHighscore = false; 

    private int _highscore;
    private String _hsName;
    private int _currScore;
    private bool isRuninng;
    

    // Start is called before the first frame update
    void Start()
    {
        newHighscore = false;
        isRuninng = gameController.running;

        _highscore = PlayerPrefs.GetInt("highscore", 0);
        _hsName = PlayerPrefs.GetString("nickname", "nobody");

        highscore.text = _highscore.ToString() + " by " + _hsName;
        highscoreInput.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isRuninng)
        {
            _currScore = scoreCalculator.Score;

            highscore.text = (_currScore > _highscore) ? (_currScore.ToString() + " by YOU") : (_highscore.ToString() + " by " + _hsName);

            newHighscore = (_currScore > _highscore) ? true : false; 
        }
        
    }

    public void OnEndEdit(String input)
    {

        highscoreInput.SetActive(false);
        PlayerPrefs.SetString("nickname", inputField.text);
        PlayerPrefs.SetInt("highscore", scoreCalculator.Score);
        gameController.endScreen.SetActive(true);

    }
}
