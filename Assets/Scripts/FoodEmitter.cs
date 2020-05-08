using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
    public float EmitterWidth = 10f;          // Half width of emitter
    public float StartDelay = 0f;
    public float LifeTime = 5f;              // time emitted goodFood is active in the scene
    public float FoodSpeedStart = 5f;
    public int FoodSpeedAccelerationMultiplier = 2;

    public ShoppingListScript ShoppingListScript;
    public TextMeshProUGUI[] ItemTextGameObjects;
    public GameController GameController;
    

    [HideInInspector]
    public List<GameObject> InstantiatedFoodObjects = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> ShoppingListItems = new List<GameObject>();

    private List<GameObject> _foodObjects = new List<GameObject>();
    private int ShoppingListSize = 4;
    private float _gameTime;
    private float _foodSpeed;
    private float[] _foodAccelerationSteps = new float[2];

    IEnumerator SpawnHandler()
    {
        yield return new WaitForSeconds(StartDelay);
        while (IsRunning && _foodObjects.Any())
        {
            Spawn();
            yield return new WaitForSeconds(SpawnInterval);
        }
    }

    void Start()
    {
        _gameTime = GameController.GameTime;
        _gameTime -= StartDelay;

        _foodSpeed = FoodSpeedStart;
        _foodAccelerationSteps[0] = _gameTime * 0.25f;
        _foodAccelerationSteps[1] = _gameTime * 0.5f;

        if (!(SpawnInterval <= 0)) _gameTime /= SpawnInterval; 
        ShoppingListItems = ShoppingListScript.CreateShoppingList(ShoppingListSize, GoodFood);

        // maybe change fixed size later

        for (int i = 0; i < ShoppingListItems.Count || i < 4; i++)
        {
            ItemTextGameObjects[i].text = ShoppingListItems[i].name;
            _foodObjects.Add(ShoppingListItems[i]);
            _foodObjects.Add(BadFood[Random.Range(0, BadFood.Length)]);
        }

        int remainingFood = Convert.ToInt32(_gameTime) - (2 * ShoppingListItems.Count);

        for (int i = 1; i <= remainingFood; i++)
        {
            _foodObjects.Add(i % 2 == 0
                ? BadFood[Random.Range(0, BadFood.Length)]
                : GoodFood[Random.Range(0, GoodFood.Length)]);
        }

        IsRunning = true;

        StartCoroutine(SpawnHandler());
    }

    void Update()
    {
        if (IsRunning && GameController.Timer <= _foodAccelerationSteps[0])
        {

            _foodSpeed = _foodSpeed * (FoodSpeedAccelerationMultiplier * 0.75f);
            ChangeSpeedOfInstantiated(_foodSpeed);
            // SpawnInterval *= 0.5f; TODO: change Shoppinglist because there are too less objects generated for that
            _foodAccelerationSteps[0] = 0;

        } else if (IsRunning && GameController.Timer <= _foodAccelerationSteps[1])
        {

            _foodSpeed = _foodSpeed * FoodSpeedAccelerationMultiplier;
            ChangeSpeedOfInstantiated(_foodSpeed);
            // SpawnInterval *= 0.5f; TODO: change Shoppinglist because there are too less objects generated for that
            _foodAccelerationSteps[1] = 0;
        }
    }

    void Spawn()
    {
        var food = _foodObjects[Random.Range(0, _foodObjects.Count)];
        _foodObjects.Remove(food);

        var emittedFood = Instantiate(food, new Vector3(Random.Range(-EmitterWidth, EmitterWidth), 0, 0) + transform.position, Quaternion.identity);
        emittedFood.GetComponent<AnimateObject>().Speed = _foodSpeed;
        InstantiatedFoodObjects.Add(emittedFood);

        Destroy(emittedFood, LifeTime);
    }

    public void SetGameTime(float gametime)
    {
        _gameTime = gametime;
    }

    private void ChangeSpeedOfInstantiated(float speed)
    {
        foreach (var instantiatedFoodObject in InstantiatedFoodObjects)
        {
            if(instantiatedFoodObject != null) instantiatedFoodObject.GetComponent<AnimateObject>().Speed = speed;
        }
    }

}
