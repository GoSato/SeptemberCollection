using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GO
{
    public class FloatingObjectManager : SingletonMonoBehavior<FloatingObjectManager>
    { 
        private MultiDictionary<ColorType, FloatingObjectBehavior> _floatinObjects = new MultiDictionary<ColorType, FloatingObjectBehavior>();

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
        }
        

        private void FindFloatingObjects()
        {
            foreach(var target in FindObjectsOfType<FloatingObjectBehavior>())
            {
                _floatinObjects.Add(target.Color, target);
            }
        }
    }
}