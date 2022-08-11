using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_buildingCostUpdate : MonoBehaviour
{
    public GameWarden GMref;
    private TMP_Text priceObject;
    public TileType tileType;
    // Start is called before the first frame update
    void Start()
    {
        priceObject = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (tileType)
        {
            case TileType.Lumberjack:
                {
                    priceObject.text = $"{(GMref.LumberjackPrice + 5 * GMref.TileCounts[TileType.Lumberjack])}";
                    break;
                }
            case TileType.Quarry:
                {
                    priceObject.text = $"{(GMref.QuarryPrice + 5 * GMref.TileCounts[TileType.Quarry])}";
                    break;
                }

            case TileType.Tavern:
                {
                    priceObject.text = $"{(GMref.TavernPrice + 5 * GMref.TileCounts[TileType.Tavern])}";
                    break;
                }

            case TileType.Stable:
                {
                    priceObject.text = $"{(GMref.StablePrice + 5 * GMref.TileCounts[TileType.Stable])}";
                    break;
                }

            case TileType.Accountant:
                {
                    priceObject.text = $"{(GMref.AccountantPrice + 25 * GMref.TileCounts[TileType.Accountant])}";
                    break;
                }

            case TileType.Church:
                {
                    priceObject.text = $"{(GMref.ChurchPrice + 100 * GMref.churchesBoughtMultiplier)}";
                    break;
                }
        }
        
    }
}
