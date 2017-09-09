using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GO
{
    public class FloatingObjectController : MonoBehaviour
    {
        [SerializeField]
        private Activator _activator;

        private ColorType _previousColor;
        private ColorType _newColor;

        private void Start()
        {
            SetActiveAll(false);
        }

        private  void SetActiveAll(bool active)
        {
            foreach (var pair in FloatingObjectManager.Instance.FloatingObjecets)
            {
                foreach (var n in pair.Value)
                {
                    n.gameObject.SetActive(active);
                }
            }
        }

        private void SetActive(ColorType color, bool active)
        {
            foreach (var n in FloatingObjectManager.Instance.FloatingObjecets[color])
            {
                n.gameObject.SetActive(active);
            }

            if (active)
            {
                _previousColor = _newColor;
                _newColor = color;
            }
        }
    }
}