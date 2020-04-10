using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShoppingList : MonoBehaviour
{
    public List<GameObject> GoodFoods;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<GameObject> CreateShoppingList(int size)
    {
        var shoppingList = new List<GameObject>();

        if (GoodFoods != null)
        {
            //TODO: maybe remove selected goodFood
            for (int i = 0; i < size; i++)
            {
                shoppingList.Add(GoodFoods[Random.Range(0, GoodFoods.Count - 1)]);
            }
        }

        return shoppingList;
    }
}
