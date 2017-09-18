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

        private Vector3 _initialScale;

        private void Awake()
        {
            _initialScale = transform.localScale;
        }

        [ContextMenu("Appear")]
        public void Appear(Action OnAppear = null)
        {
            transform.DOScale(_initialScale, _appearDuration).OnComplete(() => OnAppear());
        }

        [ContextMenu("Disappear")]
        public void Disappear(Action OnDisappear = null)
        {
            transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), _disappearDuration).OnComplete(() => OnDisappear());
        }

        public void ForceDisappear()
        {
            transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        }
    }
}