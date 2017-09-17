using UnityEngine;
using UnityEngine.VR.WSA.Persistence;
using UnityEngine.VR.WSA;
using HoloToolkit.Unity.InputModule;
using System.Collections.Generic;

namespace GO
{
    public class PersistoMatic : MonoBehaviour, IInputClickHandler
    {
        [SerializeField]
        private List<GameObject> _anchorObjects;
        [SerializeField]
        private float _distanceFromPlayer = 3f;
        //[SerializeField]
        //private Activator _activator;

        private GrabController _grabController;

        public string ObjectAnchorStoreName;

        WorldAnchorStore anchorStore;

        [Tooltip("アンカーを空間に配置可能な状態か")]
        bool _canPlacing = false;

        #region ### MonoBehavior

        void Start()
        {
            Debug.Log("WorldAnchorStore.GetAsync()");
            //InputManager.Instance.PushFallbackInputHandler(gameObject);
            WorldAnchorStore.GetAsync(AnchorStoreReady);
            _grabController = GetComponent<GrabController>();
        }

        void Update()
        {
            // アンカーをカメラに追従させる
            if (_canPlacing)
            {
                gameObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * _distanceFromPlayer;
            }
        }

        #endregion

        void AnchorStoreReady(WorldAnchorStore store)
        {
            anchorStore = store;
            _canPlacing = true;

            Debug.Log("looking for " + ObjectAnchorStoreName);
            string[] ids = anchorStore.GetAllIds();
            for (int index = 0; index < ids.Length; index++)
            {
                Debug.Log(ids[index]);
                if (ids[index] == ObjectAnchorStoreName)
                {
                    WorldAnchor wa = anchorStore.Load(ids[index], gameObject);
                    _canPlacing = false;
                    break;
                }
            }
        }

        #region ### IInputClickHandler ###

        public void OnInputClicked(InputClickedEventData eventData)
        {
            if (anchorStore == null)
            {
                return;
            }

            // 空間にアンカーを固定
            if (_canPlacing)
            {
                WorldAnchor attachingAnchor = gameObject.AddComponent<WorldAnchor>();

                // 空間に正しく配置されている(ロストしていない)
                if (attachingAnchor.isLocated)
                {
                    Debug.Log("Saving persisted position immediately");
                    bool saved = anchorStore.Save(ObjectAnchorStoreName, attachingAnchor);
                    Debug.Log("saved: " + saved);
                }
                else
                {
                    attachingAnchor.OnTrackingChanged += AttachingAnchor_OnTrackingChanged;
                }

                SetActiveAnchorObjects(false);
                _grabController.enabled = false;
            }

            // 固定を解除
            else
            {
                WorldAnchor anchor = gameObject.GetComponent<WorldAnchor>();
                if (anchor != null)
                {
                    DestroyImmediate(anchor);
                }

                string[] ids = anchorStore.GetAllIds();
                for (int index = 0; index < ids.Length; index++)
                {
                    Debug.Log(ids[index]);
                    if (ids[index] == ObjectAnchorStoreName)
                    {
                        bool deleted = anchorStore.Delete(ids[index]);
                        Debug.Log("deleted: " + deleted);
                        break;
                    }
                }

                SetActiveAnchorObjects(true);
                //_activator.Reset();
                //DisplayManager.Instance.Reset();
                _grabController.enabled = true;
            }

            _canPlacing = !_canPlacing;
        }

        #endregion

        private void AttachingAnchor_OnTrackingChanged(WorldAnchor self, bool located)
        {
            if (located)
            {
                Debug.Log("Saving persisted position in callback");
                bool saved = anchorStore.Save(ObjectAnchorStoreName, self);
                Debug.Log("saved: " + saved);
                self.OnTrackingChanged -= AttachingAnchor_OnTrackingChanged;
            }
        }

        private void SetActiveAnchorObjects(bool active)
        {
            for (int i = 0; i < _anchorObjects.Count; i++)
            {
                _anchorObjects[i].SetActive(active);
            }
        }
    }
}