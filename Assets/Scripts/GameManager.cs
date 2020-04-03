using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float gameTime = 20;
    public bool running = true;
    public GameObject endScreen;
    public GameObject FoodEmitter;
    public Text timeText;
    private float timer;

    private void Start()
    {
        timer = gameTime;
    }


    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            timer -= Time.deltaTime;
            timeText.text = Mathf.Round(timer).ToString();              //  Displays the contdown

            if (timer <= 0f)
            {
                running = false;
                endScreen.SetActive(true);                              //  Activates the restart button
                FoodEmitter.SetActive(false);                           //  Deactivates the FoodEmitter
                // Here you could also load a new scene to prevent the Foodemitter to create additional objects ...
            }
        }
    }

    public void restartGame()
    {
        SceneManager.LoadScene(0);
    }
}
