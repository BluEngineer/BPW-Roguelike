using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum enemyType {basicEnemy, SniperEnemy, swarmEnemy }

namespace DungeonGeneration
{

    public enum RoomType { NormalRoom, StartRoom, EndRoom, ChestRoom };

    public class DungeonGenerator: MonoBehaviour
    {
        private static DungeonGenerator _instance;
        public static DungeonGenerator Instance { get { return _instance; } }

        private void Awake()
        {

            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }
    

        public enum Tile { Floor }

        [Header("Prefabs")]
        public GameObject[] forestFloor;
        public GameObject[] forestWall;

        public GameObject[] dungeonFloor;
        public GameObject[] dungeonWall;

        public GameObject scifiFloor;
        public GameObject scifiWall;

        public GameObject dungeonPortal;
        public GameObject playerSpawnPoint;

        public GameObject currentFloorType;
        public GameObject currentWallType;

        public GameObject starterGunPrefab;

        //vervang gameobject later met dictionary met enemies
        //public Dictionary<enemyType type, >;
        public GameObject enemy;
        public GameObject chestPrefab;

        public NavMeshSurface navMeshSurface;

        [Header("Dungeon Settings")]
        public int amountRooms;
        public int width = 100;
        public int height = 100;
        public int minRoomSize = 3;
        public int maxRoomSize = 7;
     

        private Dictionary<Vector2Int, Tile> dungeonDictionary = new Dictionary<Vector2Int, Tile>();
        private List<Room> roomList = new List<Room>();
        private List<GameObject> allSpawnedObjects = new List<GameObject>();
        public Vector3[] test123;


        void Start()
        {

        }


        public void AssignTileset()
        {
            if (GameManager.Instance.dungeonLayer == 1)
            {
                currentWallType = forestWall[Random.Range(0, forestWall.Length)];
                currentFloorType = forestFloor[Random.Range(0, forestFloor.Length)];
            }
            if (GameManager.Instance.dungeonLayer == 2)
            {
                currentWallType = dungeonWall[Random.Range(0, dungeonFloor.Length)];
                currentFloorType = dungeonFloor[Random.Range(0, dungeonFloor.Length)];
                amountRooms += 4;
                width += 3;
                height += 5;
                maxRoomSize += 2;

            }

            if (GameManager.Instance.dungeonLayer == 3)
            {
                currentWallType = scifiWall;
                currentFloorType = scifiFloor;
                amountRooms += 7;
                width += 7;
                height += 9;
                maxRoomSize += 2;

            }

        }

        private void AllocateRooms()
        {
            for (int i = 0; i < amountRooms; i++)
            {
                Room room = new Room()
                {
                    position = new Vector2Int(Random.Range(0, width), Random.Range(0, height)),
                    size = new Vector2Int(Random.Range(minRoomSize, maxRoomSize), Random.Range(minRoomSize, maxRoomSize))
                };

                

                if (CheckIfRoomFitsInDungeon(room))
                {
                    AddRoomToDungeon(room);
                }
                else
                {
                    i--;
                }
            }

        }

        public void AssignStartEndRooms()
        {
            //Geschreven door Bas en Ravi
            List<Vector3> roomDistance = new List<Vector3>();

            int count = 0;
            int id = int.MaxValue;
            float longestDist = 0;
            int lastRoomId = int.MaxValue;
            int curRoomId = 0;

            while (curRoomId != lastRoomId)
            {
                for (int i = 0; i < amountRooms - 1;)
                {

                    float distance = Vector2Int.Distance(roomList[curRoomId].position, roomList[i + 1].position);
                    if (distance > longestDist)
                    {
                        longestDist = distance;
                        id = count;
                    }
                    roomDistance.Add(new Vector3(distance, curRoomId, i + 1));


                    i++;
                    count++;

                    if (i == amountRooms - 1)
                    {
                        lastRoomId = curRoomId;
                        curRoomId++;
                        i = curRoomId;
                    }

                    if(curRoomId == amountRooms - 1)
                    {
                        lastRoomId = curRoomId;

                    }
                    
                }
            }

            //Vector3.Max(roomDistance);
            roomList[(int)roomDistance[id][1]].roomType = RoomType.StartRoom;
            roomList[(int)roomDistance[id][2]].roomType = RoomType.EndRoom;
            test123 = roomDistance.ToArray();

            SpawnStartEndPoint();

        }

        private void AssignChestRoom()
        {
            for (int i = 0; i < (2 + GameManager.Instance.dungeonLayer); i++)
            {
                roomList[Random.Range(0, roomList.Count)].roomType = RoomType.ChestRoom;

            }
        }

        private void SpawnStartEndPoint()
        {
            for (int i = 0; i < roomList.Count; i++)
            {
                if(roomList[i].roomType == RoomType.EndRoom)
                {
                    Instantiate(dungeonPortal, new Vector3(roomList[i].position.x + roomList[i].size.x / 2, 0.3f, roomList[i].position.y + roomList[i].size.y / 2), Quaternion.identity);
                }

                if(roomList[i].roomType == RoomType.StartRoom)
                {
                    Instantiate(playerSpawnPoint, new Vector3(roomList[i].position.x + roomList[i].size.x / 2, 0.25f, roomList[i].position.y + roomList[i].size.y / 2), Quaternion.identity);
                    Instantiate(starterGunPrefab, new Vector3(roomList[i].position.x + roomList[i].size.x / 2, 1f, roomList[i].position.y + roomList[i].size.y / 2), Quaternion.identity);

                }
            }
        }

        private void SpawnEnemiesInRoom(int roomIndex)
        {
            for (int i = 0; i < roomList.Count; i++)
            {
                if (roomList[i].roomType != RoomType.StartRoom && roomList[i].roomType != RoomType.EndRoom && roomList[i].roomType != RoomType.ChestRoom)
                {
                    Instantiate(enemy, new Vector3(roomList[i].position.x + roomList[i].size.x / 2, 0.3f, roomList[i].position.y + roomList[i].size.y / 2), Quaternion.identity);
                }
            }
        }

        private void SpawnChestInRoom()
        {
            for (int i = 0; i < roomList.Count; i++)
            {
                if (roomList[i].roomType == RoomType.ChestRoom)
                {
                    int[] tileYRotations = { 0, 90, 180, 270 };
                    Instantiate(chestPrefab, new Vector3(roomList[i].position.x + roomList[i].size.x / 2, 0.3f, roomList[i].position.y + roomList[i].size.y / 2), Quaternion.Euler(0, tileYRotations[Random.Range(0, tileYRotations.Length)],0));
                }
            }
        }


        private void AddRoomToDungeon(Room room)
        {
            for (int xx = room.position.x; xx < room.position.x + room.size.x; xx++)
            {
                for (int yy = room.position.y; yy < room.position.y + room.size.y; yy++)
                {
                    Vector2Int pos = new Vector2Int(xx, yy);
                    dungeonDictionary.Add(pos, Tile.Floor);
                }
            }
            roomList.Add(room);
        }

        private bool CheckIfRoomFitsInDungeon(Room room)
        {
            for (int xx = room.position.x; xx < room.position.x + room.size.x; xx++)
            {
                for (int yy = room.position.y; yy < room.position.y + room.size.y; yy++)
                {
                    Vector2Int pos = new Vector2Int(xx, yy);
                    if (dungeonDictionary.ContainsKey(pos))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void AllocateCorridors()
        {

            for (int i = 0; i < roomList.Count; i++)
            {
                //modulo
                //10 % 3 = 1
                //20 % 3 = 2
                // 0 1 2 3 4 5 [6 7]
                Room startRoom = roomList[i];
                Room otherRoom = roomList[(i + Random.Range(1, roomList.Count - 1)) % roomList.Count];

                // -1, 0, 1
                int dirX = Mathf.RoundToInt(Mathf.Sign(otherRoom.position.x - startRoom.position.x));
                for(int x = startRoom.position.x; x != otherRoom.position.x; x += dirX)
                {
                    Vector2Int pos = new Vector2Int(x, startRoom.position.y);
                    if (!dungeonDictionary.ContainsKey(pos))
                    {
                        dungeonDictionary.Add(pos, Tile.Floor);
                    }
                }

                int dirY = Mathf.RoundToInt(Mathf.Sign(otherRoom.position.y - startRoom.position.y));
                for (int y = startRoom.position.y; y != otherRoom.position.y; y += dirY)
                {
                    Vector2Int pos = new Vector2Int(otherRoom.position.x, y);
                    if (!dungeonDictionary.ContainsKey(pos))
                    {
                        dungeonDictionary.Add(pos, Tile.Floor);
                    }
                }
            }
        }

        private void BuildDungeon()
        {
            foreach(KeyValuePair<Vector2Int, Tile> kv in dungeonDictionary)
            {
                AssignTileset();
                //te lui om te berekenen
                int[] tileYRotations = {0,90,180,270};
                GameObject floor = Instantiate( currentFloorType, new Vector3Int(kv.Key.x, 0, kv.Key.y), Quaternion.Euler(0,tileYRotations[ Random.Range(0,tileYRotations.Length)],0));
                allSpawnedObjects.Add(floor);
                floor.transform.parent = gameObject.transform;
                        
                SpawnWallsForTile(kv.Key);
            }
        }

        public void ResetDungeon()
        {
            foreach(GameObject spawnedObj in allSpawnedObjects)
            {
                Destroy(spawnedObj);
            }

            dungeonDictionary.Clear();
            roomList.Clear();
        }

        private void SpawnWallsForTile(Vector2Int position)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    if(Mathf.Abs(x) == Mathf.Abs(z)) { continue; }
                    Vector2Int gridPos = position + new Vector2Int(x, z);
                    if (!dungeonDictionary.ContainsKey(gridPos))
                    {
                        //Spawn Wall
                        Vector3 direction = new Vector3(gridPos.x, 0, gridPos.y) - new Vector3(position.x, 0, position.y);
                        GameObject wall = Instantiate(currentWallType, new Vector3(position.x, 0, position.y), Quaternion.LookRotation(direction));
                        allSpawnedObjects.Add(wall);
                        wall.transform.parent = gameObject.transform;
                    }
                }
            }
        }


        public void GenerateDungeon()
        {
            AssignTileset();
            AllocateRooms();
            AllocateCorridors();
            AssignStartEndRooms();
            AssignChestRoom();
            BuildDungeon();
            SpawnEnemiesInRoom(0);
            SpawnChestInRoom();
            navMeshSurface.BuildNavMesh();
            Debug.Log(" Dungeon Generated");
        }

    }

    public class Room
    {
        public Vector2Int position;
        public Vector2Int size;
        public RoomType roomType;
        //temp vervang dit met een beter systeem?
        
        //public enum Biome {forest, dungeon, mountain, military }

    }

}
