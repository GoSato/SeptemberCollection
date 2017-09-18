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
        public List<ScaleController> ScalingObjectList;
    }

    public class DisplayManager : SingletonMonoBehavior<DisplayManager>
    {
        [SerializeField]
        private List<DisplayList> _displayList;

        public Action<int> OnNext;
        public Action<int> OnBack;

        private bool _canReact;

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

        private void Start()
        {
            HideAll();
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
                if(_displayList[i].ID == 0)
                {
                    return;
                }
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

                    foreach(var scaling in display.ScalingObjectList)
                    {
                        _canReact = false;

                        if (active)
                        {
                            scaling.gameObject.SetActive(true);
                            scaling.Appear(() => _canReact = true);
                        }
                        else
                        {
                            scaling.Disappear(() => {
                                scaling.gameObject.SetActive(false);
                                _canReact = true;
                                });
                        }
                    }
                }
            }
        }

        [ContextMenu("Reset")]
        public void Reset()
        {
            if (!_canReact)
            {
                return;
            }

            Hide(_index);
            _index = 0;
            Show(_index);
        }

        public void Next()
        {
            if (!_canReact)
            {
                return;
            }

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
            if (!_canReact)
            {
                return;
            }

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