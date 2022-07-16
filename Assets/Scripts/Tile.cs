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

    SpriteRenderer _myRenderer;
    TileType _myType = TileType.Grassland;
    bool _canRoll;

    void Awake()
    {
        _myRenderer = gameObject.AddComponent<SpriteRenderer>();
        _myRenderer.transform.localPosition = new Vector3(0,0,0);
    }

    void Update()
    {
        _myRenderer.sprite = Resources.Load<Sprite>($"Sprites/{_myType.ToString()}");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hovering " + X+""+Y);
    }

    void OnMouseOver()
    {
        Debug.Log(X+""+Y);
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

    public override string ToString()
    {
        return _myType.ToString();
    }
}
