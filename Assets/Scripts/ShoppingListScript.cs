using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShoppingListScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<GameObject> CreateShoppingList(int size, GameObject[] foodObjects)
    {
        var shoppingList = new List<GameObject>();


        //TODO: maybe remove selected goodFood
        for (int i = 0; i < size; i++)
        {
            shoppingList.Add(foodObjects[Random.Range(0, foodObjects.Length - 1)]);
        }


        return shoppingList;
    }
}
