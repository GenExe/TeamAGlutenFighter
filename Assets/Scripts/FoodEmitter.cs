﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FoodEmitter : MonoBehaviour
{
    public bool IsRunning;
    public GameObject[] GoodFood;
    public GameObject[] BadFood;
    public float SpawnInterval = 1.5f;        // Spawn interval
    public float EmitterWidth = 10;          // Half width of emitter
    public float StartDelay = 0;
    public float LifeTime = 5;              // time emitted goodFood is active in the scene
    public ShoppingListScript ShoppingListScript;
    public TextMeshProUGUI[] ItemTextGameObjects;
    public GameController GameController;
    

    [HideInInspector]
    public List<GameObject> InstantiatedFoodObjects = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> ShoppingListItems = new List<GameObject>();

    private List<GameObject> FoodObjects = new List<GameObject>();
    private int ShoppingListSize = 4;
    private float _gametime;

    void Start()
    {
        _gametime = GameController.GameTime;
        _gametime -= StartDelay;

        if (!(SpawnInterval <= 0)) _gametime /= SpawnInterval; 
        ShoppingListItems = ShoppingListScript.CreateShoppingList(ShoppingListSize, GoodFood);

        // maybe change fixed size later

        for (int i = 0; i < ShoppingListItems.Count || i < 4; i++)
        {
            ItemTextGameObjects[i].text = ShoppingListItems[i].name;
            FoodObjects.Add(ShoppingListItems[i]);
            FoodObjects.Add(BadFood[Random.Range(0, BadFood.Length)]);
        }

        int remainingFood = Convert.ToInt32(_gametime) - (2 * ShoppingListItems.Count);

        for (int i = 1; i <= remainingFood; i++)
        {
            FoodObjects.Add(i % 2 == 0
                ? BadFood[Random.Range(0, BadFood.Length)]
                : GoodFood[Random.Range(0, GoodFood.Length)]);
        }

        IsRunning = true;
        InvokeRepeating("Spawn", StartDelay, SpawnInterval);
    }

    void Spawn()
    {
        if (!IsRunning || !FoodObjects.Any()) return;

        var food = FoodObjects[Random.Range(0, FoodObjects.Count)];
        FoodObjects.Remove(food);

        var emittedFood = Instantiate(food, new Vector3(Random.Range(-EmitterWidth, EmitterWidth), 0, 0) + transform.position, Quaternion.identity);
        InstantiatedFoodObjects.Add(emittedFood);

        Destroy(emittedFood, LifeTime);
    }

    public void SetGameTime(float gametime)
    {
        _gametime = gametime;
    }

}
