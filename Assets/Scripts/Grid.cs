using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int _width;
    private int _height;
    public Tile[,] _gridArray;
    private float _cellSize;
    private GameWarden _gameWarden;

    public Grid (int width, int height, float cellSize, Transform parent, GameWarden gameWarden)
    {
        this._width = width;
        this._height = height;
        this._cellSize = cellSize;
        this._gameWarden = gameWarden;

        _gridArray = new Tile[_width, _height];

        for (int x = 0; x < _gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < _gridArray.GetLength(1); y++)
            {
                _gridArray[x,y] = new GameObject(x + "" + y).AddComponent<Tile>();
                _gridArray[x,y].transform.SetParent(parent);
                _gridArray[x,y].transform.position = GetWorldPosition(x,y) + new Vector3(_cellSize / 2, _cellSize / 2, 0);
                _gridArray[x,y].X = x;
                _gridArray[x,y].Y = y;
                _gridArray[x,y].GameWarden = _gameWarden;
                BoxCollider2D col = _gridArray[x,y].gameObject.AddComponent<BoxCollider2D>();
                col.size = new Vector2(_cellSize, _cellSize);
            }
        }
    }

    public void Print ()
    {
        for (int x = 0; x < _gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < _gridArray.GetLength(1); y++)
            {
                Debug.DrawLine(GetWorldPosition(x,y), GetWorldPosition(x,y+1), Color.magenta);
                Debug.DrawLine(GetWorldPosition(x,y), GetWorldPosition(x+1,y), Color.magenta);
            }
        }
    }

    public void ToggleSprites()
    {
        for (int x = 0; x < _gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < _gridArray.GetLength(1); y++)
            {
                SpriteRenderer _sprite = _gridArray[x,y].GetComponent<SpriteRenderer>();
                _sprite.enabled = !_sprite.enabled;
            }
        }
    }

    private Vector3 GetWorldPosition(int x, int y, bool border=true)
    {
        Vector3 newV = new Vector3(x, y) * _cellSize;
        if (border)
            newV += new Vector3(_cellSize, _cellSize);
        return new Vector3(x, y) * _cellSize;
    }
}
