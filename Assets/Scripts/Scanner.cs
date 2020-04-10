using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                       // add this library to use UI elements of Unity
using System;


public class Scanner : MonoBehaviour
{
    public GameObject failFX;
    public GameObject successFX;
    public Text scoreText;

    private Action<EventParam> updatedScoreListener;

    void OnEnable () {
        updatedScoreListener = new Action<EventParam>(RefreshScore);
        EventManager.StartListening ("ScoreUpdated", updatedScoreListener);
    }

    private void OnDisable()
    {
        EventManager.StopListening("ScoreUpdated", updatedScoreListener);
    }


    void RefreshScore(EventParam e) {
        scoreText.text = e.Score.ToString();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit,
                Mathf.Infinity))
            {
                EventParam hitObjData = new EventParam();
                hitObjData.HitPoint = hit.point;

                if (hit.collider.tag == "goodFood")
                {
                    Destroy(hit.collider.gameObject);
                    Instantiate(successFX, hit.point, Quaternion.identity);
                    hitObjData.PointsOfHitObject = 50;
                }

                if (hit.collider.tag == "badFood")
                {
                    Destroy(hit.collider.gameObject); // destroys object when scanned
                    Instantiate(failFX, hit.point, Quaternion.identity); // instantiates the particle system at the point of hit
                    hitObjData.PointsOfHitObject = -50;
                }

                EventManager.TriggerEvent ("ObjectHit", hitObjData);


                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance,
                    Color.yellow);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);

            }
        }
    }

}
