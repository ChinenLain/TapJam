using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace GameLogic
{
    public abstract class IActorFsm : MonoBehaviour
    {
        protected Animator animator;
        protected AnimatorStateInfo animatorInfo;

        protected ActorStateType stateType;

        protected List<AnimationInfo> animations = new List<AnimationInfo>();

        private string preStateName = "";
        private string currentStateName = "null";
        private string nextStateName = "null";
        private int nextState_priority = 0;

        protected string DefaultStateName = "null";

        private void ToNextState(bool force = false)
        {
            if (nextStateName == "null")
            {
                AnimationInfo info = GetAnimationInfo(currentStateName);
                if (!info.loop) 
                {
                    if(ChangeState(DefaultStateName, force))
                    {
                        nextStateName = "null";
                        nextState_priority = 0;
                    } 
                } 
            }
            else if(ChangeState(nextStateName, force))
            {
                nextStateName = "null";
                nextState_priority = 0;
            }
        }

        /// <summary>
        /// �ŵ�MonoBehaviour��֡��������������
        /// �����Զ�����״̬
        /// </summary>
        protected void UpdateState()
        {
            if(currentStateName == "null")
            {
                if(DefaultStateName == "null")
                {
                    Debug.Log("δ����Ĭ��״̬");
                }
                else
                {
                    currentStateName = DefaultStateName;
                }
            }

            
            animatorInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (animatorInfo.normalizedTime >= 0.96f) OnStateEnd(currentStateName);

            ToNextState();

            //Debug.Log($"{preStateName},{currentStateName},{nextStateName}");
        }

        /// <summary>
        /// �л���ǰ״̬
        /// </summary>
        /// <param name="stateName">״̬��</param>
        /// <param name="force">�Ƿ�ǿ���л������ܵ�ǰ״̬�����Ƿ��������</param>
        /// <returns></returns>
        protected bool ChangeState(string stateName,bool force = false)
        {
            AnimationInfo currentinfo = GetAnimationInfo(currentStateName);
            AnimationInfo info = GetAnimationInfo(stateName);
            if (info == null) return false;
            if (force || currentinfo.skip)
            {
                preStateName = currentStateName;
                currentStateName = stateName;
                animator.Play(stateName,0,0f);
                stateType = (ActorStateType)info.stateType;
                OnStateChanged(preStateName,currentStateName);
                return true;
            }
            else
            {
                animatorInfo = animator.GetCurrentAnimatorStateInfo(0);
                if(animatorInfo.normalizedTime >= 0.96f) 
                {
                    preStateName = currentStateName;
                    currentStateName = stateName;
                    animator.Play(stateName,0,0f);
                    stateType = (ActorStateType)info.stateType;
                    OnStateChanged(preStateName, currentStateName);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// ������һ״̬
        /// </summary>
        /// <param name="name">״̬��</param>
        /// <param name="priority">״̬���ȼ�</param>
        protected void SetNextState(string name,int priority = 0)
        {
            if(priority >= nextState_priority)
            {
                nextStateName = name;
                nextState_priority = priority;
            }
        }

        /// <summary>
        /// ��ȡ��һ״̬
        /// </summary>
        /// <returns>״̬��</returns>
        protected string GetPreState()
        {
            return preStateName;
        }

        /// <summary>
        /// ��ȡ��ǰ״̬
        /// </summary>
        /// <returns>״̬��</returns>
        protected string GetCurrentState()
        {
            return currentStateName;
        }

        /// <summary>
        /// ��ǰ״̬�ı�ʱ�Ļص�����
        /// </summary>
        /// <param name="oldState">��״̬��</param>
        /// <param name="newState">��״̬��</param>
        protected abstract void OnStateChanged(string oldState, string newState);

        /// <summary>
        /// ��ǰ״̬������������βʱ�Ļص�����
        /// </summary>
        /// <param name="state"></param>
        protected abstract void OnStateEnd(string state);


        protected AnimationInfo GetAnimationInfo(string name) 
        {
            foreach(AnimationInfo info in animations)
            {
                if(info.name == name)
                {
                    return info;
                }
            }
            return null;
        }
    }

    public class AnimationInfo
    {
        public string name;
        public bool skip;
        public int stateType;
        public bool loop;
        public AnimationInfo(string name, int stateType,bool skip = true,bool loop = true)
        {
            this.name = name;
            this.skip = skip;
            this.stateType = stateType;
            this.loop = loop;
        }
    }
}
