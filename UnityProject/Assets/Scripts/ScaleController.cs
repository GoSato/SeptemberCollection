using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace GO
{
    public class ScaleController : MonoBehaviour
    {
        [SerializeField]
        private float _appearDuration;
        [SerializeField]
        private float _disappearDuration;
        [SerializeField]
        private float _rotateDuration;
        [SerializeField]
        private float _rotationAngle;
        [SerializeField]
        private bool _autoRotate = true;

        private AutoRotation _autoRotation;
        private Vector3 _initialScale;
        private Quaternion _initialRot;

        private void Awake()
        {
            _initialScale = transform.localScale;
            _initialRot = transform.localRotation;
            _autoRotation = GetComponentInChildren<AutoRotation>();
        }

        [ContextMenu("Appear")]
        public void Appear(Action OnAppear = null)
        {
            transform.DOScale(_initialScale, _appearDuration).OnComplete(() =>
            {
                OnAppear();
            });

            if (_autoRotate)
            {
                transform.DORotate(_initialRot.eulerAngles, _rotateDuration).OnComplete(() => {
                    if(_autoRotation != null)
                    {
                        _autoRotation.enabled = true;
                    }
                });
            }
        }

        [ContextMenu("Disappear")]
        public void Disappear(Action OnDisappear = null)
        {
            transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), _disappearDuration).OnComplete(() =>
            {
                OnDisappear();
            });

            if (_autoRotate)
            {
                transform.DORotate(_initialRot.eulerAngles - new Vector3(0, _rotationAngle, 0), _rotateDuration).OnComplete(() => 
                {
                    if(_autoRotation != null)
                    {
                        _autoRotation.enabled = false;
                    }
                });
            }
        }

        public void ForceDisappear()
        {
            transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        }
    }
}