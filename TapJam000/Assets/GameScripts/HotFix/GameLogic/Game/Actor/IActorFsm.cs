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
        /// 放到MonoBehaviour的帧更新生命周期中
        /// 用于自动更新状态
        /// </summary>
        protected void UpdateState()
        {
            if(currentStateName == "null")
            {
                if(DefaultStateName == "null")
                {
                    Debug.Log("未定义默认状态");
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
        /// 切换当前状态
        /// </summary>
        /// <param name="stateName">状态名</param>
        /// <param name="force">是否强制切换（不管当前状态动画是否可跳过）</param>
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
        /// 设置下一状态
        /// </summary>
        /// <param name="name">状态名</param>
        /// <param name="priority">状态优先级</param>
        protected void SetNextState(string name,int priority = 0)
        {
            if(priority >= nextState_priority)
            {
                nextStateName = name;
                nextState_priority = priority;
            }
        }

        /// <summary>
        /// 获取上一状态
        /// </summary>
        /// <returns>状态名</returns>
        protected string GetPreState()
        {
            return preStateName;
        }

        /// <summary>
        /// 获取当前状态
        /// </summary>
        /// <returns>状态名</returns>
        protected string GetCurrentState()
        {
            return currentStateName;
        }

        /// <summary>
        /// 当前状态改变时的回调函数
        /// </summary>
        /// <param name="oldState">旧状态名</param>
        /// <param name="newState">新状态名</param>
        protected abstract void OnStateChanged(string oldState, string newState);

        /// <summary>
        /// 当前状态动画播放至结尾时的回调函数
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
