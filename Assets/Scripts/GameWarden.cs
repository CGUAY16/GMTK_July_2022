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
    public int RoadPrice;
    public float Timer = 5f;
    public float TimerReset = 1f;

    public TextMeshProUGUI MushroomCount;
    public TextMeshProUGUI CrystalCount;
    public TextMeshProUGUI GoldCount;
    public SpriteRenderer HeldTileIcon;

    public Dictionary<TileType,int> TileCounts = new Dictionary<TileType,int>();

    public Grid _grid;

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
        // Debug.Log(Timer);
        if (Timer <= 0)
            Tax();

        // TODO Convert to switch statement
        if (Input.GetMouseButtonDown(2))
        {
            ClearHeld();
            HeldTile = -1;
        }

        if(HeldTile == -1)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && Gold >= LumberjackPrice)
            {
                HeldTile = 1;
                HeldTileIcon.sprite = Resources.Load<Sprite>("Sprites/Lumberjack");
                RemoveGold(LumberjackPrice);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && Gold >= QuarryPrice)
            {
                HeldTile = 2;
                HeldTileIcon.sprite = Resources.Load<Sprite>("Sprites/Quarry");
                RemoveGold(QuarryPrice);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && Gold >= TavernPrice)
            {
                HeldTile = 3;
                HeldTileIcon.sprite = Resources.Load<Sprite>("Sprites/Tavern");
                RemoveGold(TavernPrice);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && Gold >= StablePrice)
            {
                HeldTile = 4;
                HeldTileIcon.sprite = Resources.Load<Sprite>("Sprites/Stable");
                RemoveGold(StablePrice);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5) && Gold >= AccountantPrice)
            {
                HeldTile = 5;
                HeldTileIcon.sprite = Resources.Load<Sprite>("Sprites/Accountant");
                RemoveGold(AccountantPrice);
            }
            if (Input.GetKeyDown(KeyCode.Alpha6) && Gold >= ChurchPrice)
            {
                HeldTile = 6;
                HeldTileIcon.sprite = Resources.Load<Sprite>("Sprites/Church");
                // RemoveGold(ChurchPrice);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Sell();
        }
    }

    public void Sell()
    {
        AddGold(Chitin + Crystal);
        Chitin = 0;
        MushroomCount.SetText(Chitin.ToString());
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
        int taxAmt = (int)Mathf.Floor(100 * (Mathf.Pow(1.01f, Time.time)));
        if(Gold <= taxAmt)
        {
            Debug.Log("0 tax");
        }
        else
        {
            RemoveGold(taxAmt);
        }
        StartCoroutine(Church((int)Mathf.Floor(taxAmt * (0.1f * TileCounts[TileType.Church]))));
    }

    IEnumerator Church(int amt)
    {
        yield return new WaitForSeconds(3f);
        AddGold(amt);
    }
}
