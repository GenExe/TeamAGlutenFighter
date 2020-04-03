using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateObject : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isRunning = true;
    public float speed = 0.1f;

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            this.transform.localPosition -= new Vector3(0, 0, speed);
        }
    }
}
