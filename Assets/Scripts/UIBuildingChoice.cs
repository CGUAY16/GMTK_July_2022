using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuildingChoice : MonoBehaviour
{
    public GameWarden gameWardenRef;
    
    public void SelectCabin()
    {
        if(gameWardenRef.HeldTile == -1)
        {
            if (gameWardenRef.Spores >= (gameWardenRef.LumberjackPrice + 5 * gameWardenRef.TileCounts[TileType.Lumberjack]))
            {
                gameWardenRef.HeldTile = 1;
                gameWardenRef.HeldTileVisual.color = gameWardenRef.fixingAlpha;
                gameWardenRef.HeldTileVisual.sprite = Resources.Load<Sprite>("Sprites/Lumberjack");
                gameWardenRef.RemoveSpores((gameWardenRef.LumberjackPrice + 5 * gameWardenRef.TileCounts[TileType.Lumberjack]));
            }
        }
    }

    public void SelectQuarry()
    {
        if (gameWardenRef.HeldTile == -1)
        {
            if (gameWardenRef.Spores >= (gameWardenRef.QuarryPrice + 5 * gameWardenRef.TileCounts[TileType.Quarry]))
            {
                gameWardenRef.HeldTile = 2;
                gameWardenRef.HeldTileVisual.color = gameWardenRef.fixingAlpha;
                gameWardenRef.HeldTileVisual.sprite = Resources.Load<Sprite>("Sprites/Quarry");
                gameWardenRef.RemoveSpores((gameWardenRef.QuarryPrice + 5 * gameWardenRef.TileCounts[TileType.Quarry]));
            }
        }
    }

    public void SelectTavern()
    {
        if (gameWardenRef.HeldTile == -1)
        {
            if(gameWardenRef.Spores >= (gameWardenRef.TavernPrice + 5 * gameWardenRef.TileCounts[TileType.Tavern]))
            {
                gameWardenRef.HeldTile = 3;
                gameWardenRef.HeldTileVisual.color = gameWardenRef.fixingAlpha;
                gameWardenRef.HeldTileVisual.sprite = Resources.Load<Sprite>("Sprites/Tavern");
                gameWardenRef.RemoveSpores((gameWardenRef.TavernPrice + 5 * gameWardenRef.TileCounts[TileType.Tavern]));
            }
        }
    }

    public void SelectStable()
    {
        if (gameWardenRef.HeldTile == -1)
        {
            if (gameWardenRef.Spores >= (gameWardenRef.StablePrice + 5 * gameWardenRef.TileCounts[TileType.Stable]))
            {
                gameWardenRef.HeldTile = 4;
                gameWardenRef.HeldTileVisual.color = gameWardenRef.fixingAlpha;
                gameWardenRef.HeldTileVisual.sprite = Resources.Load<Sprite>("Sprites/Stable");
                gameWardenRef.RemoveSpores((gameWardenRef.StablePrice + 5 * gameWardenRef.TileCounts[TileType.Stable]));
            }
            
        }
        
    }

    public void SelectAccoutant()
    {
        if(gameWardenRef.HeldTile == -1)
        {
            if (gameWardenRef.Spores >= (gameWardenRef.AccountantPrice + 25 * gameWardenRef.TileCounts[TileType.Accountant])
                && (gameWardenRef.TileCounts[TileType.Accountant] < 10))
            {
                gameWardenRef.HeldTile = 5;
                gameWardenRef.HeldTileVisual.color = gameWardenRef.fixingAlpha;
                gameWardenRef.HeldTileVisual.sprite = Resources.Load<Sprite>("Sprites/Accountant");
                gameWardenRef.RemoveSpores((gameWardenRef.AccountantPrice + 25 * gameWardenRef.TileCounts[TileType.Accountant]));
            }
        }
        
    }

    public void SelectChurch()
    {
        if(gameWardenRef.HeldTile == -1)
        {
            if (gameWardenRef.Spores >= (gameWardenRef.ChurchPrice + 100 * gameWardenRef.churchesBoughtMultiplier)
                && (gameWardenRef.TileCounts[TileType.Church] < 3))
            {
                gameWardenRef.HeldTile = 6;
                gameWardenRef.HeldTileVisual.color = gameWardenRef.fixingAlpha;
                gameWardenRef.HeldTileVisual.sprite = Resources.Load<Sprite>("Sprites/Church");
                gameWardenRef.RemoveSpores((gameWardenRef.ChurchPrice + 100 * gameWardenRef.churchesBoughtMultiplier));
            }
        }
        
    }

}
