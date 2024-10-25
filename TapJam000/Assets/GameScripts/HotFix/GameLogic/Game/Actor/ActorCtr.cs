using System;
using System.Collections;
using System.Collections.Generic;
using TEngine;
using UnityEngine;
using UnityEngine.Windows;
using static GameLogic.Direction;

namespace GameLogic
{
    public class ActorCtr : IActorFsm
    {
        private Rigidbody rb;

        private float move_speed = 3.5f;
        private bool moving;

        private Vector2 move_pos;

        private bool hitting;
        private int hit_pos;
        private bool trigger;
        private float trigger_time;
        void Start()
        {
            moving = false;

            hitting = false;

            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();

            DefaultStateName = "ch-stand";
            animations.Add(new AnimationInfo("ch-stand",(int)ActorStateType.Idle));
            animations.Add(new AnimationInfo("ch-moveleft", (int)ActorStateType.Move));
            animations.Add(new AnimationInfo("ch-moveright", (int)ActorStateType.Move));
            animations.Add(new AnimationInfo("ch-moveup", (int)ActorStateType.Move));
            animations.Add(new AnimationInfo("ch-movedown", (int)ActorStateType.Move));
            animations.Add(new AnimationInfo("ch-hitleft", (int)ActorStateType.Attack, false,false));
            animations.Add(new AnimationInfo("ch-hitright", (int)ActorStateType.Attack, false,false));
            animations.Add(new AnimationInfo("ch-hitup", (int)ActorStateType.Attack, false, false));
            animations.Add(new AnimationInfo("ch-hitdown", (int)ActorStateType.Attack, false, false));


            GameEvent.AddEventListener<Vector2>(UIEventDefine.StickDrag,OnStickDrag);
            GameEvent.AddEventListener<Vector2>(UIEventDefine.StickEndDrag, OnStickEndDrag);
            GameEvent.AddEventListener<int,float>(UIEventDefine.AttackClick, OnAttackClick);
            GameEvent.AddEventListener<int,float>(UIEventDefine.AttackEndClick, OnAttackEndClick);
        }

        private void SetMoveState(Vector2 input)
        {
            int dir = GetClosestDirectionType(input);
            switch (dir)
            {
                case (int)DirectionType.Left:
                    if (GetCurrentState() == "ch-moveleft") return;
                    SetNextState("ch-moveleft");
                    break;
                case (int)DirectionType.Right:
                    if (GetCurrentState() == "ch-moveright") return;
                    SetNextState("ch-moveright");
                    break;
                case (int)DirectionType.Up:
                    if (GetCurrentState() == "ch-moveup") return;
                    SetNextState("ch-moveup");
                    break;
                case (int)DirectionType.Down:
                    if (GetCurrentState() == "ch-movedown") return;
                    SetNextState("ch-movedown");
                    break;
            }
        }

        private void SetAttackState(int dir)
        {
            switch (dir)
            {
                case (int)DirectionType.Left:
                    SetNextState("ch-hitleft",1);
                    break;
                case (int)DirectionType.Right:
                    SetNextState("ch-hitright",1);
                    break;
                case (int)DirectionType.Up:
                    SetNextState("ch-hitup", 1);
                    break;
                case (int)DirectionType.Down:
                    SetNextState("ch-hitdown",1);
                    break;
            }
        }

        private void OnStickDrag(Vector2 input)
        {
            moving = true;
            move_pos = input;
            SetMoveState(move_pos);
        }

        private void OnStickEndDrag(Vector2 input)
        {
            moving = false;
            rb.velocity = Vector3.zero;
            SetNextState(DefaultStateName);
        }

        private void OnAttackClick(int dir,float holdTime)
        {
            hitting = true;
            hit_pos = dir;
            trigger = false;
            trigger_time = 0f;
        }

        private void OnAttackEndClick(int dir, float holdTime)
        {
            hitting=false;
        }

        private void FixedUpdate()
        {

        }

        private void Update()
        {
            if (hitting)
            {
                
                if(!trigger)
                {
                    SetAttackState(hit_pos);
                }
                else
                {
                    trigger_time += Time.deltaTime;
                    if(trigger_time > 0.4f) SetAttackState(hit_pos);
                }
            }

            if (moving)
            {
                if (GetAnimationInfo(GetCurrentState()).stateType == (int)ActorStateType.Move)
                {
                    rb.velocity = new Vector3(move_pos.x, 0, move_pos.y) * move_speed;
                }
                else
                {
                    rb.velocity = Vector3.zero;
                }

                SetMoveState(move_pos);
            }
            
            UpdateState();
        }

        protected override void OnStateChanged(string oldState, string newState)
        {
            if(GetAnimationInfo(newState).stateType == (int)ActorStateType.Attack)
            {
                trigger = true;
                trigger_time = 0f;
            }
        }

        protected override void OnStateEnd(string state)
        {
            
        }
    }
}
