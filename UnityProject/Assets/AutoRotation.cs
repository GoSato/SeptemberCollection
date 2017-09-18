using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GO
{
    public class AutoRotation : MonoBehaviour
    {
        [SerializeField]
        private float _angle = 90;

        // Use this for initialization
        void Start()
        {

        }

        void Update()
        {
            transform.Rotate(new Vector3(0, 0, _angle) * Time.deltaTime, Space.Self);
        }
    }
}