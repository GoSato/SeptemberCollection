﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GO
{
    public class SmokeController : MonoBehaviour
    {
        private bool _isEnable = true;
        private MeshRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            DisAppear();
            DisplayManager.Instance.OnNext += Appear;
        }

        public void SetActive()
        {
            if (!_isEnable)
            {
                Appear();
            }
            else
            {
                DisAppear();
            }
        }

        public void Appear(int id = 0)
        {
            if (!_isEnable)
            {
                _renderer.enabled = true;
                _isEnable = true;
            }
        }

        public void DisAppear()
        {
            if (_isEnable)
            {
                _renderer.enabled = false;
                _isEnable = false;
            }
        }
    }
}