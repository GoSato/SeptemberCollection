using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GO
{
    [System.Serializable]
    public struct DisplayList
    {
        public int ID;
        public List<GameObject> ObjectList;
    }

    public class DisplayManager : SingletonMonoBehavior<DisplayManager>
    {
        [SerializeField]
        private List<DisplayList> _displayList;
        [SerializeField]
        private List<Activator> _activatorList;

        private int _prevId;
        private int _currentId;

        private void Start()
        {
            foreach(var activator in _activatorList)
            {
                activator.OnActivate += Show;
            }
        }

        private void Show(int id)
        {
            if (id == _currentId) return;

            _prevId = _currentId;
            _currentId = id;

            SetActive(_prevId, false);
            SetActive(id, true);
        }

        private void Hide(int id)
        {
            if (id != _currentId) return;

            _currentId = _prevId;
            SetActive(id, false);

        }

        private void SetActive(int id, bool active)
        {
            foreach (var display in _displayList)
            {
                if (id == display.ID)
                {
                    foreach (var obj in display.ObjectList)
                    {
                        obj.SetActive(active);
                    }
                }
            }
        }
    }
}