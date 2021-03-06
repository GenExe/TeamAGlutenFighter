﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject FoodEmitter;
    public float GameTime = 20;
    public bool Running = true;
    public GameObject EndScreen;
    public Text TimeText;
    public Highscore Highscore;
    public AudioSource VictoryAudioSource;

    public float Timer;
    private FoodEmitter _foodEmitterScript;

    // Start is called before the first frame update
    void Start()
    {
        Timer = GameTime;
        _foodEmitterScript = FoodEmitter.GetComponent<FoodEmitter>();
        _foodEmitterScript.SetGameTime(GameTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Running)
        {
            Timer -= Time.deltaTime;
            TimeText.text = Mathf.Round(Timer).ToString();              //  Displays the countdown

            if (Timer <= 0f)
            {
                // stop animation from every instantiated goodFood and remove collider
                foreach (var food in _foodEmitterScript.InstantiatedFoodObjects)
                {
                    if (food != null)
                    {
                        AnimateObject animateScript = food.GetComponent<AnimateObject>();
                        animateScript.IsRunning = false;
                        food.GetComponent<MeshCollider>().enabled = false;
                    }
                }

                Running = false;
                _foodEmitterScript.IsRunning = false;

                if (Highscore.NewHighscore)
                {
                    VictoryAudioSource.Play();
                    Highscore.HighscoreInput.SetActive(true);
                    Highscore.NickInputField.Select();
                    Highscore.NickInputField.ActivateInputField();
                }
                else
                {
                    EndScreen.SetActive(true);  //  Activates the restart button
                }
            }
        }
    }

    public void RestartGame()
    {
        VictoryAudioSource.Stop();
        EventManager.TriggerEvent("ScoreUpdated", new EventParam());
        Debug.Log("RestartGame() Button clicked!");
        SceneManager.LoadScene(0);
    } 
}
