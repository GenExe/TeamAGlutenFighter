using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.EventSystem
{
    class ScoreUIManager : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _multiplierValueText = null;
        [SerializeField]
        private GameObject _hitScoreInfoObject = null;
        [SerializeField]
        private Color32 _positiveScoreColor = Color.green;
        [SerializeField]
        private Color32 _negativeScoreColor = Color.red;

        private Action<EventParam> _updateMultiplierListener;
        private Action<EventParam> _spawnHitInfoListener;

        private Vector3 _multiplierOriginalScale;
        private bool _isShrinking = false;
        private float _shrinkSpeed = 0.1f;

        void Start()
        {
            _multiplierOriginalScale = (Vector3)_multiplierValueText?.transform.localScale;
        }

        void Update()
        {
            UpdateMultiplierScale();
        }

        void OnEnable()
        {
            _updateMultiplierListener = new Action<EventParam>(UpdateMultiplier);
            _spawnHitInfoListener = new Action<EventParam>(SpawnHitInfo);
            EventManager.StartListening("MultiplierUpdated", _updateMultiplierListener);
            EventManager.StartListening("SpawnHitInfo", _spawnHitInfoListener);
        }

        void OnDisable()
        {
            EventManager.StopListening("MultiplierUpdated", _updateMultiplierListener);
            EventManager.StopListening("SpawnHitInfo", _spawnHitInfoListener);
        }

        private void UpdateMultiplier(EventParam param)
        {
            if (_multiplierValueText != null)
            {
                _multiplierValueText.text = param.Score.ToString();
                _multiplierValueText.transform.localScale += Vector3.one * 10f;
                _isShrinking = true;
            }
        }

        private void SpawnHitInfo(EventParam param)
        {
            GameObject tempHitInfo = Instantiate(_hitScoreInfoObject) as GameObject;
            tempHitInfo.transform.localPosition += param.HitPoint;

            if (param.Score > 0)
            {
                tempHitInfo.GetComponentInChildren<TMP_Text>().SetText($"+{param.Score}");
                tempHitInfo.GetComponentInChildren<TMP_Text>().color = _positiveScoreColor;
            }
            else
            {
                tempHitInfo.GetComponentInChildren<TMP_Text>().SetText(param.Score.ToString());
                tempHitInfo.GetComponentInChildren<TMP_Text>().color = _negativeScoreColor;
            }

            Destroy(tempHitInfo.gameObject, 0.5f);
        }

        private void UpdateMultiplierScale()
        {
            if (_isShrinking && _multiplierValueText?.transform.localScale != _multiplierOriginalScale)
            {
                _multiplierValueText.transform.localScale -= Vector3.one * _shrinkSpeed;
            }
            else
            {
                _isShrinking = false;
                // set original scale again, as there seems some offset after transforming scale
                _multiplierOriginalScale = _multiplierValueText.transform.localScale;
            }
        }
    }
}
