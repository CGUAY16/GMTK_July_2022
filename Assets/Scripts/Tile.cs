using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Tile : MonoBehaviour
{
    enum TileType { Grassland, Lumberjack, Quarry, Tavern, Stable, Accountant, Church, Road };

    public int X;
    public int Y;
    public GameWarden GameWarden;

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
        if(Input.GetMouseButtonDown(0))
        {
            Iterate();
        }
        if(Input.GetMouseButtonDown(1))
        {
            Deiterate();
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

    void CheckNeighbors(Tile[,] grid)
    {
        Debug.Log(grid[X+1,Y]);
        Debug.Log(grid[X,Y+1]);
        Debug.Log(grid[X-1,Y]);
        Debug.Log(grid[X,Y-1]);
    }

    int Roll()
    {
        int num = (int)Mathf.Floor(Random.Range(1f,6f));
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
            GameWarden.GatherChitin(Roll());
        else
            GameWarden.GatherCrystal(Roll());
        yield return new WaitForSeconds(1.5f);
        _canRoll = true;
    }
}
