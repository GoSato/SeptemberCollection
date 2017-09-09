using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GO
{
    public enum ColorType
    {
        Red,Blue,Gree,Yellow,Black
    }

    public class FloatingObjectBehavior : MonoBehaviour
    {
        [SerializeField]
        private ColorType _color;

        public ColorType Color
        {
            get
            {
                return _color;
            }
        }
    }
}