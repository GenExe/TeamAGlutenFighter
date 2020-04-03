using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FoodEmitter : MonoBehaviour
{
    public bool isRunning;
    public GameObject[] food;
    public float spawnInterval;        // Spawn interval
    public float emitterWidth;          // Half width of emitter
    public float startDelay;
    public float lifeTime = 5;              // time emitted goodFood is active in the scene
    public List<Transform> spawnPoints = new List<Transform>();
    [HideInInspector]
    public List<GameObject> instantiatedFoodObjects = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        isRunning = true;
        InvokeRepeating("Spawn", startDelay, spawnInterval);
    }

    void Spawn()
    {
        if (!isRunning) return;

        GameObject emittedFood;

        // if spawnPoints is empty the emitterWidth will count
        if (!spawnPoints.Any())
        {
            instantiatedFoodObjects.Add(emittedFood = Instantiate(food[Random.Range(0, food.Length)], new Vector3(Random.Range(-emitterWidth, emitterWidth), 0, 0) + transform.position, Quaternion.identity));
        }
        // if not, a random SpawnPoint is selected each time
        else
        {
            instantiatedFoodObjects.Add(emittedFood = Instantiate(food[Random.Range(0, food.Length)], spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity));
        }

        Destroy(emittedFood, lifeTime);
    }



}
