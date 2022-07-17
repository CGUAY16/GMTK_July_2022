using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Tile : MonoBehaviour
{
    // enum TileType { Grassland, Lumberjack, Quarry, Tavern, Stable, Accountant, Church, Road };

    public int X;
    public int Y;
    public GameWarden GameWarden;

    public int tavernNeighborCount = 0;
    public int stableNeighborCount = 0;
    public float stableReductionPercent = 1;

    SpriteRenderer _myRenderer;
    TileType _myType = TileType.Grassland;
    bool _canRoll = true;

    void Awake()
    {
        _myRenderer = gameObject.AddComponent<SpriteRenderer>();
        _myRenderer.transform.localPosition = new Vector3(0,0,0);
        _myRenderer.sprite = Resources.Load<Sprite>($"Sprites/{_myType.ToString()}");
    }

    void Update()
    {
        if (_myType == TileType.Lumberjack && _canRoll)
            StartCoroutine(WorkTile("chitin"));
        if (_myType == TileType.Quarry && _canRoll)
            StartCoroutine(WorkTile("crystal"));
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (GameWarden.HeldTile!=-1&&GameWarden.HeldTile!=(int)_myType)
            {
                _myType = (TileType)GameWarden.HeldTile;
                _myRenderer.sprite = Resources.Load<Sprite>($"Sprites/{_myType.ToString()}");
                Debug.Log(GameWarden.Register((TileType)GameWarden.HeldTile));
                GameWarden.ClearHeld();
            }
        }
    }
    
    void Iterate()
    {
        if ((int)_myType < System.Enum.GetNames(typeof(TileType)).Length - 1)
            _myType++;
        else
            _myType = TileType.Grassland;
        _myRenderer.sprite = Resources.Load<Sprite>($"Sprites/{_myType.ToString()}");
    }

    void Deiterate()
    {
        if ((int)_myType > 0)
            _myType--;
        else
            _myType = TileType.Road;
    }

    void CheckNeighborsForTaverns()
    {
        Debug.Log(GameWarden._grid._gridArray[X + 1,Y]);
        Debug.Log(GameWarden._grid._gridArray[X + 1,Y + 1]);
        Debug.Log(GameWarden._grid._gridArray[X, Y + 1]);
        Debug.Log(GameWarden._grid._gridArray[X - 1,Y + 1]);
        Debug.Log(GameWarden._grid._gridArray[X - 1,Y]);
        Debug.Log(GameWarden._grid._gridArray[X - 1,Y - 1]);
        Debug.Log(GameWarden._grid._gridArray[X,Y - 1]);
        Debug.Log(GameWarden._grid._gridArray[X + 1,Y - 1]);

        tavernNeighborCount = 0;

        if (GameWarden._grid._gridArray[X + 1, Y]._myType == TileType.Tavern) // E
            tavernNeighborCount++;
        if (GameWarden._grid._gridArray[X + 1, Y + 1]._myType == TileType.Tavern) // NE
            tavernNeighborCount++;
        if (GameWarden._grid._gridArray[X, Y + 1]._myType == TileType.Tavern) // N
            tavernNeighborCount++;
        if (GameWarden._grid._gridArray[X - 1, Y + 1]._myType == TileType.Tavern) // NW
            tavernNeighborCount++;
        if (GameWarden._grid._gridArray[X - 1, Y]._myType == TileType.Tavern) // W
            tavernNeighborCount++;
        if (GameWarden._grid._gridArray[X - 1, Y - 1]._myType == TileType.Tavern) // SW
            tavernNeighborCount++;
        if (GameWarden._grid._gridArray[X, Y - 1]._myType == TileType.Tavern) // S
            tavernNeighborCount++;
        if (GameWarden._grid._gridArray[X + 1, Y - 1]._myType == TileType.Tavern) // SE
            tavernNeighborCount++;
    }

    void CheckNeighborsForStables()
    {
        Debug.Log(GameWarden._grid._gridArray[X + 1, Y]);
        Debug.Log(GameWarden._grid._gridArray[X + 1, Y + 1]);
        Debug.Log(GameWarden._grid._gridArray[X, Y + 1]);
        Debug.Log(GameWarden._grid._gridArray[X - 1, Y + 1]);
        Debug.Log(GameWarden._grid._gridArray[X - 1, Y]);
        Debug.Log(GameWarden._grid._gridArray[X - 1, Y - 1]);
        Debug.Log(GameWarden._grid._gridArray[X, Y - 1]);
        Debug.Log(GameWarden._grid._gridArray[X + 1, Y - 1]);

        stableNeighborCount = 0;

        if (GameWarden._grid._gridArray[X + 1, Y]._myType == TileType.Stable) // E
            stableNeighborCount++;
        if (GameWarden._grid._gridArray[X + 1, Y + 1]._myType == TileType.Stable) // NE
            stableNeighborCount++;
        if (GameWarden._grid._gridArray[X, Y + 1]._myType == TileType.Stable) // N
            stableNeighborCount++;
        if (GameWarden._grid._gridArray[X - 1, Y + 1]._myType == TileType.Stable) // NW
            stableNeighborCount++;
        if (GameWarden._grid._gridArray[X - 1, Y]._myType == TileType.Stable) // W
            stableNeighborCount++;
        if (GameWarden._grid._gridArray[X - 1, Y - 1]._myType == TileType.Stable) // SW
            stableNeighborCount++;
        if (GameWarden._grid._gridArray[X, Y - 1]._myType == TileType.Stable) // S
            stableNeighborCount++;
        if (GameWarden._grid._gridArray[X + 1, Y - 1]._myType == TileType.Stable) // SE
            stableNeighborCount++;
    }

    int Roll()
    {
        return (int)Mathf.Floor(Random.Range(1f,6f));
    }

    public override string ToString()
    {
        return _myType.ToString();
    }

    IEnumerator WorkTile(string resource)
    {
        _canRoll = false;
        if (resource == "chitin")
        {
            CheckNeighborsForTaverns();
            GameWarden.GatherMushwood(Roll() + tavernNeighborCount);
        }
        else
        {
            GameWarden.GatherCrystal(Roll() + tavernNeighborCount);
        }
        yield return new WaitForSeconds(1.5f * stableReductionPercent);
        _canRoll = true;
    }
}
