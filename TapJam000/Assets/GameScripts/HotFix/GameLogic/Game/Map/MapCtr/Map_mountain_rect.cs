using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class Map_mountain_rect : MapCtrBase
    {
        List<Sprite> background = new List<Sprite>();
        List<Sprite> tree01 = new List<Sprite>();
        List<Sprite> tree02 = new List<Sprite>();
        List<Sprite> leaf01 = new List<Sprite>();
        List<Sprite> leaf02 = new List<Sprite>();
        List<Sprite> fogtree = new List<Sprite>();
        List<Sprite> rain = new List<Sprite>();

        GameObject obj_background;
        GameObject obj_tree;
        GameObject obj_leaf;
        GameObject obj_rain;
        GameObject obj_river;

        public override async UniTask AddRes()
        {
            background.Add(await GameModule.Resource.LoadAssetAsync<Sprite>("mountain-01"));
            background.Add(await GameModule.Resource.LoadAssetAsync<Sprite>("mountain-02"));
            background.Add(await GameModule.Resource.LoadAssetAsync<Sprite>("mountain-03"));
            tree01.Add(await GameModule.Resource.LoadAssetAsync<Sprite>("tree-01"));
            tree01.Add(await GameModule.Resource.LoadAssetAsync<Sprite>("tree-02"));
            tree01.Add(await GameModule.Resource.LoadAssetAsync<Sprite>("tree-03"));
            tree02.Add(await GameModule.Resource.LoadAssetAsync<Sprite>("tree-04"));
            tree02.Add(await GameModule.Resource.LoadAssetAsync<Sprite>("tree-05"));
            leaf01.Add(await GameModule.Resource.LoadAssetAsync<Sprite>("leaf-01"));
            leaf02.Add(await GameModule.Resource.LoadAssetAsync<Sprite>("leaf-02"));
            fogtree.Add(await GameModule.Resource.LoadAssetAsync<Sprite>("fogtree-01"));
            fogtree.Add(await GameModule.Resource.LoadAssetAsync<Sprite>("fogtree-02"));
            fogtree.Add(await GameModule.Resource.LoadAssetAsync<Sprite>("fogtree-03"));
        }

        public override async void InitRandomMap(int type)
        {
            await AddRes();
            obj_background.GetComponent<SpriteRenderer>().sprite = GetRandomSprite(background);
            switch ((MapType)type)
            {
                case MapType.MOUNTAIN_RECT01:
                    obj_tree.GetComponent<SpriteRenderer>().sprite = GetRandomSprite(tree01);
                    obj_leaf.GetComponent<SpriteRenderer>().sprite = GetRandomSprite(leaf01);
                    obj_rain.SetActive(false);
                    obj_river.SetActive(false);
                    break;
                case MapType.MOUNTAIN_RECT02:
                    obj_tree.GetComponent<SpriteRenderer>().sprite = GetRandomSprite(tree02);
                    obj_leaf.GetComponent<SpriteRenderer>().sprite = GetRandomSprite(leaf02);
                    obj_rain.SetActive(false);
                    obj_river.SetActive(false);
                    break;
                case MapType.MOUNTAIN_FOG:
                    obj_tree.GetComponent<SpriteRenderer>().sprite = GetRandomSprite(fogtree);
                    obj_leaf.GetComponent<SpriteRenderer>().sprite = GetRandomSprite(leaf02);
                    obj_rain.SetActive(true);
                    obj_rain.GetComponent<SpriteRenderer>().sprite = GetRandomSprite(rain);
                    obj_river.SetActive(true);
                    break;
            }
            GameEvent.Send(GameEventDefine.LoadMapFinish);
        }

       

        void Start()
        {
            Init();
            obj_background = transform.Find("background").gameObject;
            obj_tree = transform.Find("tree").gameObject;
            obj_leaf = transform.Find("leaf").gameObject;
            obj_rain = transform.Find("rain").gameObject;
            obj_river = transform.Find("river").gameObject;
            InitRandomMap(_mapType);
        }


        void Update()
        {

        }
    }
}
