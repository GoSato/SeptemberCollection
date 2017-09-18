using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

        public Action<int> OnNext;
        public Action<int> OnBack;

        public int DisplayCount
        {
            get
            {
                return _displayList.Count;
            }
        }

        private int _index = 0;

        public int CurrentIndex
        {
            get
            {
                return _index;
            }
        }

        private void Show(int id)
        {
            SetActive(id, true);
        }

        private void Hide(int id)
        {
            SetActive(id, false);
        }

        private void HideAll()
        {
            for (int i = 0; i < _displayList.Count; i++)
            {
                SetActive(_displayList[i].ID, false);
            }
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

        [ContextMenu("Reset")]
        public void Reset()
        {
            Hide(_index);
            _index = 0;
            Show(_index);
        }

        public void Next()
        {
            Hide(_index);

            _index++;
            if(_index == 11)
            {
                _index = 1;
            }

            Show(_index);

            if (OnNext != null)
            {
                OnNext.Invoke(_index);
            }
        }

        public void Back()
        {
            Hide(_index);
            _index--;

            if(_index == 0 || _index  == -1)
            {
                _index = 10;
            }
            Show(_index);

            if(OnBack != null)
            {
                OnBack.Invoke(_index);
            }
        }
    }
}