using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameWarden : MonoBehaviour
{
    public int Mushwood;
    public int Crystal;
    public int Gold;
    public int HeldTile = -1;
    public int StartingGold;
    public int LumberjackPrice;
    public int QuarryPrice;
    public int TavernPrice;
    public int StablePrice;
    public int AccountantPrice;
    public int ChurchPrice;
    public float Timer = 5f;
    public float TimerReset = 1f;

    public TextMeshProUGUI MushroomCount;
    public TextMeshProUGUI CrystalCount;
    public TextMeshProUGUI GoldCount;
    public SpriteRenderer HeldTileIcon;

    public Dictionary<TileType,int> TileCounts = new Dictionary<TileType,int>();

    public Grid _grid;

    public KingsDice_Behavior _behavior;
    public Music_SFX_manager soundManager;

    Time time;


    void Start()
    {
        Gold = StartingGold;
        float cellSize = Screen.height / 50 / 4.75f;
        this._grid = new Grid(10,6,cellSize,this.gameObject.transform,this);
        string[] TileTypeNames = System.Enum.GetNames (typeof(TileType));
        for(int i = 1; i < TileTypeNames.Length; i++)
            TileCounts.Add((TileType)i, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0)
            Tax();

        if (Input.GetMouseButtonDown(2))
        {
            ClearHeld();
            HeldTile = -1;
        }

        if(HeldTile == -1)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && Gold >= (LumberjackPrice + 5 * TileCounts[TileType.Lumberjack]))
            {
                HeldTile = 1;
                HeldTileIcon.sprite = Resources.Load<Sprite>("Sprites/Lumberjack");
                RemoveGold((LumberjackPrice + 5 * TileCounts[TileType.Lumberjack]));
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && Gold >= (QuarryPrice + 5 * TileCounts[TileType.Quarry]))
            {
                HeldTile = 2;
                HeldTileIcon.sprite = Resources.Load<Sprite>("Sprites/Quarry");
                RemoveGold((QuarryPrice + 5 * TileCounts[TileType.Quarry]));
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && Gold >= (TavernPrice + 5 * TileCounts[TileType.Tavern]))
            {
                HeldTile = 3;
                HeldTileIcon.sprite = Resources.Load<Sprite>("Sprites/Tavern");
                RemoveGold((TavernPrice + 5 * TileCounts[TileType.Tavern]));
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && Gold >= (StablePrice + 5 * TileCounts[TileType.Tavern]))
            {
                HeldTile = 4;
                HeldTileIcon.sprite = Resources.Load<Sprite>("Sprites/Stable");
                RemoveGold((StablePrice + 5 * TileCounts[TileType.Tavern]));
            }
            if (Input.GetKeyDown(KeyCode.Alpha5) && Gold >= (AccountantPrice + 25 * TileCounts[TileType.Accountant]))
            {
                HeldTile = 5;
                HeldTileIcon.sprite = Resources.Load<Sprite>("Sprites/Accountant");
                RemoveGold((AccountantPrice + 25 * TileCounts[TileType.Accountant]));
            }
            if (Input.GetKeyDown(KeyCode.Alpha6) && Gold >= (ChurchPrice + 25 * TileCounts[TileType.Church]))
            {
                HeldTile = 6;
                HeldTileIcon.sprite = Resources.Load<Sprite>("Sprites/Church");
                RemoveGold((ChurchPrice + 25 * TileCounts[TileType.Church]));
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Sell();
        }
    }

    public void Sell()
    {
        AddGold(Mushwood + Crystal);
        Mushwood = 0;
        MushroomCount.SetText(Mushwood.ToString());
        Crystal = 0;
        CrystalCount.SetText(Crystal.ToString());
    }

    public void AddGold(int coins)
    {
        Gold += coins;
        GoldCount.SetText(Gold.ToString());
    }

    public void RemoveGold(int coins)
    {
        Gold -= coins;
        GoldCount.SetText(Gold.ToString());
    }

    public void GatherMushwood(int mushrooms)
    {
        Mushwood += mushrooms;
        MushroomCount.SetText(Mushwood.ToString());
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

    public void Tax()
    {
        Timer = 10f;
        int taxAmt = (int)Mathf.Floor(0 * (Mathf.Pow(1.01f, Time.time)));
        if(Gold <= taxAmt)
        {
            Debug.Log("0 tax");
            _behavior.isTaxSeason = true;
        }
        else
        {
            RemoveGold(taxAmt);
        }
        StartCoroutine(Church((int)Mathf.Floor(taxAmt * (0.1f * TileCounts[TileType.Church]))));
    }

    IEnumerator Church(int amt)
    {
        yield return new WaitForSeconds(1f);
        AddGold(amt);
    }
}
