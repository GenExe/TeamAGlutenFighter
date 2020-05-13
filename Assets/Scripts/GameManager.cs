using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float GameTime = 20;
    public bool Running = true;
    public GameObject EndScreen;
    public GameObject FoodEmitter;
    public Text TimeText;
    private float _timer;

    private void Start()
    {
        _timer = GameTime;
    }


    // Update is called once per frame
    void Update()
    {
        if (Running)
        {
            _timer -= Time.deltaTime;
            TimeText.text = Mathf.Round(_timer).ToString();              //  Displays the contdown

            if (_timer <= 0f)
            {
                Running = false;
                EndScreen.SetActive(true);                              //  Activates the restart button
                FoodEmitter.SetActive(false);                           //  Deactivates the FoodEmitter
                // Here you could also load a new scene to prevent the Foodemitter to create additional objects ...
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
