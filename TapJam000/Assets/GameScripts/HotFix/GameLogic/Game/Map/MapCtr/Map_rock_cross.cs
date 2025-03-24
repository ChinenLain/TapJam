using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class Map_rock_cross : MapCtrBase
    {
        List<Sprite> background = new List<Sprite>();

        GameObject obj_background;

        public override async UniTask AddRes()
        {
            background.Add(await GameModule.Resource.LoadAssetAsync<Sprite>("backrock-01"));
            background.Add(await GameModule.Resource.LoadAssetAsync<Sprite>("backrock-02"));
            background.Add(await GameModule.Resource.LoadAssetAsync<Sprite>("backrock-03"));
            background.Add(await GameModule.Resource.LoadAssetAsync<Sprite>("backrock-04"));
            background.Add(await GameModule.Resource.LoadAssetAsync<Sprite>("backrock-05"));
            background.Add(await GameModule.Resource.LoadAssetAsync<Sprite>("backrock-06"));
        }

        public override async void InitRandomMap(int type)
        {
            await AddRes();
            obj_background.GetComponent<SpriteRenderer>().sprite = GetRandomSprite(background);
            GameEvent.Send(GameEventDefine.LoadMapFinish);
        }

        void Start()
        {
            Init();
            obj_background = transform.Find("background").gameObject;

            InitRandomMap(_mapType);
        }


        void Update()
        {

        }
    }
}
