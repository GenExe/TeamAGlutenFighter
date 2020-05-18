using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Highscore : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _highscore = null;
    [SerializeField]
    private ScoreCalculator _scoreCalculator = null;
    [SerializeField]
    private GameController _gameController = null;

    public GameObject HighscoreInput;
    public InputField NickInputField;
    public bool NewHighscore = false;
    public bool ShouldResetHighscore = false;

    private int _highscoreScore = 0;
    private String _hsName = String.Empty;
    private int _currScore = 0;
    private bool isRuninng = false;

    // Start is called before the first frame update
    void Start()
    {
        ResetHighscore();
        NewHighscore = false;
        isRuninng = _gameController.Running;

        _highscoreScore = PlayerPrefs.GetInt("highscore", 0);
        _hsName = PlayerPrefs.GetString("nickname", "nobody");

        _highscore.SetText(_highscoreScore.ToString() + " by " + _hsName);

        HighscoreInput.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isRuninng)
        {
            _currScore = _scoreCalculator.Score;

            _highscore.SetText((_currScore > _highscoreScore) ? (_currScore.ToString() + " by YOU") : (_highscoreScore.ToString() + " by " + _hsName));

            NewHighscore = (_currScore > _highscoreScore) ? true : false;
        }
    }

    public void OnEndEdit(String input)
    {
        HighscoreInput.SetActive(false);
        PlayerPrefs.SetString("nickname", NickInputField.text);
        PlayerPrefs.SetInt("highscore", _scoreCalculator.Score);
        _gameController.EndScreen.SetActive(true);
    }

    public void ResetHighscore()
    {
        if (ShouldResetHighscore)
        {
            PlayerPrefs.SetString("nickname", "YOU");
            PlayerPrefs.SetInt("highscore", 0);
        }
    }
}