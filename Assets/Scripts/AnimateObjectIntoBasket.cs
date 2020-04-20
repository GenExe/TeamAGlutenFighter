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

    private GameObject trailEffect;
    private bool instantiateTrail = true;
    void Start()
    {
        shoppingBasketFirstTarget = GameObject.Find("/Environment/ShoppingBasket/FirstTarget"); 
        shoppingBasketFinalTarget = GameObject.Find("/Environment/ShoppingBasket/FinalTarget");
    }

    void Update()
    {
        if (isPutObjcetIntoBasket)
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
                    float step = 0.2f + (movementSpeed * Time.deltaTime);
                    this.transform.position = Vector3.MoveTowards(this.transform.position, shoppingBasketFirstTarget.transform.position, step);
                }
                else if (Vector3.Distance(transform.position, shoppingBasketFinalTarget.transform.position) > 0.5 && reachedFinalTarget == false)
                {
                    reachedFirstTarget = true;
                    float step = 0.2f + (movementSpeed * Time.deltaTime);
                    this.transform.position = Vector3.MoveTowards(this.transform.position, shoppingBasketFinalTarget.transform.position, step);
                }
            }
        }
    }
}
