using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    //层数
    static public bool nextmap = true;
    public int layerNum = 0;
    public int minlayerNum = 12;
    public int maxlayerNum = 15;
    [Range(2, 4)]
    public int LayerNum = 8;
    public float cellHeight = 1f;
    public float layerWidth = 6f;

    public Transform _firstPos;

    static public List<MapLayer> mapLayers;
    static public List<MapLayer> newmapLayers;
    [SerializeField] MapRoom _mapRoomPrefab;

    //[Button("测试")]
    private void Start()
    {
        if (nextmap)
        {
            ClearAllRoom();//清空上一层创建的房间
            CreateMapLayer();//创建房间
            ConnectRoom();//连接房间
            CreateLine();//房间画线
            nextmap = false;
        }
        else 
        {
            RecreatRoom();
            ConnectRoom();//连接房间
            CreateLine();//房间画线
        }

    }
    private void ClearAllRoom()
    {
        var _array = GetComponentsInChildren<MapRoom>();
        for (int i = _array.Length - 1; i >= 0; i--)
        {
            DestroyImmediate(_array[i].gameObject);
        }
        mapLayers?.Clear();
    }



    public void CreateMapLayer()
    {

        layerNum = Random.Range(minlayerNum, maxlayerNum);
        mapLayers = new List<MapLayer>();
        newmapLayers = new List<MapLayer>();

        for (int i = 0; i < layerNum; i++)
        {
            MapLayer mapLayer = new MapLayer
            {
                posY = _firstPos.localPosition.y + i * cellHeight + Random.Range(-0.5f, 0.5f)
            };

            //每一层有多少个房子
            List<MapRoom> mapRooms = new List<MapRoom>();
            var num = Random.Range(5, LayerNum);
            if (i == 0) num = 1;//设置第一层房间数
            else if (i == layerNum - 1) num = 1;//设置最后一层房间数
            float posX=_firstPos.position.x;//上一间房子的x坐标位置
            for (int j = 0; j < num; j++)
            {
                var mapRoom = GameObject.Instantiate<MapRoom>(_mapRoomPrefab, transform);
                mapRoom.name = $"Room({i},{j})";
                posX = posX + layerWidth/num + Random.Range(-0.5f, 0.5f);//平铺之后+随机横向偏移
                mapRoom.transform.position = new Vector3(posX, mapLayer.posY, 0);
                mapRoom.posX = posX;
                mapRooms.Add(mapRoom);
            }
            mapLayer.rooms = mapRooms;
            mapLayers.Add(mapLayer);
            newmapLayers = mapLayers;
        }
    }

    public void ConnectRoom()
    {
        for (int i = 0; i < newmapLayers.Count - 1; i++)
        {
            var rooms = newmapLayers[i].rooms;
            for (int j = 0; j < rooms.Count; j++)
            {
                var min = newmapLayers[i + 1].rooms.Select(r =>
                {
                    return (Vector2.Distance(r.transform.position, rooms[j].transform.position), r);
                }).ToList().Min();
                rooms[j].connectRooms.Add(min.Item2);
            }
        }

        //断路检测 
        for (int i = 1; i < newmapLayers.Count; i++)
        {
            var rooms = newmapLayers[i].rooms;
            for (int j = 0; j < rooms.Count; j++)
            {
                var currentRoom = rooms[j];
                //遍历下层所有房间
                var lastRooms = newmapLayers[i - 1].rooms;
                for (int k = 0; k < lastRooms.Count; k++)
                {
                    if (lastRooms[k].connectRooms.Contains(currentRoom)) break;

                    //如果下层所有房间中没有一间房间将当前房间连接上，则下层房间中距离最近的一间房间添加连接
                    if (k == lastRooms.Count - 1)
                    {
                        //Debug.Log($"{i - 1}：{k}");
                        var min = lastRooms.Select(r =>
                            {
                                return (Vector2.Distance(r.transform.position, currentRoom.transform.position), r);
                            }).ToList().Min();
                        min.Item2.connectRooms.Add(currentRoom);
                    }
                }
            }
        }
    }
    [SerializeField] LineRenderer _linePrefab;
    private void CreateLine()
    {
        for (int i = 0; i < newmapLayers.Count - 1; i++)
        {
            var layer = newmapLayers[i];
            for (int j = 0; j < layer.rooms.Count; j++)
            {
                var room = layer.rooms[j];
                for (int k = 0; k < room.connectRooms.Count; k++){
                    var line = Instantiate<LineRenderer>(_linePrefab,room.transform);
                    line.positionCount=2;
                    line.SetPosition(0, room.transform.position);
                    line.SetPosition(1, room.connectRooms[k].transform.position);
                }
            }
        }
    }
    private void RecreatRoom() 
    {
        newmapLayers = new List<MapLayer>();
        for (int i = 0; i < mapLayers.Count; i++)
        {
            MapLayer mapLayer = new MapLayer
            {
                posY = mapLayers[i].posY,
            };

            //每一层有多少个房子
            List<MapRoom> mapRooms = new List<MapRoom>();
            for (int j = 0; j < mapLayers[i].rooms.Count; j++)
            {
                var mapRoom = GameObject.Instantiate<MapRoom>(_mapRoomPrefab, transform);
                mapRoom.name = $"Room({i},{j})";
                float posX = mapLayers[i].rooms[j].posX;
                mapRoom.transform.position = new Vector3(posX, mapLayer.posY, 0);
                mapRoom.GetComponent<SpriteRenderer>().sprite = mapRoom.sprites[mapLayers[i].rooms[j].type];
                mapRooms.Add(mapRoom);
               
            }
            mapLayer.rooms = mapRooms;
            newmapLayers.Add(mapLayer);
        }
    }
}

