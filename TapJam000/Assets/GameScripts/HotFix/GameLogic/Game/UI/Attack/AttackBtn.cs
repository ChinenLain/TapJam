using System;
using System.Collections;
using System.Collections.Generic;
using TEngine;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameLogic
{
    public class AttackBtn : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        private RectTransform m_RectAttackParent;
        private bool isHolding;
        private float HoldTime;
        int dir;

        public void OnDrag(PointerEventData eventData)
        {
            OnPointerDown(eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                m_RectAttackParent, eventData.position, eventData.pressEventCamera, out Vector2 position);
            dir = Direction.GetClosestDirectionType(position);
            HoldTime = 0f;
            isHolding = true;
            GameEvent.Send(UIEventDefine.AttackClick, dir, HoldTime);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isHolding = false;
            GameEvent.Send(UIEventDefine.AttackEndClick, dir, HoldTime);
        }

        
        internal void Init(RectTransform rectTransform)
        {
            m_RectAttackParent = rectTransform;
        }

        void Start()
        {
            isHolding = false;
            HoldTime = 0f;
            dir = (int)Direction.DirectionType.Mid;
        }

        
        void Update()
        {
            if(isHolding)
            {
                HoldTime += Time.deltaTime;
            }
        }
    }
}
