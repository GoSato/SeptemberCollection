using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GO
{
    /// <summary>
    /// Playerがエリア内に入ると、Markerに関連するコンポーネントをActiveにする
    /// </summary>
    public class Activator : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> _activateObjectList = new List<GameObject>();
        
        public Action OnActive;
        public Action OnDeactive;

        /// <summary>
        /// プレイヤーがアクティブエリア内にいるか
        /// </summary>
        private bool _isInsideActiveArea = false;
        public bool IsInsideActiveArea
        {
            get { return _isInsideActiveArea; }
            private set { _isInsideActiveArea = value; }
        }

        private void Start()
        {
            Deactivate();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(CanReact(other.gameObject))
            {
                Activate();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(CanReact(other.gameObject))
            {
                Deactivate();
            }
        }

        private bool CanReact(GameObject obj)
        {
            if(obj.CompareTag("Player"))
            {
                return true;
            }
            return false;
        }

        private void Activate()
        {
            Debug.Log("Activate : " + transform.parent.name);
            IsInsideActiveArea = true;

            for (int i = 0; i < _activateObjectList.Count; ++i)
            {
                _activateObjectList[i].SetActive(true);
            }

            if (OnActive != null)
            {
                OnActive.Invoke();
            }
        }

        private void Deactivate()
        {
            Debug.Log("Deactivate : " + transform.parent.name);
            IsInsideActiveArea = false;

            for (int i = 0; i < _activateObjectList.Count; ++i)
            {
                _activateObjectList[i].SetActive(false);
            }

            if (OnDeactive != null)
            {
                OnDeactive.Invoke();
            }
        }
    }
}
