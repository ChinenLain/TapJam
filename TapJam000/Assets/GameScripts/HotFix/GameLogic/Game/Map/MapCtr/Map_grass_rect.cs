using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class Map_grass_rect : MapCtrBase
    {
        List<Sprite> background = new List<Sprite>();
        List<Sprite> ghost = new List<Sprite>();

        GameObject obj_background;
        GameObject obj_ghost;

        public override async UniTask AddRes()
        {
            background.Add(await GameModule.Resource.LoadAssetAsync<Sprite>("backrock-07"));
            background.Add(await GameModule.Resource.LoadAssetAsync<Sprite>("backrock-08"));
            background.Add(await GameModule.Resource.LoadAssetAsync<Sprite>("backrock-09"));
            ghost.Add(await GameModule.Resource.LoadAssetAsync<Sprite>("ghost-01"));
            ghost.Add(await GameModule.Resource.LoadAssetAsync<Sprite>("ghost-02"));
        }

        public override async void InitRandomMap(int type)
        {
            await AddRes();
            obj_background.GetComponent<SpriteRenderer>().sprite = GetRandomSprite(background);
            obj_ghost.GetComponent<SpriteRenderer>().sprite = GetRandomSprite(ghost);

            GameEvent.Send(GameEventDefine.LoadMapFinish);
        }

        

        void Start()
        {
            Init();
            obj_background = transform.Find("background").gameObject;
            obj_ghost = transform.Find("ghost").gameObject;

            InitRandomMap(_mapType);

        }


        void Update()
        {

        }
    }
}
