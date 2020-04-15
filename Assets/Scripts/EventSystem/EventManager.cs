﻿ using System;
 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 

/*
this struct holds all needed data for scoring events
this struct is ALWAYS passed, even if not needed (generality)
*/
public struct EventParam {
    public int Score;
    public int PointsOfHitObject;
    public Vector3 HitPoint;
}

 public class EventManager : MonoBehaviour
 {
 
     private Dictionary<string, Action<EventParam>> eventDictionary;
     private static EventManager eventManager;

     public static EventManager Instance {
         get {
             if (!eventManager) {
                 eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;
 
                 if (!eventManager) {
                     Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                 } else {
                     eventManager.Init();
                 }
             }
             return eventManager;
         }
     }
 
     void Init() {
         if (eventDictionary == null) {
             eventDictionary = new Dictionary<string, Action<EventParam>>();
         }
     }
 
     public static void StartListening(string eventName, Action<EventParam> listener) {
         Action<EventParam> thisEvent;
         if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
             //Add more event to the existing one
             thisEvent += listener;
 
             //Update the Dictionary
             Instance.eventDictionary[eventName] = thisEvent;
         } else {
             //Add event to the Dictionary for the first time
             thisEvent += listener;
             Instance.eventDictionary.Add(eventName, thisEvent);
         }
     }
 
     public static void StopListening(string eventName, Action<EventParam> listener) {
         if (eventManager == null) return;
         Action<EventParam> thisEvent;
         if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
             //Remove event from the existing one
             thisEvent -= listener;
 
             //Update the Dictionary
             Instance.eventDictionary[eventName] = thisEvent;
         }
     }
 
     public static void TriggerEvent(string eventName, EventParam eventParam) {
         Action<EventParam> thisEvent = null;
         if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
             thisEvent.Invoke(eventParam);
         }
     }
 }
