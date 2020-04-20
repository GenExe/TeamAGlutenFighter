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
    void Start()
    {
        shelfFirstTarget = GameObject.Find("/Environment/Shelf3/FirstTarget");
        shelfFinalTarget = GameObject.Find("/Environment/Shelf3/FinalTarget");
    }

    void Update()
    {
          if (isPutObjcetIntoShelf)
          {
            if (instantiateTrail)
            {
                trailEffect = Instantiate(GameObject.Find("/VisualEffects/Trail"), gameObject.transform.position, Quaternion.identity);
                trailEffect.transform.parent = gameObject.transform;
                instantiateTrail = false;
            }
            if (!reachedFinalTarget)
            {
                if (Vector3.Distance(transform.position, shelfFirstTarget.transform.position) > 1 && reachedFirstTarget == false)
                {
                    float step = 0.2f + (movementSpeed * Time.deltaTime);
                    this.transform.position = Vector3.MoveTowards(this.transform.position, shelfFirstTarget.transform.position, step);
                }
                else if (Vector3.Distance(transform.position, shelfFinalTarget.transform.position) > 1 && reachedFinalTarget == false)
                {
                    reachedFirstTarget = true;
                    float step = 0.2f + (movementSpeed * Time.deltaTime);
                    this.transform.position = Vector3.MoveTowards(this.transform.position, shelfFinalTarget.transform.position, step);
                }
            }
        }
    }
}
