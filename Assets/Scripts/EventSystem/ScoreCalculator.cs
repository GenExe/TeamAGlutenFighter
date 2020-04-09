using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreCalculator : MonoBehaviour {

    private int _score = 0;

    private int _streakCounter = 0;

    public int Score {
        get {
            return _score;
        }
    }

    void OnEnable () {
        EventManager.StartListening ("GlutenfreeHit", GlutenfreeHit);
        EventManager.StartListening ("GlutenHit", GlutenHit);
        EventManager.StartListening ("Restart", Restart);
        EventManager.StartListening ("Destroy", Destroy);
        EventManager.StartListening("GameOver", GameOver);
    }

    void OnDisable () {
        EventManager.StopListening ("GlutenfreeHit", GlutenfreeHit);
        EventManager.StopListening ("GlutenHit", GlutenHit);
        EventManager.StopListening ("Restart", Restart);
        EventManager.StopListening ("Destroy", Destroy);
        EventManager.StopListening("GameOver", GameOver);
    }

    void GameOver() {
        // dunno what to do here yet
        // high score list or something?
    }

    void Restart() {
        _score = 0;
        _streakCounter = 0;
    }

    void GlutenfreeHit () {
        Debug.Log ("GlutenfreeHit was called!");
        // do some score calc magic here

        if(_streakCounter >= 5) {
            _score += 100;
        } else {
            _score += 50;
        }

        _streakCounter += 1;

        if(_streakCounter >= 5) {
            //TODO: enable streak animation
        }
        // trigger event 
        EventManager.TriggerEvent("ScoreUpdated");
    }

    void GlutenHit () {
        Debug.Log ("GlutenHit was called!");
        // do some score calc magic here

        _score -= 50;
        _streakCounter = 0;

        EventManager.TriggerEvent("ScoreUpdated");
    }

    void Destroy () {
        Debug.Log ("Destroy was called!");
    }
}
