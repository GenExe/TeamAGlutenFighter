﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                       // add this library to use UI elements of Unity


public class Scanner : MonoBehaviour
{
    public int Score = 0;
    public GameObject failFX;
    public GameObject successFX;
    public Text scoreText;

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
                    Instantiate(successFX, hit.point, Quaternion.identity);

                }

                if (hit.collider.tag == "badFood")
                {
                    Score--; // decrease of score
                    Destroy(hit.collider.gameObject); // destroys object when scanned
                    Instantiate(failFX, hit.point,
                        Quaternion.identity); // instantiates the particle system at the point of hit
                }

                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance,
                    Color.yellow);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);

            }

            scoreText.text = Score.ToString();
        }
    }
}
