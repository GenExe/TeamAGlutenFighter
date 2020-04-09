using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public GameObject foodEmitter;
    public float gameTime = 20;
    public bool running = true;
    public GameObject endScreen;
    public Text timeText;

    private float _timer;
    private FoodEmitter _foodEmitterScript;

    // Start is called before the first frame update
    void Start()
    {
        _timer = gameTime;
        _foodEmitterScript = foodEmitter.GetComponent<FoodEmitter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            _timer -= Time.deltaTime;
            timeText.text = Mathf.Round(_timer).ToString();              //  Displays the countdown

            if (_timer <= 0f)
            {
                // stop animation from every instantiated food and remove collider
                foreach (var food in _foodEmitterScript.instantiatedFoodObjects)
                {
                    if (food != null)
                    {
                        AnimateObject animateScript = food.GetComponent<AnimateObject>();
                        animateScript.isRunning = false;
                        food.GetComponent<MeshCollider>().enabled = false;
                    }
                }
                running = false;
                _foodEmitterScript.isRunning = false;
                endScreen.SetActive(true);                              //  Activates the restart button
                EventManager.TriggerEvent ("GameOver");
            }
        }
    }

 

    public void RestartGame()
    {
        Debug.Log("RestartGame() Button clicked!");
        EventManager.TriggerEvent ("Restart");
        SceneManager.LoadScene(0);
    }
}
