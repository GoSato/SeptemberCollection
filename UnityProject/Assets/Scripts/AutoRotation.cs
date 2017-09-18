using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GO
{
    public class AutoRotation : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _rotation;

        void Update()
        {
            transform.Rotate(_rotation * Time.deltaTime, Space.Self);
        }
    }
}