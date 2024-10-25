using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TEngine;
using System;

namespace GameLogic
{
    class UIAttackWidget : UIWidget
    {
        private AttackBtn attackWidget;

        private Sprite attack_m;
        private Sprite attack_l;
        private Sprite attack_b;
        private Sprite attack_r;
        private Sprite attack_t;


        #region 脚本工具生成的代码
        private Image m_imgAttackBtn;
        
        protected override void ScriptGenerator()
        {
            m_imgAttackBtn = FindChildComponent<Image>("m_imgAttackBtn");
        }
        #endregion

        #region 事件

        #endregion

        protected override void RegisterEvent()
        {
            AddUIEvent<int,float>(UIEventDefine.AttackClick,OnAttatckClick);
            AddUIEvent<int,float>(UIEventDefine.AttackEndClick, OnAttatckEndClick);
        }

        private void OnAttatckClick(int dir,float holdTime)
        {
            switch(dir)
            {
                case (int)Direction.DirectionType.Left:
                    m_imgAttackBtn.sprite = attack_l;
                    break;
                case (int)Direction.DirectionType.Down:
                    m_imgAttackBtn.sprite = attack_b;
                    break;
                case (int)Direction.DirectionType.Right:
                    m_imgAttackBtn.sprite = attack_r;
                    break;
                case (int)Direction.DirectionType.Up:
                    m_imgAttackBtn.sprite = attack_t;
                    break;
            }
        }

        private void OnAttatckEndClick(int dir, float holdTime)
        {
            m_imgAttackBtn.sprite = attack_m;
            Log.Info($"{ holdTime}");
        }

        protected override void OnCreate()
        {
            attack_m = GameModule.Resource.LoadAsset<Sprite>("attack-1-01");
            attack_l = GameModule.Resource.LoadAsset<Sprite>("attack-1-02");
            attack_b = GameModule.Resource.LoadAsset<Sprite>("attack-1-03");
            attack_r = GameModule.Resource.LoadAsset<Sprite>("attack-1-04");
            attack_t = GameModule.Resource.LoadAsset<Sprite>("attack-1-05");
            AddComponent();
        }

        protected override void OnRefresh()
        {
            AddComponent();
        }

        private void AddComponent()
        {
            attackWidget = gameObject.GetComponent<AttackBtn>();
            if (attackWidget == null)
            {
                attackWidget = gameObject.AddComponent<AttackBtn>();
                attackWidget.Init(gameObject.GetComponent<RectTransform>());
            }
        }
    }
}
