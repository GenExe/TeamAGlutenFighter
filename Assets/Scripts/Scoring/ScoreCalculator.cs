using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreCalculator : MonoBehaviour {
    [SerializeField]
    private int _positiveHitBaseValue = 50;
    [SerializeField]
    private int _negativeHitBaseValue = 50;
    [SerializeField]
    private int _shoppingListBonus = 25;

    private int _score = 0;
    private int _multiplier = 1;
    private int _multiplyCounter;

    private Action<EventParam> _objectHitListener;
    private Action<EventParam> _restartListener;
    private Action<EventParam> _destroyListener;

    public int Score {
        get {
            return _score;
        }

        private set
        {
            _score = value < 0 ? 0 : value;
            EventParam e2 = new EventParam();
            e2.Score = _score;
            EventManager.TriggerEvent("ScoreUpdated", e2);
        }
    }

    public int Multiplier
    {
        get
        {
            return _multiplier;
        }

        private set
        {
            _multiplier = value;
            var updatedMultiplier = new EventParam();
            updatedMultiplier.Score = Multiplier;
            EventManager.TriggerEvent("MultiplierUpdated", updatedMultiplier);
        }
    }

    void OnEnable () {
        _objectHitListener = new Action<EventParam>(ObjectHit);
        _restartListener = new Action<EventParam>(Restart);
        _destroyListener = new Action<EventParam>(Destroy);
        EventManager.StartListening ("ObjectHit", _objectHitListener);
        EventManager.StartListening ("Restart", _restartListener);
        EventManager.StartListening ("Destroy", _destroyListener);
    }

    void OnDisable () {
        EventManager.StopListening ("ObjectHit", _objectHitListener);
        EventManager.StopListening ("Restart", _restartListener);
        EventManager.StopListening ("Destroy", _destroyListener);
    }

    void Restart(EventParam e) {
        _score = 0;
    }

    void ObjectHit(EventParam e) {
        Debug.Log ("ObjectHit was called!");
        var tempScore = 0;
        var bonus = 0;

        switch (e.ScoreClass)
        {
            default:
            case null:
            case ScoreClass.NONE:
                return;
            case ScoreClass.SHOPPINGLIST:
                tempScore = _positiveHitBaseValue;
                bonus = _shoppingListBonus;
                break;
            case ScoreClass.UP:
                tempScore = _positiveHitBaseValue;
                break;
            case ScoreClass.DOWN:
                tempScore = -_negativeHitBaseValue;
                break;
        }
        
        var multiplier = EvaluateMultiplier(tempScore);

        tempScore *= multiplier;
        tempScore += bonus;

        EventParam spawnHitInfo = new EventParam();
        spawnHitInfo.Score = tempScore;
        spawnHitInfo.HitPoint = e.HitPoint;
        EventManager.TriggerEvent("SpawnHitInfo", spawnHitInfo);

        Score += tempScore;
    }

    void Destroy (EventParam e) {
        Debug.Log ("Destroy was called!");
    }

    private int EvaluateMultiplier(int score)
    {
        int tempMultiplier = Multiplier;

        if (score > 0)
        {
            _multiplyCounter++;

            if (_multiplyCounter >= 20)
            {
                tempMultiplier = 8;
            }
            else if (_multiplyCounter >= 10)
            {
                tempMultiplier = 4;
            }
            else if (_multiplyCounter >= 5)
            {
                tempMultiplier = 2;
            }
        }
        else
        {
            tempMultiplier = 1;
            _multiplyCounter = 0;
        }

        if (tempMultiplier != Multiplier)
        {
            Multiplier = tempMultiplier;
        }

        return tempMultiplier;
    }
}
