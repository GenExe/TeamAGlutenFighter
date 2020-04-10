using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;
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
    public ShoppingList ShoppingList;

    [HideInInspector]
    public List<GameObject> InstantiatedFoodObjects = new List<GameObject>();

    private List<GameObject> FoodObjects = new List<GameObject>();
    private int ShoppingListSize = 2;
    private float _gametime = 10f;

    // Start is called before the first frame update
    void Start()
    {
        _gametime -= StartDelay;

        if (!(SpawnInterval <= 0)) _gametime /= SpawnInterval;

        List<GameObject> shoppingList = ShoppingList.CreateShoppingList(ShoppingListSize);

        foreach (var item in shoppingList)
        {
            FoodObjects.Add(item);
            FoodObjects.Add(BadFood[Random.Range(0, BadFood.Length)]);
        }

        int remainingFood = Convert.ToInt32(_gametime) - (2 * shoppingList.Count);

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



}
