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
    public GameObject FoodEmitter;
    public float GameTime = 20;
    public bool Running = true;
    public GameObject EndScreen;
    public Text TimeText;

    private float _timer;
    private FoodEmitter _foodEmitterScript;

    // Start is called before the first frame update
    void Start()
    {
        _timer = GameTime;
        _foodEmitterScript = FoodEmitter.GetComponent<FoodEmitter>();
        _foodEmitterScript.SetGameTime(GameTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Running)
        {
            _timer -= Time.deltaTime;
            TimeText.text = Mathf.Round(_timer).ToString();              //  Displays the countdown

            if (_timer <= 0f)
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
                EndScreen.SetActive(true);                              //  Activates the restart button
            }
        }
    }

 

    public void RestartGame()
    {
        Debug.Log("RestartGame() Button clicked!");
        SceneManager.LoadScene(0);
    }
}
