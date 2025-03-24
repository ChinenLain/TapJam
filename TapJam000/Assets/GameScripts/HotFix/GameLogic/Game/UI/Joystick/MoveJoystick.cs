using System.Collections;
using System.Collections.Generic;
using TEngine;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameLogic
{
    public class MoveJoystick : MonoBehaviour, IDragHandler, IEndDragHandler,IPointerDownHandler,IPointerUpHandler
    {

        public RectTransform joystickBackground;
        public RectTransform joystickHandle;

        public RectTransform joystickParent;

        private Vector2 inputVector;

        public void Init(RectTransform joystickBackground, RectTransform joystickHandle)
        {
            this.joystickBackground = joystickBackground;
            this.joystickHandle = joystickHandle;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                joystickParent, eventData.position, eventData.pressEventCamera, out position);

            inputVector = position / (joystickParent.sizeDelta / 2);
            inputVector = inputVector.magnitude > 1.0f ? inputVector.normalized : inputVector;

            joystickHandle.anchoredPosition = new Vector2(inputVector.x * (joystickParent.sizeDelta.x / 2),
                                                          inputVector.y * (joystickParent.sizeDelta.y / 2));

            GameEvent.Send(UIEventDefine.StickDrag, inputVector);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            joystickHandle.anchoredPosition = Vector2.zero;
            inputVector = Vector2.zero;
            GameEvent.Send(UIEventDefine.StickEndDrag, inputVector);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnEndDrag(eventData);
        }

        public void Start()
        {
            joystickParent = GetComponent<RectTransform>();
        }

        private void FixedUpdate()
        {

        }
    }
}
