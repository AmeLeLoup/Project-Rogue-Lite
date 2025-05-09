using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class GeneratorPCGNoCoroutine : MonoBehaviour
{

    [SerializeField] private Tilemap groundMap;
    [SerializeField] private Tilemap wallMap;
    [SerializeField] private List<TileBase> tiles;
    [SerializeField] private TileBase groundRuleTile;
    [SerializeField] private TileBase wallsRuleTile;
    [SerializeField] private Vector3Int startPosition = Vector3Int.zero;

    [Header("Settings")]
    [SerializeField] private int lMin;
    [SerializeField] private int lMax;
    [SerializeField] private int iterMax;
    [SerializeField] private int nbTilesMax;
    [SerializeField] private int heightMax;
    [SerializeField] private int widhtMax;
    
    [Header("Fill Settings")]
    [SerializeField] private int fillIteration;
    [SerializeField] private int aliveNeighbourNedded = 3;

    private Vector2Int[] _directions = new[]
    {
        Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left
    };
    private Vector3Int[] _moreNeighbours = new[]
    {
        new Vector3Int(0, 1, 0),
        new Vector3Int(1, 1, 0),
        new Vector3Int(1, 0, 0),
        new Vector3Int(1, -1, 0),
        new Vector3Int(0, -1, 0),
        new Vector3Int(-1, -1, 0),
        new Vector3Int(-1, 0, 0),
        new Vector3Int(-1, 1, 0)
    };
    private BoundsInt _barrier;

    void Start()
    {
        SetBarrier();
        Generate();
    }
    private void OnDrawGizmos()
    {
        SetBarrier();

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_barrier.center, _barrier.size);
    }
    void Update()
    {
    }
    private void SetBarrier()
    {
        Vector3Int size = new Vector3Int(widhtMax, heightMax, 0);
        Vector3Int boundsPosition = startPosition - size / 2;

        _barrier = new BoundsInt(boundsPosition, size);
    }
    private void Generate()
    {
        Debug.Log("Generate Drunkard");
        GenerateDrunkard();
        
        Debug.Log("Generate Fill");
        GenerateFillHoles();
        
        Debug.Log("Remove Alone Tiles");
        RemoveAloneTiles();
        
        Debug.Log("Add Walls");
        GenerateOuterWalls();
    }
    public void RestartGeneration()
    {
        StopGeneration();
        Generate();
    }
    public void StopGeneration()
    {
        groundMap.ClearAllTiles();
        wallMap.ClearAllTiles();
    }
    private void GenerateDrunkard()
    {
        Vector2Int direction = Vector2Int.zero;
        Vector3Int position = startPosition;

        int tileCount = 0;
        int iterCount = 0;

        AddTile(startPosition, ref tileCount);

        while (tileCount < nbTilesMax && iterCount < iterMax)
        {
            direction = _directions[Random.Range(0, _directions.Length)];
            int currentPathLength = Random.Range(lMin, lMax);

            Vector3Int futurePosition = position + currentPathLength * new Vector3Int(direction.x, direction.y, 0);

            if (IsInBounds(futurePosition))
            {
                for (int i = 0; i < currentPathLength; i++)
                {
                    position += new Vector3Int(direction.x, direction.y, 0);
                    AddTile(position, ref tileCount);
                }

                iterCount++;

            }
        }
    }
    private void GenerateFillHoles()
    {
        List<Vector3Int> aliveCells = new List<Vector3Int>();
        List<Vector3Int> deadCells = new List<Vector3Int>();
        for (int i = 0; i < fillIteration; i++)
        {
            aliveCells.Clear();
            deadCells.Clear();
            for (int x = _barrier.xMin; x < _barrier.xMax; x++)
            {
                for (int y = _barrier.yMin; y < _barrier.yMax; y++)
                {
                    Vector3Int position = new Vector3Int(x, y);
                
                    bool isAlive = groundMap.HasTile(position);
                
                    int countAliveNeighbours = 0;
                    foreach (Vector3Int neighbour in _moreNeighbours)
                    {
                        if (IsInBounds(position + neighbour))
                        {
                            TileBase t = groundMap.GetTile(position + neighbour);
                            if (t != null)
                            {
                                countAliveNeighbours++;
                            }
                        }
                    }

                    if (isAlive)
                    {
                        aliveCells.Add(position);
                    }
                    else
                    { 
                        if (countAliveNeighbours >= aliveNeighbourNedded + i)
                        {
                            aliveCells.Add(position);
                        }
                        else
                        {
                            deadCells.Add(position);
                        }
                    }
                }
            }

            foreach (Vector3Int aliveCell in aliveCells)
            {
                if(!groundMap.HasTile(aliveCell)) 
                {
                    //map.SetTile(aliveCell, GetRandomTile());
                    groundMap.SetTile(aliveCell, groundRuleTile);
                }
            }
            foreach (Vector3Int deadCell in deadCells)
            {
                if (groundMap.HasTile(deadCell))
                {
                    groundMap.SetTile(deadCell, null);
                }
            }
        }
    }
    private void RemoveAloneTiles()
    {
        List<Vector3Int> aliveCells = new List<Vector3Int>();
        List<Vector3Int> deadCells = new List<Vector3Int>();
        for (int i = 0; i < fillIteration; i++)
        {
            aliveCells.Clear();
            deadCells.Clear();
            for (int x = _barrier.xMin; x < _barrier.xMax; x++)
            {
                for (int y = _barrier.yMin; y < _barrier.yMax; y++)
                {
                    Vector3Int position = new Vector3Int(x, y);
                
                    bool isAlive = groundMap.HasTile(position);
                
                    int countAliveNeighbours = 0;
                    foreach (Vector3Int neighbour in _moreNeighbours)
                    {
                        if (IsInBounds(position + neighbour))
                        {
                            TileBase t = groundMap.GetTile(position + neighbour);
                            if (t != null)
                            {
                                countAliveNeighbours++;
                            }
                        }
                    }

                    if (isAlive)
                    {
                        if (countAliveNeighbours == 3)
                        {
                            deadCells.Add(position);
                        }
                        else
                        {
                            aliveCells.Add(position);
                        }
                    }
                    else
                    {
                        deadCells.Add(position);
                    }
                }
            }

            foreach (Vector3Int aliveCell in aliveCells)
            {
                if(!groundMap.HasTile(aliveCell)) 
                {
                    //map.SetTile(aliveCell, GetRandomTile());
                    groundMap.SetTile(aliveCell, groundRuleTile);
                }
            }
            foreach (Vector3Int deadCell in deadCells)
            {
                if (groundMap.HasTile(deadCell))
                {
                    groundMap.SetTile(deadCell, null);
                }
            }
        }
    }
    private void GenerateOuterWalls()
    {
        List<Vector3Int> aliveCells = new List<Vector3Int>();
        for (int x = _barrier.xMin; x < _barrier.xMax; x++)
        {
            for (int y = _barrier.yMin; y < _barrier.yMax; y++)
            {
                Vector3Int position = new Vector3Int(x, y);
            
                bool isAlive = groundMap.HasTile(position);
            
                int countAliveNeighbours = 0;
                foreach (Vector3Int neighbour in _moreNeighbours)
                {
                    if (IsInBounds(position + neighbour))
                    {
                        TileBase t = groundMap.GetTile(position + neighbour);
                        if (t != null)
                        {
                            countAliveNeighbours++;
                        }
                    }
                }

                if (isAlive)
                {
                }
                else
                {
                    if (countAliveNeighbours >= 1) 
                        aliveCells.Add(position);
                   
                }
            }
        }

        foreach (Vector3Int aliveCell in aliveCells)
        {
            if(!wallMap.HasTile(aliveCell)) 
            {
                wallMap.SetTile(aliveCell, wallsRuleTile);
            }
        }
    }
    
    private void AddTile(Vector3Int position, ref int count)
    {
        if (groundMap.HasTile(position))
            return;

        //map.SetTile(position, GetRandomTile());
        groundMap.SetTile(position, groundRuleTile);
        count++;
    }
    private TileBase GetRandomTile()
    {
        return tiles[Random.Range(0, tiles.Count)];
    }
    private bool IsInBounds(Vector3Int position)
    {
        return (position.x >= _barrier.xMin && position.x < _barrier.xMax && position.y >= _barrier.yMin &&
                position.y < _barrier.yMax);
    }
}
