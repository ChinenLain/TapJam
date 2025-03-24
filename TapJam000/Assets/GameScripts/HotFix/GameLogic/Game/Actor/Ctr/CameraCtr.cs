using GameBase;
using System.Collections;
using System.Collections.Generic;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class CameraCtr : MonoBehaviour
    {
        private enum StateType
        {
            STATIC = 0,
            FOLLOW = 1,
            MOVE = 2,
        }

        private StateType state;

        Camera main_camera;

        public Transform target;
        private Vector3 target_position;

        private float move_speed;

        private Vector3 cameraVelocity;

        private float camera_size;

        void Start()
        {
            state = StateType.STATIC;
            cameraVelocity = Vector3.zero;
            main_camera = transform.FindChildComponent<Camera>("Main Camera");
            Follow(GameObject.Find("MainActor")?.GetComponent<Transform>());
        }

        
        void Update()
        {
            if (main_camera == null) 
                return;

            if (state == StateType.FOLLOW && target != null)
            {
                if (target == null)
                    return;
                SetPostion(target.position, true);
            }
            else if (state == StateType.MOVE)
            {
                if (target == null)
                    return;
                SetPostion(target_position, true);
            }
        
        }

        public void Follow(Transform _target,float _speed = 0.4f)
        {
            target = _target;
            move_speed = _speed;
            state = StateType.FOLLOW;  
        }

        public void StopFollow()
        {
            state = StateType.STATIC;
            target = null;
        }

        public void Move(Vector3 _target,bool isSmooth = false, float _speed = 0.4f)
        {
            
            if (isSmooth)
            {
                move_speed = _speed;
                target_position = _target;
                state = StateType.MOVE;
            }
            else
            {
                SetPostion(_target);
                state = StateType.STATIC;
            } 
        }

        private void SetPostion(Vector3 position,bool isSmooth = false)
        {
            if (!isSmooth)
            {
                transform.position = position;
            }
            else
            {
                transform.position = Vector3.SmoothDamp(transform.position, position,
                    ref cameraVelocity,move_speed);
            }
        }
    }
}
