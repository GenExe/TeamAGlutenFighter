using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateObjectIntoShelf : MonoBehaviour
{
    public bool isPutObjcetIntoShelf = false;
    public float movementSpeed;

    public GameObject shelfFirstTarget;
    public GameObject shelfFinalTarget;
    private bool reachedFirstTarget = false;
    private bool reachedFinalTarget = false;

    private GameObject trailEffect;
    private bool instantiateTrail = true;

    private static bool shelfFull = false;

    private float objectGab = 2.8f;
    private int lineSpace = 6;
    private static int BasketcountObjInLine = 0;

    private float shelfHeightCoordinate = 3.85f;
    private int shelfHeight = 3;
    private static int BasketcountObjInHeight = 0;

    private float shelfStartingPositionX;
    private float shelfStartingPositionY;
    private float shelfStartingPositionZ;

    void Start()
    {
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
                    float step = 0.2f + (movementSpeed * Time.deltaTime);
                    this.transform.position = Vector3.MoveTowards(this.transform.position, shelfFirstTarget.transform.position, step);
                }
                else if (Vector3.Distance(transform.position, shelfFinalTarget.transform.position) > 0.8f && reachedFinalTarget == false)
                {
                    reachedFirstTarget = true;
                    float step = 0.2f + (movementSpeed * Time.deltaTime);
                    this.transform.position = Vector3.MoveTowards(this.transform.position, shelfFinalTarget.transform.position, step);
                }
                else
                {
                    reachedFinalTarget = true;
                    //Destroy(gameObject);
                    if (BasketcountObjInLine < this.lineSpace)
                    {
                        shelfFinalTarget.transform.position = new Vector3(shelfFinalTarget.transform.position.x + this.objectGab, shelfFinalTarget.transform.position.y, shelfFinalTarget.transform.position.z);
                        BasketcountObjInLine++;
                    }
                    else if (BasketcountObjInHeight < this.shelfHeight)
                    {
                        BasketcountObjInLine = 0;
                        BasketcountObjInHeight++;
                        shelfStartingPositionY -= shelfHeightCoordinate;
                        shelfStartingPositionX -= lineSpace * objectGab;
                        shelfFinalTarget.transform.position = new Vector3(shelfStartingPositionX, shelfStartingPositionY, shelfStartingPositionZ);
                    }
                }
            }
        }
        if (BasketcountObjInHeight == this.shelfHeight && BasketcountObjInLine > this.lineSpace)
        {
            shelfFull = true;
        }
    }
}
