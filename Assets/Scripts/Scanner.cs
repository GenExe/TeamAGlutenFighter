using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                       // add this library to use UI elements of Unity
using UnityEngine.XR;

public class Scanner : MonoBehaviour
{
    public int Score = 0;
    public GameObject failFX;
    public GameObject successFX;
    public Text scoreText;
    private GameObject laserOuterBeam;
    private GameObject laserInnerBeam;

    //public float cooldownTime = 2;

    public float laserUptime;
    private float uptimeCounter;
    private float recoverAt;
    private bool shooting;

    private bool onCooldown;
    private float cooldownCounter;
    public float laserCooldown;

    //VR
    //public GameObject laser;
    //private InputDevice device;
    //private HapticCapabilities capabilities;
    //private bool supportHaptics;
    //private bool supportsTrigger;
    //private IEnumerator laserCoroutine;
    //VR

    private void Start()
    {
        laserOuterBeam = GameObject.Find("/PlayerFov/Barcode Scanner/LaserOuterBeam");
        laserInnerBeam = GameObject.Find("/PlayerFov/Barcode Scanner/LaserInnerBeam");
        uptimeCounter = laserUptime;
        shooting = false;
        onCooldown = false;
        recoverAt = 0;

        //VR
        //device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        //supportHaptics = device.TryGetHapticCapabilities(out capabilities);
        //VR
    }


    // Update is called once per frame
    void Update()
    {
        //VR
        bool shooting = false;
        //supportHaptics = device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out shooting);
        //if (!supportsTrigger && Input.GetButton("Fire1"))
        //  shooting = true;
        //VR

        if (Input.GetMouseButtonDown(0) && !shooting && !onCooldown)
        {
            shooting = true;
        }

        if (!onCooldown)
        {
            activateLaser();
        }
        else
        {
            cooldownLaser();
        }

        if (shooting)
        {
            laserOuterBeam.SetActive(true);
            laserInnerBeam.SetActive(true);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit,
                Mathf.Infinity))
            {
                if (hit.collider.tag == "goodFood")
                {
                    Score++;
                    hit.collider.tag = "-";
                    hit.transform.gameObject.GetComponent<AnimateObjectIntoBasket>().isPutObjcetIntoBasket = true;  //Moves objects into basket, NullReferenceException occurs???
                    hit.transform.gameObject.GetComponent<AnimateObject>().isRunning = false;
                    //Destroy(hit.collider.gameObject);
                    Instantiate(successFX, hit.point, Quaternion.identity);
                }

                if (hit.collider.tag == "badFood")
                {
                    Score--; // decrease of score
                    hit.collider.tag = "-";
                    hit.transform.gameObject.GetComponent<AnimateObjectIntoShelf>().isPutObjcetIntoShelf = true;  //Moves objects into shelf, NullReferenceException occurs???
                    hit.transform.gameObject.GetComponent<AnimateObject>().isRunning = false;
                    //Destroy(hit.collider.gameObject); // destroys object when scanned
                    Instantiate(failFX, hit.point, Quaternion.identity); // instantiates the particle system at the point of hit
                }

                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);

            }
            scoreText.text = Score.ToString();
        }
    }

    void activateLaser()
    {
        if (recoverAt < uptimeCounter)
        {
            uptimeCounter -= 1 * Time.deltaTime;
        }
        else
        {
            shooting = false;
            laserOuterBeam.SetActive(false);
            laserInnerBeam.SetActive(false);
            uptimeCounter = laserUptime;
            onCooldown = true;
        }
    }

    void cooldownLaser()
    {
        if (recoverAt < cooldownCounter)
        {
            cooldownCounter -= 1 * Time.deltaTime;
        }
        else
        {
            cooldownCounter = laserCooldown;
            onCooldown = false;
        }
    }
}
