using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public abstract class MapCtrBase : MonoBehaviour
    {
        public int _mapType;

        protected GameObject generate;
        protected GameObject trigger_left;
        protected GameObject trigger_right;
        protected GameObject trigger_up;
        protected GameObject trigger_down;

        protected void Init()
        {
            
            generate = transform.Find("mapface/generate").gameObject;

            Transform ts;
            ts = transform.Find("Game/Trigger/trigger_left");
            if (ts != null) { trigger_left = ts.gameObject; trigger_left.AddComponent<TriggerBhv>().Init(this, "left"); }

            ts = transform.Find("Game/Trigger/trigger_right");
            if (ts != null) { trigger_right = ts.gameObject; trigger_right.AddComponent<TriggerBhv>().Init(this, "right"); }

            ts = transform.Find("Game/Trigger/trigger_up");
            if (ts != null) { trigger_up = ts.gameObject; trigger_up.AddComponent<TriggerBhv>().Init(this, "up"); }

            ts = transform.Find("Game/Trigger/trigger_down");
            if (ts != null) { trigger_down = ts.gameObject; trigger_down.AddComponent<TriggerBhv>().Init(this, "down"); }
        }

        public abstract void InitRandomMap(int type);

        public abstract UniTask AddRes();

        public void OnMapTriggerEnter(string trigger_name, Collider other) 
        { 
            MapManager.Instance.OnMapTriggerEnter(trigger_name, other);
        }
        public void OnMapTriggerStay(string trigger_name, Collider other)
        {
            MapManager.Instance.OnMapTriggerEnter(trigger_name, other);
        }
        public void OnMapTriggerExit(string trigger_name, Collider other)
        {
            MapManager.Instance.OnMapTriggerEnter(trigger_name, other);
        }

        public Sprite GetRandomSprite(List<Sprite> sprites)
        {
            if(sprites.Count == 0) return null;
            else
            {
                int randomIndex = Random.Range(0,sprites.Count);
                return sprites[randomIndex];
            }
        }
    }



    public class TriggerBhv : MonoBehaviour
    {
        public MapCtrBase _mapCtr;
        public string _name;
        public void Init(MapCtrBase mapCtr, string name)
        {
            _mapCtr = mapCtr;
            _name = name;
        }

        private void OnTriggerEnter(Collider other)
        {
            _mapCtr.OnMapTriggerEnter(_name, other);
        }

        private void OnTriggerStay(Collider other)
        {
            _mapCtr.OnMapTriggerStay(_name, other);
        }

        private void OnTriggerExit(Collider other)
        {
            _mapCtr.OnMapTriggerExit(_name,other);
        }
    }
}
