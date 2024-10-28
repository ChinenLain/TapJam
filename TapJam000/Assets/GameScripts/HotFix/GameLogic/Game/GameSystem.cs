using Cysharp.Threading.Tasks;
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

        public async UniTaskVoid LoadGame()
        {
            await UniTask.Yield();

            GameModule.UI.ShowUI<UIGameWindow>();

            MapManager.Instance.ChangeChapter(0);
            MapManager.Instance.ChangeMap(8);

            GameEvent.Send(GameEventDefine.DialogueTaskStart,0, "ch00", 0,5);
        }

        public void DestroyGame()
        {

        }

        public override void Update()
        {

        }
    }
}
