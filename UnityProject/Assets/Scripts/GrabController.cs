using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

namespace GO
{
    /// <summary>
    /// 対象を掴んで、回転、移動を行うコンポーネント
    /// </summary>
    public class GrabController : MonoBehaviour, IInputHandler, ISourceStateHandler
    {
        [SerializeField]
        private float _rotateSpeed = 1f;

        private GameObject _targetObj;  // 回転させるGameObjecet
        private bool _isHold;   // つかんでいるか
        private IInputSource _currentInputSource;
        private uint _currentInputSourceId;
        private Vector3 _prevPos;

        #region ### MonoBehavior ###

        private void Start()
        {
            _targetObj = transform.root.gameObject;
        }

        private void Update()
        {
            if(!_isHold)
            {
                return;
            }

            Vector3 handPos;
            _currentInputSource.TryGetPosition(_currentInputSourceId, out handPos);
            handPos = Camera.main.transform.InverseTransformDirection(handPos);

            var diff = _prevPos - handPos;
            _prevPos = handPos;
            _targetObj.transform.Rotate(new Vector3(0, diff.x * 360f * _rotateSpeed, 0f), Space.World);
            //_targetObj.transform.position -= new Vector3(0f, diff.y * _moveSpeed, 0f);
        }

        #endregion

        #region ### IInputHandler ###

        public void OnInputDown(InputEventData eventData)
        {
            if(!eventData.InputSource.SupportsInputInfo(eventData.SourceId, SupportedInputInfo.Position))
            {
                return;
            }

            if(_isHold)
            {
                return;
            }

            _isHold = true;

            _currentInputSource = eventData.InputSource;
            _currentInputSourceId = eventData.SourceId;

            _currentInputSource.TryGetPosition(_currentInputSourceId, out _prevPos);
            _prevPos = Camera.main.transform.InverseTransformDirection(_prevPos);
        }

        public void OnInputUp(InputEventData eventData)
        {
            if(!_isHold)
            {
                return;
            }

            _isHold = false;
            InputManager.Instance.PopModalInputHandler();
        }

        #endregion

        #region ### ISourceStateHandler ###

        public void OnSourceDetected(SourceStateEventData eventData)
        {

        }

        public void OnSourceLost(SourceStateEventData eventData)
        {
            if (!_isHold)
            {
                return;
            }

            _isHold = false;
            InputManager.Instance.PopModalInputHandler();
        }

        #endregion
    }
}