using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GO
{
    /// <summary>
    /// Playerの接触時に、設定したオブジェクトのActiveを切り替えるコンポーネント
    /// </summary>
    public class Activator : MonoBehaviour
    {
        [SerializeField]
        private int _id;
        [SerializeField]
        private List<GameObject> _activateObjectList = new List<GameObject>();
        [SerializeField]
        [Tooltip("Player接触時、一度のみコールバックを実行")]
        private bool _enterOnly = false;

        private bool _isEntered = false; // Playerが接触中か否か

        public Action<int> OnActivate;
        public Action<int> OnDeactivate;

        private void Start()
        {
            Deactivate();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (CanReact(other.gameObject))
            {
                Activate();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_enterOnly)
            {
                return;
            }

            if (CanReact(other.gameObject))
            {
                Deactivate();
            }
        }

        private bool CanReact(GameObject obj)
        {
            if (obj.CompareTag("Player"))
            {
                return true;
            }
            return false;
        }

        private void Activate()
        {
            if (_isEntered)
            {
                return;
            }

            Debug.Log("Activate : " + _id);

            foreach(var target in GetComponentsInChildren<IActivatable>())
            {
                target.OnActivate();
            }

            for (int i = 0; i < _activateObjectList.Count; ++i)
            {
                _activateObjectList[i].SetActive(true);
            }

            if (OnActivate != null)
            {
                OnActivate.Invoke(_id);
            }

            _isEntered = true;
        }

        private void Deactivate()
        {
            if(!_isEntered)
            {
                return;
            }

            Debug.Log("Deactivate : " + _id);

            foreach (var target in GetComponentsInChildren<IActivatable>())
            {
                target.OnDeactivate();
            }

            for (int i = 0; i < _activateObjectList.Count; ++i)
            {
                _activateObjectList[i].SetActive(false);
            }

            if (OnDeactivate != null)
            {
                OnDeactivate.Invoke(_id);
            }

            _isEntered = false;
        }

        public void Reset()
        {
            Deactivate();
        }
    }
}
