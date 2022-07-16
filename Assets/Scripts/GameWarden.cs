using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameWarden : MonoBehaviour
{
    public int Chitin;
    public int Crystal;
    public int Gold;
    public int HeldTile = -1;

    public TextMeshProUGUI MushroomCount;
    public TextMeshProUGUI CrystalCount;
    public SpriteRenderer HeldTileIcon;

    public Dictionary<TileType,int> TileCounts = new Dictionary<TileType,int>();

    Grid _grid;

    void Start()
    {
        float cellSize = Screen.height / 50 / 4.75f;
        this._grid = new Grid(10,6,cellSize,this.gameObject.transform,this);
        string[] TileTypeNames = System.Enum.GetNames (typeof(TileType));
        for(int i = 1; i < TileTypeNames.Length; i++)
            TileCounts.Add((TileType)i, 0);
    }

    // Update is called once per frame
    void Update()
    { 
        // TODO Convert to switch statement
        if (Input.GetMouseButtonDown(2))
        {
            ClearHeld();
            HeldTile = -1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            HeldTile = 1;
            HeldTileIcon.sprite = Resources.Load<Sprite>("Sprites/Lumberjack");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            HeldTile = 2;
            HeldTileIcon.sprite = Resources.Load<Sprite>("Sprites/Quarry");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            HeldTile = 3;
            HeldTileIcon.sprite = Resources.Load<Sprite>("Sprites/Tavern");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            HeldTile = 4;
            HeldTileIcon.sprite = Resources.Load<Sprite>("Sprites/Stable");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            HeldTile = 5;
            HeldTileIcon.sprite = Resources.Load<Sprite>("Sprites/Accountant");
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            HeldTile = 6;
            HeldTileIcon.sprite = Resources.Load<Sprite>("Sprites/Church");
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            HeldTile = 7;
            HeldTileIcon.sprite = Resources.Load<Sprite>("Sprites/Road");
        }
        // Debug.Log(""+TileCounts[(TileType)1]+TileCounts[(TileType)2]+TileCounts[(TileType)3]+TileCounts[(TileType)4]+TileCounts[(TileType)5]+TileCounts[(TileType)6]+TileCounts[(TileType)7]);
    }

    public void GatherChitin(int mushrooms)
    {
        Chitin += mushrooms;
        MushroomCount.SetText(Chitin.ToString());
    }

    public void GatherCrystal(int rocks)
    {
        Crystal += rocks;
        CrystalCount.SetText(Crystal.ToString());
    }

    public void ClearHeld()
    {
        HeldTile = -1;
        HeldTileIcon.sprite = null;
    }

    public int Register(TileType tile)
    {
        return ++TileCounts[tile];
    }
}
