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
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                m_RectAttackParent, eventData.position, eventData.pressEventCamera, out position);
            position = GetClosestDirection(position);
            if (position.x == -1) dir = 0;
            else if (position.x == 1) dir = 2;
            else dir = (position.y == -1) ? 1 : 3;
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
            dir = 0;
        }

        
        void Update()
        {
            if(isHolding)
            {
                HoldTime += Time.deltaTime;
            }
        }

        Vector2 GetClosestDirection(Vector2 direction)
        {
            Vector2[] directions = new Vector2[]
            {
            new Vector2(0, 1),   // Up
            new Vector2(1, 0),   // Right
            new Vector2(0, -1),  // Down
            new Vector2(-1, 0)   // Left
            };

            Vector2 closestDirection = directions[0];
            float minAngle = Vector2.Angle(direction, closestDirection);

            for (int i = 1; i < directions.Length; i++)
            {
                float angle = Vector2.Angle(direction, directions[i]);
                if (angle < minAngle)
                {
                    minAngle = angle;
                    closestDirection = directions[i];
                }
            }

            return closestDirection;
        }
    }
}
