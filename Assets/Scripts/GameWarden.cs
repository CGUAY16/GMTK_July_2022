using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameWarden : MonoBehaviour
{
    public int Chitin;
    public int Crystal;
    public int Gold;

    public TextMeshProUGUI MushroomCount;
    public TextMeshProUGUI CrystalCount;

    Grid _grid;

    void Start()
    {
        float cellSize = Screen.height / 100 / 4.75f;
        this._grid = new Grid(10,6,cellSize,this.gameObject.transform,this);
    }

    // Update is called once per frame
    void Update()
    {
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
}
