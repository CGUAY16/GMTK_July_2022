using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Tile : MonoBehaviour
{
    public int X;
    public int Y;
    private float cdBetweenCollectionRoll = 5f;
    public GameWarden GameWarden;

    public int tavernNeighborCount = 0;
    public int stableNeighborCount = 0;
    public float stableReductionPercent = 1f;

    public SpriteRenderer MyRenderer { get; private set; }
    public TileType _myType = TileType.Grassland;
    private bool _canRoll = true;

    void Awake()
    {
        MyRenderer = gameObject.AddComponent<SpriteRenderer>();
        MyRenderer.transform.localPosition = new Vector3(0,0,0);
        MyRenderer.sprite = Resources.Load<Sprite>($"Sprites/{_myType.ToString()}");
    }

    void Update()
    {
        if (_myType == TileType.Lumberjack && _canRoll)
            StartCoroutine(WorkTile("Mushwood"));
        if (_myType == TileType.Quarry && _canRoll)
            StartCoroutine(WorkTile("Crystal"));
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // if player holding tile && heldtile is not equal to this tile's type, then...;
            if (GameWarden.HeldTile != -1 && GameWarden.HeldTile != (int)_myType)
            {
                _myType = (TileType)GameWarden.HeldTile;
                MyRenderer.sprite = Resources.Load<Sprite>($"Sprites/{_myType.ToString()}");
                GameWarden.Register((TileType)GameWarden.HeldTile);
                if(_myType == TileType.Church)
                {
                    GameWarden.churchesBoughtMultiplier++;
                }
                GameWarden.ClearHeld();
            }
        }
    }
    
    void CheckNeighborsForTaverns()
    {
        tavernNeighborCount = 0;

        if (X < GameWarden._grid._gridArray.GetLength(0) - 1)
            if (GameWarden._grid._gridArray[X + 1, Y]._myType == TileType.Tavern) // E
                tavernNeighborCount++;
        if (X < GameWarden._grid._gridArray.GetLength(0) - 1 && Y < GameWarden._grid._gridArray.GetLength(1) - 1)
            if (GameWarden._grid._gridArray[X + 1, Y + 1]._myType == TileType.Tavern) // NE
                tavernNeighborCount++;
        if (X < GameWarden._grid._gridArray.GetLength(0) - 1 && Y > 0)
            if (GameWarden._grid._gridArray[X + 1, Y - 1]._myType == TileType.Tavern) // SE
                tavernNeighborCount++;
        if (Y < GameWarden._grid._gridArray.GetLength(1) - 1)
            if (GameWarden._grid._gridArray[X, Y + 1]._myType == TileType.Tavern) // N
                tavernNeighborCount++;
        if (Y < GameWarden._grid._gridArray.GetLength(1) - 1 && X > 0)
            if (GameWarden._grid._gridArray[X - 1, Y + 1]._myType == TileType.Tavern) // NW
                tavernNeighborCount++;
        if (X > 0)
            if (GameWarden._grid._gridArray[X - 1, Y]._myType == TileType.Tavern) // W
                tavernNeighborCount++;
        if (X > 0 && Y > 0)
            if (GameWarden._grid._gridArray[X - 1, Y - 1]._myType == TileType.Tavern) // SW
                tavernNeighborCount++;
        if (Y > 0)
            if (GameWarden._grid._gridArray[X, Y - 1]._myType == TileType.Tavern) // S
                tavernNeighborCount++;
    }

    void CheckNeighborsForStables()
    {
        stableNeighborCount = 0;

        if (X < GameWarden._grid._gridArray.GetLength(0) - 1)
            if (GameWarden._grid._gridArray[X + 1, Y]._myType == TileType.Stable) // E
                stableNeighborCount++;
        if (X < GameWarden._grid._gridArray.GetLength(0) - 1 && Y < GameWarden._grid._gridArray.GetLength(1) - 1)
            if (GameWarden._grid._gridArray[X + 1, Y + 1]._myType == TileType.Stable) // NE
                stableNeighborCount++;
        if (X < GameWarden._grid._gridArray.GetLength(0) - 1 && Y > 0)
            if (GameWarden._grid._gridArray[X + 1, Y - 1]._myType == TileType.Stable) // SE
                stableNeighborCount++;
        if (Y < GameWarden._grid._gridArray.GetLength(1) - 1)
            if (GameWarden._grid._gridArray[X, Y + 1]._myType == TileType.Stable) // N
                stableNeighborCount++;
        if (Y < GameWarden._grid._gridArray.GetLength(1) - 1 && X > 0)
            if (GameWarden._grid._gridArray[X - 1, Y + 1]._myType == TileType.Stable) // NW
                stableNeighborCount++;
        if (X > 0)
            if (GameWarden._grid._gridArray[X - 1, Y]._myType == TileType.Stable) // W
                stableNeighborCount++;
        if (X > 0 && Y > 0)
            if (GameWarden._grid._gridArray[X - 1, Y - 1]._myType == TileType.Stable) // SW
                stableNeighborCount++;
        if (Y> 0)
            if (GameWarden._grid._gridArray[X, Y - 1]._myType == TileType.Stable) // S
                stableNeighborCount++;


        // stable percent calc.
        stableReductionPercent = 1 - (0.1f * stableNeighborCount);
    }

    int Roll()
    {
        return (int)Mathf.Floor(Random.Range(1f,6f));
    }

    IEnumerator WorkTile(string resource)
    {
        _canRoll = false;
        CheckNeighborsForStables();
        CheckNeighborsForTaverns();
        if (resource == "Mushwood")
        {
            GameWarden.GatherMushwood(Roll() + tavernNeighborCount);
        }
        else if (resource == "Crystal")
        {
            GameWarden.GatherCrystal(Roll() + tavernNeighborCount);
        }
        yield return new WaitForSeconds(cdBetweenCollectionRoll * stableReductionPercent);
        _canRoll = true;
    }

    public void UpdateCurrentTileSprite()
    {
        switch (_myType)
        {
            case TileType.Grassland:
                {
                    MyRenderer.sprite = Resources.Load<Sprite>($"Sprites/{_myType.ToString()}");
                    break;
                }
            case TileType.Lumberjack:
                {
                    MyRenderer.sprite = Resources.Load<Sprite>($"Sprites/{_myType.ToString()}");
                    break;
                }
            case TileType.Quarry:
                {
                    MyRenderer.sprite = Resources.Load<Sprite>($"Sprites/{_myType.ToString()}");
                    break;
                }
            case TileType.Tavern:
                {
                    MyRenderer.sprite = Resources.Load<Sprite>($"Sprites/{_myType.ToString()}");
                    break;
                }
            case TileType.Stable:
                {
                    MyRenderer.sprite = Resources.Load<Sprite>($"Sprites/{_myType.ToString()}");
                    break;
                }
            case TileType.Accountant:
                {
                    MyRenderer.sprite = Resources.Load<Sprite>($"Sprites/{_myType.ToString()}");
                    break;
                }
            case TileType.Church:
                {
                    MyRenderer.sprite = Resources.Load<Sprite>($"Sprites/{_myType.ToString()}");
                    break;
                }
        }
    }

    public void UpdateCurrentTileSprite(TileType type, bool faded)
    {
        switch (type)
        {
            case TileType.Grassland:
                {
                    MyRenderer.sprite = Resources.Load<Sprite>($"Sprites/{type.ToString()}");
                    break;
                }
            case TileType.Lumberjack:
                {
                    MyRenderer.sprite = Resources.Load<Sprite>($"Sprites/{type.ToString()}");
                    break;
                }
            case TileType.Quarry:
                {
                    MyRenderer.sprite = Resources.Load<Sprite>($"Sprites/{type.ToString()}");
                    break;
                }
            case TileType.Tavern:
                {
                    MyRenderer.sprite = Resources.Load<Sprite>($"Sprites/{type.ToString()}");
                    break;
                }
            case TileType.Stable:
                {
                    MyRenderer.sprite = Resources.Load<Sprite>($"Sprites/{type.ToString()}");
                    break;
                }
            case TileType.Accountant:
                {
                    MyRenderer.sprite = Resources.Load<Sprite>($"Sprites/{type.ToString()}");
                    break;
                }
            case TileType.Church:
                {
                    if (faded)
                    {
                        MyRenderer.sprite = Resources.Load<Sprite>($"Sprites/FadedChurch");
                    }
                    else
                        MyRenderer.sprite = Resources.Load<Sprite>($"Sprites/{type.ToString()}");
                    break;
                }
        }
    }
}
