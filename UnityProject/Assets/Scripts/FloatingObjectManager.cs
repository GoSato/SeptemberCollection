using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GO
{
    public class FloatingObjectManager : SingletonMonoBehavior<FloatingObjectManager>
    {
        [SerializeField]
        private Activator _activator;

        private MultiDictionary<ColorType, FloatingObjectBehavior> _floatinObjects = new MultiDictionary<ColorType, FloatingObjectBehavior>();
        private FloatingObjectController _controller;

        public MultiDictionary<ColorType, FloatingObjectBehavior> FloatingObjecets
        {
            get
            {
                return _floatinObjects;
            }
        }

        private void Awake()
        {
            FindFloatingObjects();
            _controller = GetComponent<FloatingObjectController>();
        }

        private void Start()
        {
            _controller.SetActiveAll(false);
            _activator.OnActive += OnStart;
        }

        private void FindFloatingObjects()
        {
            foreach(var target in FindObjectsOfType<FloatingObjectBehavior>())
            {
                _floatinObjects.Add(target.Color, target);
            }
        }

        public void OnActivate(ColorType color)
        {
            _controller.SetActive(color, true);
        }

        public void OnDeactivate(ColorType color)
        {
            _controller.SetActive(color, false);
        }

        public void OnStart()
        {
            _controller.SetActiveAll(true);
        }

        /// <summary>
        /// TODO : 使わない可能性あり
        /// </summary>
        public void OnForceQuite()
        {
            _controller.SetActiveAll(false);
        }
    }
}