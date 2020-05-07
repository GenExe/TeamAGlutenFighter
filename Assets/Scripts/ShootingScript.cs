using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;                       // add this library to use UI elements of Unity


public class ShootingScript : MonoBehaviour
{
    public GameObject FailFx;
    public GameObject SuccessFx;
    public FoodEmitter FoodEmitter;
    public Text ActualScoreText;

    private EventParam _eventParam = new EventParam();

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit,
                Mathf.Infinity))
            {

                if (hit.collider.tag == "goodFood")
                {
                    Destroy(hit.collider.gameObject);
                    Instantiate(SuccessFx, hit.point, Quaternion.identity);
                    string foodName = hit.collider.name;
                    if (FoodEmitter.ShoppingListItems.Any(sh => sh.name + "(Clone)" == foodName))
                    {
                        // increase of score with shopping list bonus
                        _eventParam.ScoreClass = ScoreClass.SHOPPINGLIST;    
                        EventManager.TriggerEvent("ObjectHit", _eventParam);
                        ActualScoreText.text = EventManager.FindObjectOfType<ScoreCalculator>().Score.ToString();

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
                        _eventParam.ScoreClass = ScoreClass.UP;                 
                        EventManager.TriggerEvent("ObjectHit", _eventParam);
                        ActualScoreText.text = EventManager.FindObjectOfType<ScoreCalculator>().Score.ToString();
                    }
                }

                if (hit.collider.tag == "badFood")
                {
                    // decrease of score
                    _eventParam.ScoreClass = ScoreClass.DOWN;
                    EventManager.TriggerEvent("ObjectHit", _eventParam);
                    ActualScoreText.text = EventManager.FindObjectOfType<ScoreCalculator>().Score.ToString();

                    // destroys object when scanned
                    Destroy(hit.collider.gameObject);
                    // instantiates the particle system at the point of hit
                    Instantiate(FailFx, hit.point, Quaternion.identity); 
                }

                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance,
                    Color.yellow);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);

            }

            
        }
    }
}
