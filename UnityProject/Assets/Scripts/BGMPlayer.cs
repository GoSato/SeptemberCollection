using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GO
{
    public class BGMPlayer : MonoBehaviour, IActivatable
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void OnActivate()
        {
            Play();
        }

        public void OnDeactivate()
        {
            Stop();
        }

        private void Play()
        {
            _audioSource.Play();
        }

        private void Stop()
        {
            _audioSource.Stop();
        }
    }
}