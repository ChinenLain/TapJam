using Cysharp.Threading.Tasks;
using GameBase;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class MapManager : SingletonBehaviour<MapManager>
    {

        public int currentMapId;
        public Dictionary<int,GameObject> m_mapObj = new Dictionary<int, GameObject>();

        public int currentChapterId;
        public List<MapInfo> m_mapInfos = new List<MapInfo>();

        public GameObject actor;

        public void ChangeChapter(int chapterId)
        {
            switch (chapterId)
            {
                case 1:
                    currentChapterId = chapterId;
                    m_mapInfos = ChapterMapDefine.GetMapInfo(ChapterMapDefine.chapter_01);
                    ClearMap();
                    ChangeMap(m_mapInfos[0].id);
                    break;
            }
        }

        public void ChangeMap(int mapId) 
        { 
            MapInfo mapInfo = null;
            for(int i = 0; i < m_mapInfos.Count; i++)
            {
                if (m_mapInfos[i].id == mapId)
                {
                    mapInfo = m_mapInfos[i];
                    break;
                }
            }
            if (mapInfo == null) return;

            actor.transform.position = new Vector3(0, 0.7f, 0);

            CloseMap(currentMapId);

            currentMapId = mapId;

            if(m_mapObj.ContainsKey(currentMapId))
            {
                OpenMap(currentMapId);
                GameEvent.Send(GameEventDefine.LoadMapFinish);
                return;
            }

            GameObject currentMapObj = null;

            switch ((MapType)mapInfo.maptype)
            {
                case MapType.FOREST_RECT01:
                    currentMapObj = Instantiate(GameModule.Resource.LoadAsset<GameObject>("Map_forest_rect"),
                        Vector3.zero,Quaternion.identity);
                    currentMapObj.GetComponent<Map_forest_rect>()._mapType = 0;
                    break;
                case MapType.FOREST_RECT02:
                    currentMapObj = Instantiate(GameModule.Resource.LoadAsset<GameObject>("Map_forest_rect"),
                        Vector3.zero, Quaternion.identity);
                    currentMapObj.GetComponent<Map_forest_rect>()._mapType = 1;
                    break;
                case MapType.FOREST_FOG:
                    currentMapObj = Instantiate(GameModule.Resource.LoadAsset<GameObject>("Map_forest_rect"),
                        Vector3.zero, Quaternion.identity);
                    currentMapObj.GetComponent<Map_forest_rect>()._mapType = 2;
                    break;
                case MapType.MOUNTAIN_RECT01:
                    currentMapObj = Instantiate(GameModule.Resource.LoadAsset<GameObject>("Map_mountain_rect"),
                        Vector3.zero, Quaternion.identity);
                    currentMapObj.GetComponent<Map_mountain_rect>()._mapType = 3;
                    break;
                case MapType.MOUNTAIN_RECT02:
                    currentMapObj = Instantiate(GameModule.Resource.LoadAsset<GameObject>("Map_mountain_rect"),
                        Vector3.zero, Quaternion.identity);
                    currentMapObj.GetComponent<Map_mountain_rect>()._mapType = 4;
                    break;
                case MapType.MOUNTAIN_FOG:
                    currentMapObj = Instantiate(GameModule.Resource.LoadAsset<GameObject>("Map_mountain_rect"),
                        Vector3.zero, Quaternion.identity);
                    currentMapObj.GetComponent<Map_mountain_rect>()._mapType = 5;
                    break;
                case MapType.MOUNTAIN_LONG:
                    currentMapObj = Instantiate(GameModule.Resource.LoadAsset<GameObject>("Map_mountain_long"),
                        Vector3.zero, Quaternion.identity);
                    currentMapObj.GetComponent<Map_mountain_long>()._mapType = 6;
                    break;
                case MapType.ROCK_RECT:
                    currentMapObj = Instantiate(GameModule.Resource.LoadAsset<GameObject>("Map_rock_rect"),
                        Vector3.zero, Quaternion.identity);
                    currentMapObj.GetComponent<Map_rock_rect>()._mapType = 7;
                    break;
                case MapType.ROCK_CROSS:
                    currentMapObj = Instantiate(GameModule.Resource.LoadAsset<GameObject>("Map_rock_cross"),
                        Vector3.zero, Quaternion.identity);
                    currentMapObj.GetComponent<Map_rock_cross>()._mapType = 8;
                    break;
                case MapType.GRASS_RECT:
                    currentMapObj = Instantiate(GameModule.Resource.LoadAsset<GameObject>("Map_grass_rect"),
                        Vector3.zero, Quaternion.identity);
                    currentMapObj.GetComponent<Map_grass_rect>()._mapType = 9;
                    break;
            }

            if(currentMapObj != null)
                m_mapObj.Add(mapId, currentMapObj);
        }

        private void DestroyMap(int mapId)
        {
            GameObject map = null;
            foreach (var mapObj in m_mapObj)
            {
                if (mapObj.Key == mapId)
                {
                    map = mapObj.Value;
                    break;
                }
            }
            if (map == null) return;
            m_mapObj.Remove(mapId);
            Destroy(map);
        }

        private void ClearMap()
        {
            foreach (var mapObj in m_mapObj)
            {
                if(mapObj.Value != null)
                {
                    Destroy(mapObj.Value);
                }
            }
            m_mapObj.Clear();
        }

        private void CloseMap(int mapId)
        {
            GameObject map = null;
            foreach (var mapObj in m_mapObj)
            {
                if (mapObj.Key == mapId)
                {
                    map = mapObj.Value;
                }
            }
            if (map == null) return;
            map.SetActive(false);
        }

        private void OpenMap(int mapId)
        {
            GameObject map = null;
            foreach (var mapObj in m_mapObj)
            {
                if (mapObj.Key == mapId)
                {
                    map = mapObj.Value;
                }
            }
            if (map == null) return;
            map.SetActive(true);
        }

        public void OnMapTriggerEnter(string trigger_name, Collider other)
        {
            if (other.gameObject.tag != "Player") return;
            MapInfo mapInfo = null;
            for (int i = 0; i < m_mapInfos.Count; i++)
            {
                if (m_mapInfos[i].id == currentMapId)
                {
                    mapInfo = m_mapInfos[i];
                }
            }

            switch (trigger_name)
            {
                case "left":
                    if (mapInfo.left_id != -1)
                        ChangeMap(mapInfo.left_id);
                    break;
                case "right":
                    if (mapInfo.right_id != -1)
                        ChangeMap(mapInfo.right_id);
                    break;
                case "up":
                    if (mapInfo.up_id != -1)
                        ChangeMap(mapInfo.up_id);
                    break;
                case "down":
                    if (mapInfo.down_id != -1)
                        ChangeMap(mapInfo.down_id);
                    break;
            }
        }
        public void OnMapTriggerStay(string trigger_name, Collider other)
        {
            if (other.gameObject.tag != "Player") return;
        }
        public void OnMapTriggerExit(string trigger_name, Collider other)
        {
            if (other.gameObject.tag != "Player") return;
        }
    }
}
