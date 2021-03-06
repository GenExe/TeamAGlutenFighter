﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateObjectIntoShelf : MonoBehaviour
{
    public bool isPutObjcetIntoShelf = false;
    private float movementSpeed;

    private GameObject shelfFirstTarget;
    private GameObject shelfFinalTarget;
    private bool reachedFirstTarget = false;
    private bool reachedFinalTarget = false;

    private GameObject trailEffect;
    private bool instantiateTrail = true;

    private static bool shelfFull = false;

    private float objectGab = 2.8f;
    private int lineSpace = 6;
    private static int countObjInLine = 0;

    private float shelfHeightCoordinate = 3.85f;
    private int shelfHeight = 3;
    private static int countObjInHeight = 0;

    private static float shelfStartingPositionX;
    private static float shelfStartingPositionY;
    private static float shelfStartingPositionZ;

    void Start()
    {
        movementSpeed = 10;

        shelfFirstTarget = GameObject.Find("/Environment/Shelf3/FirstTarget");
        shelfFinalTarget = GameObject.Find("/Environment/Shelf3/FinalTarget");

        shelfStartingPositionX = shelfFinalTarget.transform.position.x;
        shelfStartingPositionY = shelfFinalTarget.transform.position.y;
        shelfStartingPositionZ = shelfFinalTarget.transform.position.z;
    }

    void Update()
    {
          if (isPutObjcetIntoShelf && !shelfFull)
          {
            if (instantiateTrail)
            {
                trailEffect = Instantiate(GameObject.Find("/VisualEffects/Trail"), gameObject.transform.position, Quaternion.identity);
                trailEffect.transform.parent = gameObject.transform;
                instantiateTrail = false;
            }
            if (!reachedFinalTarget)
            {
                if (Vector3.Distance(transform.position, shelfFirstTarget.transform.position) > 0.8f && reachedFirstTarget == false)
                {
                    float step = movementSpeed * Time.deltaTime;
                    this.transform.position = Vector3.MoveTowards(this.transform.position, shelfFirstTarget.transform.position, step);
                }
                else if (Vector3.Distance(transform.position, shelfFinalTarget.transform.position) > 0.8f && reachedFinalTarget == false)
                {
                    reachedFirstTarget = true;
                    float step = movementSpeed * Time.deltaTime;
                    this.transform.position = Vector3.MoveTowards(this.transform.position, shelfFinalTarget.transform.position, step);
                }
                else
                {
                    reachedFinalTarget = true;
                    //Destroy(gameObject);
                    if (countObjInLine < this.lineSpace)
                    {
                        shelfFinalTarget.transform.position = new Vector3(shelfFinalTarget.transform.position.x + this.objectGab, shelfFinalTarget.transform.position.y, shelfFinalTarget.transform.position.z);
                        countObjInLine++;
                    }
                    else if (countObjInHeight < this.shelfHeight)
                    {
                        shelfStartingPositionY -= shelfHeightCoordinate;
                        shelfStartingPositionX = shelfStartingPositionX - (countObjInLine * objectGab);
                        shelfFinalTarget.transform.position = new Vector3(shelfStartingPositionX, shelfStartingPositionY, shelfStartingPositionZ);
                        countObjInLine = 0;
                        countObjInHeight++;
                    }
                }
            }
        }
        if (countObjInHeight == this.shelfHeight && countObjInLine > this.lineSpace)
        {
            shelfFull = true;
        }
    }
}
