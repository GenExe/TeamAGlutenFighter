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

    private bool shelfFull = false;

    void Update()
    {
          if (isPutObjcetIntoShelf)
          {
            if (!this.shelfFull)
            {
                if (!reachedFinalTarget)
                {
                    if (Vector3.Distance(transform.position, shelfFirstTarget.transform.position) > 0.01f && reachedFirstTarget == false)
                    {
                        float step = 0.2f + (movementSpeed * Time.deltaTime);
                        this.transform.position = Vector3.MoveTowards(this.transform.position, shelfFirstTarget.transform.position, step);
                    }
                    else if (Vector3.Distance(transform.position, shelfFinalTarget.transform.position) > 0.01f && reachedFinalTarget == false)
                    {
                        reachedFirstTarget = true;
                        float step = 0.2f + (movementSpeed * Time.deltaTime);
                        this.transform.position = Vector3.MoveTowards(this.transform.position, shelfFinalTarget.transform.position, step);
                    }
                }
            }
        }
    }
}
