using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWarden : MonoBehaviour
{
    // public Sprite[] TileSprites;

    Grid grid;

    int chitin;
    int stone;
    int gold;

    void Start()
    {
        float cellSize = Screen.height / 100 / 4.75f;
        this.grid = new Grid(10,6,cellSize,this.gameObject.transform);
        grid.Print();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GatherChitin(int mushrooms)
    {
        chitin += mushrooms;
    }

    void GatherStone(int rocks)
    {
        stone += rocks;
    }
}
