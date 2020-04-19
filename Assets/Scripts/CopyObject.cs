using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyObject : MonoBehaviour
{
    public GameObject prefab;
    public bool createCopy = false;

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
    }
}
