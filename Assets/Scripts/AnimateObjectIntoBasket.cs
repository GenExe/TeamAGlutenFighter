using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateObjectIntoBasket : MonoBehaviour
{
    public bool isPutObjcetIntoBasket = false;
    private float movementSpeed;

    private GameObject shoppingBasketFirstTarget;
    private GameObject shoppingBasketFinalTarget;
    private bool reachedFirstTarget = false;
    private bool reachedFinalTarget = false;

    private GameObject trailEffect;
    private bool instantiateTrail = true;

    private static float basketStartingPositionX;
    private static float basketStartingPositionY;
    private static float basketStartingPositionZ;

    private float objectGab = 1;
    private float objectGabRowAddition = 0.3f;

    private float basketHeightCoordinate = 0.9f;
    private int basketHeight = 3;
    public static int countObjInHeight = 0;

    private int lineSpace = 2;
    private int rowSpace = 7;
    private static int countObjInLine = 0;
    private static int countObjInRow = 0;

    private static bool basketFull = false;
    void Start()
    {
        movementSpeed = 10;

        shoppingBasketFirstTarget = GameObject.Find("/Environment/ShoppingBasket/FirstTarget"); 
        shoppingBasketFinalTarget = GameObject.Find("/Environment/ShoppingBasket/FinalTarget");

        basketStartingPositionX = shoppingBasketFinalTarget.transform.position.x;
        basketStartingPositionY = shoppingBasketFinalTarget.transform.position.y;
        basketStartingPositionZ = shoppingBasketFinalTarget.transform.position.z;
    }

    void Update()
    {
        if (isPutObjcetIntoBasket && !basketFull)
        {
            if (instantiateTrail)
            {
                trailEffect = Instantiate(GameObject.Find("/VisualEffects/Trail"), gameObject.transform.position, Quaternion.identity);
                trailEffect.transform.parent = gameObject.transform;
                instantiateTrail = false;
            }
            if (!reachedFinalTarget)
            {
                if (Vector3.Distance(transform.position, shoppingBasketFirstTarget.transform.position) > 0.5 && reachedFirstTarget == false)
                {
                    float step = movementSpeed * Time.deltaTime;
                    this.transform.position = Vector3.MoveTowards(this.transform.position, shoppingBasketFirstTarget.transform.position, step);
                }
                else if (Vector3.Distance(transform.position, shoppingBasketFinalTarget.transform.position) > 0.5 && reachedFinalTarget == false)
                {
                    reachedFirstTarget = true;
                    float step = movementSpeed * Time.deltaTime;
                    this.transform.position = Vector3.MoveTowards(this.transform.position, shoppingBasketFinalTarget.transform.position, step);
                } else
                {
                    reachedFinalTarget = true;
                    //Destroy(gameObject);
                    if (countObjInLine < this.lineSpace)
                    {
                        shoppingBasketFinalTarget.transform.position = new Vector3(shoppingBasketFinalTarget.transform.position.x + this.objectGab, shoppingBasketFinalTarget.transform.position.y, shoppingBasketFinalTarget.transform.position.z);
                        countObjInLine++;
                    }
                    else if (countObjInRow < this.rowSpace)
                    {
                        shoppingBasketFinalTarget.transform.position = new Vector3(shoppingBasketFinalTarget.transform.position.x - (this.objectGab * countObjInLine), shoppingBasketFinalTarget.transform.position.y, shoppingBasketFinalTarget.transform.position.z - (this.objectGab + objectGabRowAddition));
                        countObjInLine = 0;
                        countObjInRow++;
                    }
                    else if (countObjInHeight < this.basketHeight)
                    {
                        basketStartingPositionX -= countObjInLine * objectGab;
                        basketStartingPositionY += basketHeightCoordinate;
                        basketStartingPositionZ += rowSpace * (objectGab + objectGabRowAddition);
                        shoppingBasketFinalTarget.transform.position = new Vector3(basketStartingPositionX, basketStartingPositionY, basketStartingPositionZ);
                        countObjInRow = 0;
                        countObjInLine = 0;
                        countObjInHeight++;
                    }
                }
            }
        }
        if (countObjInHeight == this.basketHeight && countObjInLine == this.lineSpace)
        {
            basketFull = true;
        }
    }
}
