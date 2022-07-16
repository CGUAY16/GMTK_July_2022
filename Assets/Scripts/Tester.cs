using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    Grid grid;

    // Start is called before the first frame update
    void Start()
    {
        float cellSize = Screen.height / 100 / 4.75f;
        // Debug.Log(cellSize);
        this.grid = new Grid(10,6,cellSize,this.gameObject.transform);
        grid.Print();
    }

    // Update is called once per frame
    void Update()
    {
        // grid.Print();
    }
}
