using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer lr;
    private float textureOffset = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //create line
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //update line pos
        lr.SetPosition(0, transform.position);

        //if line hits, shorten line to hit object
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
            }
        }
        else
        {
            lr.SetPosition(1, transform.forward * 5000);
        }

        //pan texture
        textureOffset -= Time.deltaTime * 2f;
        if(textureOffset < -10f)
        {
            textureOffset += 10f;
        }
        lr.sharedMaterials[1].SetTextureOffset("_MainTex", new Vector2(textureOffset, 0f));
    }
}
