using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public bool FindDistance = false;
    public static GridManager Instance;
    [SerializeField] int width, height;
    [SerializeField] GameObject EathTile, StumpTile, ArmorTile, FlouverTile;
    [SerializeField] Transform Camera;
    private Dictionary<Vector3, Tile> Tiles;
    [SerializeField] GameObject[,] GridArray;
    [SerializeField] List<GameObject> path =  new List<GameObject>();
    [SerializeField] int StartX;
    [SerializeField] int StartZ;
    [SerializeField] int EndX;
    [SerializeField] int EndZ;
    [SerializeField] GameObject selectedUnit;

    private void Awake()
    {
        Instance = this;
        GridArray = new GameObject[width, height];
    }

    private void Update()
    {
        if(FindAnyObjectByType<Selection>()._SelectionUnit != null) selectedUnit = FindAnyObjectByType<Selection>()._SelectionUnit;
        if (FindDistance)
        {
            SetDistance();
            SetPath();
            selectedUnit.GetComponent<BaseUnit>().GetPath(path);
            selectedUnit.GetComponent<BaseUnit>().MoveUnit();
            FindDistance = false;
        }
    }

    public void GetEndUnitXZ(int x,int z)
    {
        StartX = x;
        StartZ = z;
    }
    public void GetStartUnitXZ(int x, int z)
    {
        EndX = x;
        EndZ = z;
    }

    public void GenerateGrid()
    {
        Tiles = new Dictionary<Vector3, Tile>();

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                var RandomTile = EathTile;
                int RandomValue = Random.Range(0, 6);
                switch (RandomValue)
                {
                    case 0: RandomTile = EathTile;
                        break;
                    case 1:
                        RandomTile = EathTile;
                        break;
                    case 2:
                        RandomTile = EathTile;
                        break;
                    case 3:
                        RandomTile = StumpTile;
                        break;
                    case 4:
                        RandomTile = FlouverTile;
                        break;
                    case 5:
                        RandomTile = ArmorTile;
                        break;
                }
                GameObject SpawnedTile = Instantiate(RandomTile, new Vector3(x, 0, z), Quaternion.identity);
                SpawnedTile.name = $"Tile {x} {z}";

                    SpawnedTile.GetComponent<GridStats>()._X = x;
                    SpawnedTile.GetComponent<GridStats>()._Z = z;

                GridArray[x, z] = SpawnedTile;

                Tiles[new Vector3(x, 0, z)] = SpawnedTile.GetComponent<Tile>();
            }
        }
        Camera.transform.position = new Vector3((float)width / 2 - 0.5f, 10, (float)height / 2 - 0.7f);
        GameManager.Instance.UpdateGameState(GameManager.GameState.SpawnPlayerUnits);
    }

    void InitialSetUp()
    {
        foreach (GameObject obj in GridArray)
        {
            obj.GetComponent<GridStats>()._Visited = -1;
        }
        GridArray[StartX, StartZ].GetComponent<GridStats>()._Visited = 0;
    }
    bool TestDirection(int x, int z, int step, int direction)
    {
            switch (direction)
            {
                case 1:
                    if (z + 1 < height && GridArray[x, z + 1] && GridArray[x, z + 1].GetComponent<GridStats>()._Visited == step && GridArray[x, z + 1].GetComponent<Tile>()._isWalkable)
                        return true;
                    else
                        return false;
                case 2:
                    if (x + 1 < width && GridArray[x + 1, z] && GridArray[x + 1, z].GetComponent<GridStats>()._Visited == step && GridArray[x + 1, z].GetComponent<Tile>()._isWalkable)
                        return true;
                    else
                        return false;
                case 3:
                    if (z - 1 > -1 && GridArray[x, z - 1] && GridArray[x, z - 1].GetComponent<GridStats>()._Visited == step && GridArray[x, z - 1].GetComponent<Tile>()._isWalkable)
                        return true;
                    else
                        return false;
                case 4:
                    if (x - 1 > -1 && GridArray[x - 1, z] && GridArray[x - 1, z].GetComponent<GridStats>()._Visited == step && GridArray[x - 1, z].GetComponent<Tile>()._isWalkable)
                        return true;
                    else
                        return false;
            }
        return false;
    }

    void SetDistance()
    {
        InitialSetUp();
        int x = StartX; 
        int z = StartZ;
        int[] testArray = new int[width * height];
        for (int step = 1; step < width * height; step++)
        {
            foreach (GameObject obj in GridArray)
            {
                if (obj && obj.GetComponent<GridStats>()._Visited == step - 1)
                    TestFourDirections(obj.GetComponent<GridStats>()._X, obj.GetComponent<GridStats>()._Z, step);
            }
        }
    }
    void SetPath()
    {
        int step;
        int x = EndX;
        int z = EndZ;
        List<GameObject> tempList = new List<GameObject>();
        path.Clear();
        if (GridArray[EndX, EndZ] && GridArray[EndX, EndZ].GetComponent<GridStats>()._Visited > 0)
        {
            path.Add(GridArray[x, z]);
            step = GridArray[x, z].GetComponent<GridStats>()._Visited - 1;
        }
        else
        {
            print("До туда нельзя дойти, товарищ!");
            return;
        }
        for (int i = step; step > -1; step--)
        {
            if (TestDirection(x, z, step, 1))
                tempList.Add(GridArray[x, z + 1]);
            if (TestDirection(x, z, step, 2))
                tempList.Add(GridArray[x + 1, z]);
            if (TestDirection(x, z, step, 3))
                tempList.Add(GridArray[x, z - 1]);
            if (TestDirection(x, z, step, 4))
                tempList.Add(GridArray[x - 1, z]);
            GameObject tempObj = FindClosest(GridArray[EndX, EndZ].transform, tempList);
            path.Add(tempObj);
            x = tempObj.GetComponent<GridStats>()._X;
            z = tempObj.GetComponent<GridStats>()._Z;
            tempList.Clear();
        }
    }

    void TestFourDirections(int x, int z, int step)
    {
        if (TestDirection(x, z, -1, 1))
            SetVisited(x, z + 1, step);
        if (TestDirection(x, z, -1, 2))
            SetVisited(x + 1, z, step);
        if (TestDirection(x, z, -1, 3))
            SetVisited(x, z - 1, step);
        if (TestDirection(x, z, -1, 4))
            SetVisited(x - 1, z, step);
    }

    void SetVisited(int x, int z, int step)
    {
        if (GridArray[x, z])
            GridArray[x, z].GetComponent<GridStats>()._Visited = step;
    }

    GameObject FindClosest(Transform targetLocation, List<GameObject> list)
    {
        float currentDistance = 1 * height * width;
        int indexNumber = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (Vector3.Distance(targetLocation.position, list[i].transform.position) < currentDistance)
            {
                currentDistance = Vector3.Distance(targetLocation.position, list[i].transform.position);
                indexNumber = i;
            }
        }
        return list[indexNumber];
    }

    public Tile GetTileSpawnToPlayer()
    {
        return Tiles.Where(t => t.Key.x < (width / 2) - 2 && t.Key.x > 1 && height / 3 < t.Key.z && height / 1.5f > t.Key.z && t.Value._isWalkable && t.Value.OccupieUnit == null).OrderBy(t => Random.value).First().Value;
    }
    public Tile GetTileSpawnToEnemy()
    {
        return Tiles.Where(t => t.Key.x > (width / 2) + 2 && t.Key.x < width && height / 3 < t.Key.z && height / 1.5f > t.Key.z && t.Value._isWalkable && t.Value.OccupieUnit == null).OrderBy(t => Random.value).First().Value;
    }
    public Tile GetTileSpawnToBuildingPlayer()
    {
        return Tiles.Where(t => t.Key.x == 0 && height / 3 <= t.Key.z && height / 1.5f >= t.Key.z && t.Value._isWalkable && t.Value.OccupieUnit == null && t.Value.OccupieBuilding == null).OrderBy(t => Random.value).First().Value;
    }
    public Tile GetTileSpawnToBuildingEnemy()
    {
        return Tiles.Where(t => t.Key.x == width - 1 && height / 3 <= t.Key.z && height / 1.5f >= t.Key.z && t.Value._isWalkable && t.Value.OccupieUnit == null && t.Value.OccupieBuilding == null).OrderBy(t => Random.value).First().Value;
    }
}
