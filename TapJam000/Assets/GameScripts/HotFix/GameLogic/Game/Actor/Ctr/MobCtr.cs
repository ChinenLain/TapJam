using System;
using TEngine;
using UnityEngine;
using UnityEngine.Windows;
using static GameLogic.Direction;

namespace GameLogic
{
    public class MobCtr : IActorFsm
    {
        private Rigidbody rb;

        public float blood = 100.0f;
        public float hurt = 10.0f;

        public GameObject target;
        public float move_speed = 3.5f;

        public GameObject weapon;

        public void Init(GameObject target, float blood = 100.0f, float hurt = 10.0f,float speed = 3.5f)
        {
            this.target = target;
            this.blood = blood;
            this.hurt = hurt;
            move_speed = speed;
        }

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();

            weapon = transform.Find("weapon").gameObject;
            
            animations.Add(new AnimationInfo("mob-wait", (int)ActorStateType.Idle));
            animations.Add(new AnimationInfo("mob-walk", (int)ActorStateType.Move));
            animations.Add(new AnimationInfo("mob-hit", (int)ActorStateType.Attack));
            animations.Add(new AnimationInfo("mob-hitby", (int)ActorStateType.None,false,false));

            DefaultStateName = "mob-wait";

            weapon.SetActive(false);
        }


        private void FixedUpdate()
        {
            
        }

        private void Update()
        {
            UpdateState();
            rb.velocity = Vector3.zero;
            float distance = (target.transform.position - transform.position).magnitude;
            if(GetAnimationInfo(GetCurrentState()).stateType == (int)ActorStateType.Attack)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
                    weapon.SetActive(false);
                else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.3f)
                    weapon.SetActive(true);
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.96f)
                    return;
                
                
            }
            if (distance > 0.8f)
            {
                if (GetAnimationInfo(GetCurrentState()).stateType != (int)ActorStateType.Move)
                {
                    int randomWait = UnityEngine.Random.Range(0, 150);
                    if (randomWait != 5) return;
                    SetNextState("mob-walk");
                }
                else
                {
                    int randomWait = UnityEngine.Random.Range(0, 150);
                    if (randomWait == 10)
                    {
                        SetNextState("mob-hit");
                    }
                    Vector3 m = (target.transform.position - transform.position).normalized;
                    rb.velocity = new Vector3(m.x, 0, m.z) * move_speed;
                }

            }
            else
            {
                SetNextState("mob-hit");
            }
            
        }

        public void HitBy(float damage)
        {
            blood -= damage;
            SetNextState("mob-hitby",1);
            if(blood <= 0f)
            {
                GameSystem.Instance.CurrentMapMobNum--;
                Destroy(gameObject);
            }
        }

        public void OnWeaponHit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log($"HitActor:{hurt}");
                GameEvent.Send(ActorEventDefine.HurtBy, hurt);
            }
        }

        protected override void OnStateChanged(string oldState, string newState)
        {

        }

        protected override void OnStateEnd(string state)
        {

        }
    }
}
