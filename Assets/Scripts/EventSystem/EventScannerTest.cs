using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventScannerTest : MonoBehaviour {


    void Update () {
        if (Input.GetKeyDown ("q")) {
            EventManager.TriggerEvent ("Destroy");
        }

        if (Input.GetKeyDown ("1")) {
            EventManager.TriggerEvent ("GlutenfreeHit");
        }

        if (Input.GetKeyDown ("0")) {
            EventManager.TriggerEvent ("GlutenHit");
        }
    }
}
