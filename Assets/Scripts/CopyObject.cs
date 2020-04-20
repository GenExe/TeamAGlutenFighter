using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyObject : MonoBehaviour
{
    public GameObject prefab;
    public bool createCopy = false;

    public GameObject shoppingBasketFirstTarget;
    public GameObject shoppingBasketFinalTarget;

    public GameObject shelfFirstTarget;
    public GameObject shelfFinalTarget;

    private void Update()
    {
        if(createCopy)
        {
            doCreate();
            Destroy(this.transform.gameObject);
        }
    }

    public void doCreate()
    {
        createCopy = false;
        GameObject copy = Instantiate(prefab);
        copy.transform.gameObject.SetActive(true);
        copy.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        if (copy.transform.tag == "good")
        {
            copy.transform.gameObject.GetComponent<AnimateObjectIntoBasket>().shoppingBasketFirstTarget = shoppingBasketFirstTarget;
            copy.transform.gameObject.GetComponent<AnimateObjectIntoBasket>().shoppingBasketFinalTarget = shoppingBasketFinalTarget;
        } else if(copy.transform.tag == "bad")
        {
            copy.transform.gameObject.GetComponent<AnimateObjectIntoShelf>().shelfFirstTarget = shelfFirstTarget;
            copy.transform.gameObject.GetComponent<AnimateObjectIntoShelf>().shelfFinalTarget = shelfFinalTarget;
        }
    }
}
