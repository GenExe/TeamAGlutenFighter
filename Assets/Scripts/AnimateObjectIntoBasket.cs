using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateObjectIntoBasket : MonoBehaviour
{
    public bool isPutObjcetIntoBasket = false;
    public float movementSpeed;

    public GameObject shoppingBasketFirstTarget;
    public GameObject shoppingBasketFinalTarget;
    private bool reachedFirstTarget = false;
    private bool reachedFinalTarget = false;

    private bool basketFull = false;

    void Update()
    {
        if (isPutObjcetIntoBasket)
        {
            if (!this.basketFull)
            {
                if (!reachedFinalTarget)
                {
                    if (Vector3.Distance(transform.position, shoppingBasketFirstTarget.transform.position) > 0.01f && reachedFirstTarget == false)
                    {
                        float step = 0.2f + (movementSpeed * Time.deltaTime);
                        this.transform.position = Vector3.MoveTowards(this.transform.position, shoppingBasketFirstTarget.transform.position, step);
                    }
                    else if (Vector3.Distance(transform.position, shoppingBasketFinalTarget.transform.position) > 0.01f && reachedFinalTarget == false)
                    {
                        reachedFirstTarget = true;
                        float step = 0.2f + (movementSpeed * Time.deltaTime);
                        this.transform.position = Vector3.MoveTowards(this.transform.position, shoppingBasketFinalTarget.transform.position, step);
                    }
                }
            }
        }
    }
}
