using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using GameBase;
using System.Collections;
using System.Collections.Generic;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    [Update]
    public class GameSystem : BehaviourSingleton<GameSystem>
    {
        public int CurrentMapMobNum = 7;
        bool trigger = false;
        private GameObject mainActor;
        private GameObject boss;

        private float time00 = 0f;

        public async UniTaskVoid LoadGame()
        {
            CurrentMapMobNum = 7;
            trigger = false;
            await UniTask.Yield();

            GameModule.UI.ShowUI<UIGameWindow>();

            MapManager.Instance.ChangeChapter(0);
            MapManager.Instance.ChangeMap(7);

            MapManager.Instance.passable = false;

            GameObject actor = GameModule.Resource.LoadAsset<GameObject>("MainActor");
            actor = Object.Instantiate(actor, Vector3.zero, Quaternion.identity);
            actor.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
            mainActor = actor;
            GameObject.Find("Game_Camera").GetComponent<CameraCtr>().target = actor.transform;
            MapManager.Instance.actor = mainActor;

            GameObject mob = GameModule.Resource.LoadAsset<GameObject>("Mob");

            for(int i = 0;i <= 6;i++)
            {
                float x = Random.Range(-3,3);
                float z = Random.Range(-3,3);
                mob = Object.Instantiate(mob, new Vector3(x,0.7f,z), Quaternion.identity);
                mob.GetComponent<MobCtr>().Init(actor,50,0);
                mob.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
            }
            
            
            GameEvent.Send(GameEventDefine.DialogueTaskStart,0, "ch00", 0,5);

        }

        public void DestroyGame()
        {

        }



        public override void Update()
        {
            if (CurrentMapMobNum == 0 && !trigger)
            {
                MapManager.Instance.passable = true;
                trigger = true;
                GameObject mob = GameModule.Resource.LoadAsset<GameObject>("Mob");
                mob.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                mob = Object.Instantiate(mob, Vector3.zero, Quaternion.identity);

                mob.GetComponent<MobCtr>().Init(mainActor, 1000, 25,0.01f);
                boss = mob;
                GameObject.Find("Game_Camera").GetComponent<CameraCtr>().target = mob.transform;
            }
            if (CurrentMapMobNum == 0 && trigger)
            {
                time00 += Time.deltaTime;
                if(time00 > 1.5f)
                {
                    GameObject.Find("Game_Camera").GetComponent<CameraCtr>().target = mainActor.transform;
                    GameEvent.Send(GameEventDefine.DialogueTaskStart, 0, "ch00", 6, 9);
                    boss.GetComponent<MobCtr>().move_speed = 2f;
                    CurrentMapMobNum = 3;
                }
            }
        }
    }
}
