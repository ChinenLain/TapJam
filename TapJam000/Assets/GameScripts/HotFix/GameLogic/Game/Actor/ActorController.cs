using System;
using System.Collections;
using System.Collections.Generic;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class ActorController : MonoBehaviour
    {
        public GameObject actor;
        private float speed = 0.08f;
        void Start()
        {
            GameEvent.AddEventListener<Vector2>(UIEventDefine.StickDrag,OnStickDrag);
            GameEvent.AddEventListener<Vector2>(UIEventDefine.StickDrag, OnStickEndDrag);
            GameEvent.AddEventListener<Vector2>(UIEventDefine.AttackClick, OnAttackClick);
            GameEvent.AddEventListener<Vector2>(UIEventDefine.AttackEndClick, OnAttackEndClick);
        }

        private void OnStickDrag(Vector2 input)
        {
            Vector3 moveDiection = new Vector3(-input.y, 0, input.x);
            actor.transform.Translate(moveDiection * speed,Space.World);
        }

        private void OnStickEndDrag(Vector2 vector)
        {

        }

        private void OnAttackClick(Vector2 vector)
        {

        }

        private void OnAttackEndClick(Vector2 vector)
        {

        }
    }
}
