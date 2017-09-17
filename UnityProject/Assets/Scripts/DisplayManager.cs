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

        public int DisplayCount
        {
            get
            {
                return _displayList.Count;
            }
        }

        private int index = 0;

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
            Hide(index);
            index = 0;
            Show(index);
        }

        public void Next()
        {
            Hide(index);

            index++;
            if(index == 11)
            {
                index = 1;
            }

            Show(index);
        }

        public void Back()
        {
            Hide(index);
            index--;

            if(index == 0 || index  == -1)
            {
                index = 10;
            }
            Show(index);
        }
    }
}