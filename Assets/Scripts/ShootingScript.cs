﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;                       // add this library to use UI elements of Unity


public class ShootingScript : MonoBehaviour
{
    public int Score = 0;
    public GameObject FailFx;
    public GameObject SuccessFx;
    public Text ScoreText;
    public FoodEmitter FoodEmitter;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit,
                Mathf.Infinity))
            {
                if (hit.collider.tag == "goodFood")
                {
                    Score++;
                    Destroy(hit.collider.gameObject);
                    Instantiate(SuccessFx, hit.point, Quaternion.identity);
                    string foodName = hit.collider.name;
                    if (FoodEmitter.ShoppingListItems.Any(sh => sh.name + "(Clone)" == foodName))
                    {
                        foreach (var itemText in FoodEmitter.ItemTextGameObjects)
                        {
                            if (itemText.text + "(Clone)" == foodName && !itemText.GetComponent<ShoppingListTextScript>().isChecked)
                            {
                                itemText.fontStyle = FontStyles.Strikethrough;
                                itemText.GetComponent<ShoppingListTextScript>().isChecked = true;
                                break;
                            }
                        }
                    }
                }

                if (hit.collider.tag == "badFood")
                {
                    Score--; // decrease of score
                    Destroy(hit.collider.gameObject); // destroys object when scanned
                    Instantiate(FailFx, hit.point,
                        Quaternion.identity); // instantiates the particle system at the point of hit
                }

                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance,
                    Color.yellow);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);

            }

            ScoreText.text = Score.ToString();
        }
    }
}
