using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreCalculator : MonoBehaviour {

    private int _score = 0;

    private Action<EventParam> objectHitListener;
    private Action<EventParam> restartListener;
    private Action<EventParam> destroyListener;


    public int Score {
        get {
            return _score;
        }
    }

    void OnEnable () {
        objectHitListener = new Action<EventParam>(ObjectHit);
        restartListener = new Action<EventParam>(Restart);
        destroyListener = new Action<EventParam>(Destroy);
        EventManager.StartListening ("ObjectHit", objectHitListener);
        EventManager.StartListening ("Restart", restartListener);
        EventManager.StartListening ("Destroy", destroyListener);
    }

    void OnDisable () {
        EventManager.StopListening ("ObjectHit", objectHitListener);
        EventManager.StopListening ("Restart", restartListener);
        EventManager.StopListening ("Destroy", destroyListener);
    }


    void Restart(EventParam e) {
        _score = 0;
    }

    void ObjectHit(EventParam e) {
        Debug.Log ("ObjectHit was called!");
        _score += e.PointsOfHitObject;


        EventParam e2 = new EventParam();
        e2.Score =  _score;

        EventManager.TriggerEvent("ScoreUpdated", e2);
    }


    void Destroy (EventParam e) {
        Debug.Log ("Destroy was called!");
    }
}
