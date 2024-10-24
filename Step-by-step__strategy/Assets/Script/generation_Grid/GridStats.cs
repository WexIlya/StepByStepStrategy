using UnityEngine;
using static GameManager;

public class GridStats : MonoBehaviour
{
    [SerializeField] int visited = -1;
    [SerializeField] int x;
    [SerializeField] int z;

    public int _Visited
    {
        get { return visited; }
        set { visited = value; }
    }
    public int _X
    { 
        get { return x; }
        set { x = value; }
    }
    public int _Z
    {
        get { return z; }
        set { z = value; }
    }
}
