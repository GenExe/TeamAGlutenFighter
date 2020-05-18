using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserUIFeedback : MonoBehaviour
{
    public Image imageCooldown;
    public float laserCooldown;
    private ShootingScript shootingScript;
    
    

    void Awake()
    {
        shootingScript = GameObject.FindObjectOfType<ShootingScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shootingScript.activateCooldown)
        {
            imageCooldown.fillAmount += 1 / laserCooldown * Time.deltaTime;

            if(imageCooldown.fillAmount >=1)
            {
                if (!shootingScript.shooting)
                {
                    shootingScript.activateCooldown = false;
                }

                imageCooldown.fillAmount = 0;
                
            }
        }
         
    }

   
    }

