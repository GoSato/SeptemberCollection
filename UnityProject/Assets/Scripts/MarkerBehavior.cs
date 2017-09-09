using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GO
{
    public class MarkerBehavior : MonoBehaviour
    {
        [SerializeField]
        private ColorType _color;

        private Activator _activator;

        private void Awake()
        {
            _activator = GetComponentInChildren<Activator>();
            _activator.OnActive += EnableFloatingObject;
            _activator.OnDeactive += DisableFloatingObaject;
        }

        private void EnableFloatingObject()
        {
            FloatingObjectManager.Instance.OnActivate(_color);
        }

        private void DisableFloatingObaject()
        {
            FloatingObjectManager.Instance.OnDeactivate(_color);
        }
    }
}