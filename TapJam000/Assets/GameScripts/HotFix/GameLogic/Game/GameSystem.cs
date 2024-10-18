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

            GameModule.UI.ShowUIAsync<UIGameWindow>();
        }

        public void DestroyGame()
        {

        }

        public override void Update()
        {

        }
    }
}
