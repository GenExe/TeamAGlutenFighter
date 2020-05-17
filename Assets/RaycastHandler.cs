using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class RaycastHandler : MonoBehaviour
{
    public GameObject FailFx;
    public GameObject SuccessFx;
    public FoodEmitter FoodEmitter;
    public Text Score;
    public GameObject laserOuterBeam;
    public GameObject laserInnerBeam;
    public AudioSource laserSound;

    public float laserUptime;
    private float uptimeCounter;
    private float recoverAt;
    private bool shooting;

    private bool onCooldown;
    private float cooldownCounter;
    public float laserCooldown;

    public GameObject laser;
    private InputDevice device;
    private HapticCapabilities capabilities;
    private bool supportHaptics;
    private bool supportsTrigger;
    private IEnumerator laserCoroutine;

    private void Start()
    {
        uptimeCounter = laserUptime;
        shooting = false;
        onCooldown = false;
        recoverAt = 0;
        //#if !UNITY_EDITOR || !UNITY_STANDALONE
        //        device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        //        supportHaptics = device.TryGetHapticCapabilities(out capabilities);
        //#endif

    }

    public void HandleRaycast(XRBaseInteractable inter)
    {
        //#if !UNITY_EDITOR || !UNITY_STANDALONE
        //        supportHaptics = device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out shooting);
        //        if (!supportsTrigger && Input.GetButton("Fire1"))
        //            shooting = true;
        //#endif

        //if (Input.GetMouseButtonDown(0) && !shooting && !onCooldown)
        //{
        //    shooting = true;
        //    laserSound.Play();
        //}

        //if (!onCooldown && shooting)
        //{
        //    activateLaser();
        //}
        //else
        //{
        //    cooldownLaser();
        //}

        //if (shooting)
        //{
        //    laserOuterBeam.SetActive(true);
        //    laserInnerBeam.SetActive(true);

        //    RaycastHit hit;
        //    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit,
        //        Mathf.Infinity))
        //    {
        EventParam eventParam = new EventParam();
        eventParam.HitPoint = inter.transform.position;
        Transform t = inter.gameObject.transform;

        if (inter.tag == "goodFood")
        {
            Instantiate(SuccessFx, t.position, Quaternion.identity);
            inter.colliders[0].tag = "-";
            t.gameObject.GetComponent<AnimateObjectIntoBasket>().isPutObjcetIntoBasket = true;  //Moves objects into basket, NullReferenceException occurs???
            t.gameObject.GetComponent<AnimateObject>().IsRunning = false;
            string foodName = inter.gameObject.name;

            if (FoodEmitter.ShoppingListItems.Any(sh => sh.name + "(Clone)" == foodName))
            {
                // increase of score with shopping list bonus
                eventParam.ScoreClass = ScoreClass.SHOPPINGLIST;
                EventManager.TriggerEvent("ObjectHit", eventParam);
                Score.text = EventManager.FindObjectOfType<ScoreCalculator>().Score.ToString();

                // strikeout hit fod from shoppinglist 
                foreach (var itemText in FoodEmitter.ItemTextGameObjects)
                {
                    if (itemText.text + "(Clone)" == foodName && !itemText.GetComponent<ShoppingListTextScript>().isChecked)
                    {
                        itemText.fontStyle = FontStyles.Strikethrough;
                        itemText.GetComponent<ShoppingListTextScript>().isChecked = true;
                        break;
                    }
                }
            }
            else
            {
                // increase of score
                eventParam.ScoreClass = ScoreClass.UP;
                EventManager.TriggerEvent("ObjectHit", eventParam);
                Score.text = EventManager.FindObjectOfType<ScoreCalculator>().Score.ToString();
            }
            //Destroy(hit.collider.gameObject);
        }

        if (inter.colliders[0].tag == "badFood")
        {
            Instantiate(FailFx, t.position, Quaternion.identity);

            //Moves objects into shelf, NullReferenceException occurs???
            inter.colliders[0].tag = "-";
            t.gameObject.GetComponent<AnimateObjectIntoShelf>().isPutObjcetIntoShelf = true;
            t.gameObject.GetComponent<AnimateObject>().IsRunning = false;

            // decrease of score
            eventParam.ScoreClass = ScoreClass.DOWN;
            EventManager.TriggerEvent("ObjectHit", eventParam);
            Score.text = EventManager.FindObjectOfType<ScoreCalculator>().Score.ToString();
        }
        EventManager.TriggerEvent("ObjectHit", eventParam);
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
            //laserOuterBeam.SetActive(false);
            //laserInnerBeam.SetActive(false);
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
