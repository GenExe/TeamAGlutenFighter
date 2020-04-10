using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateObject : MonoBehaviour
{
    // Start is called before the first frame update
    public bool IsRunning = true;
    public float Speed = 0.1f;

    // Update is called once per frame
    void Update()
    {
        if (IsRunning)
        {
            this.transform.localPosition -= new Vector3(0, 0, Speed);
        }
    }
}
