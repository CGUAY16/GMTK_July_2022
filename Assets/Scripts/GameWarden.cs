using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameWarden : MonoBehaviour
{
    public int Mushwood;
    public int Crystal;
    public int Spores;
    public int HeldTile = -1;
    public int StartingSpores;
    public int LumberjackPrice;
    public int QuarryPrice;
    public int TavernPrice;
    public int StablePrice;
    public int AccountantPrice;
    public int ChurchPrice;
    public float Timer = 5f;
    public float TimerReset = 1f;
    

    public int[] taxAmtArray = {150, 300, 450, 600, 750, 900, 1050, 1200, 1350, 1500};
    private int taxArrayIndex = 0;
    public int currentTaxAmt;

    public TextMeshProUGUI MushwoodCount;
    public TextMeshProUGUI CrystalCount;
    public TextMeshProUGUI SporesCount;
    public SpriteRenderer HeldTileIcon;

    public Dictionary<TileType,int> TileCounts = new Dictionary<TileType,int>();

    public Grid _grid;

    // KingsDiceReferences
    public KingsDice_Behavior _behavior;
    public GameObject KingsDiceUiPanelRef;

    public SoundData soundDataRef;
    public TMP_Text taxAmtTextRef;
    public Timer totalTimeRef;
    public Timer taxTimeRef;
    public bool isItTaxTime;

    void Start()
    {
        SoundManager.PlayMusic(SoundManager.MusicType.GameplayLoop, soundDataRef.musicFiles);
        
        Spores = StartingSpores;
        SporesCount.SetText(Spores.ToString());

        // Grid && tile info setup
        float cellSize = Screen.height / 50 / 4.75f;
        this._grid = new Grid(10,6,cellSize,this.gameObject.transform,this);
        string[] TileTypeNames = System.Enum.GetNames (typeof(TileType));
        for(int i = 1; i < TileTypeNames.Length; i++)
        {
            TileCounts.Add((TileType)i, 0);
        }
            

        // Tax Amt Setup
        currentTaxAmt = taxAmtArray[0];
        taxAmtTextRef.text = currentTaxAmt.ToString();
        isItTaxTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Timer Shenanigans
        if ((taxTimeRef.timeRemaining == 0f) && (isItTaxTime))
        {
            Debug.Log("I saw this...");
            isItTaxTime = false;
            TaxThePlayer();
        }

        // Held Tile
        if (Input.GetMouseButtonDown(2))
        {
            ClearHeld();
            HeldTile = -1;
        }

        // Held Tile(cont)
        if(HeldTile == -1)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && Spores >= (LumberjackPrice + 5 * TileCounts[TileType.Lumberjack]))
            {
                Debug.Log("hello");
                HeldTile = 1;
                HeldTileIcon.sprite = Resources.Load<Sprite>("Sprites/Lumberjack");
                RemoveSpores((LumberjackPrice + 5 * TileCounts[TileType.Lumberjack]));
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && Spores >= (QuarryPrice + 5 * TileCounts[TileType.Quarry]))
            {
                HeldTile = 2;
                HeldTileIcon.sprite = Resources.Load<Sprite>("Sprites/Quarry");
                RemoveSpores((QuarryPrice + 5 * TileCounts[TileType.Quarry]));
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && Spores >= (TavernPrice + 5 * TileCounts[TileType.Tavern]))
            {
                HeldTile = 3;
                HeldTileIcon.sprite = Resources.Load<Sprite>("Sprites/Tavern");
                RemoveSpores((TavernPrice + 5 * TileCounts[TileType.Tavern]));
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && Spores >= (StablePrice + 5 * TileCounts[TileType.Tavern]))
            {
                HeldTile = 4;
                HeldTileIcon.sprite = Resources.Load<Sprite>("Sprites/Stable");
                RemoveSpores((StablePrice + 5 * TileCounts[TileType.Tavern]));
            }
            if (Input.GetKeyDown(KeyCode.Alpha5) && Spores >= (AccountantPrice + 25 * TileCounts[TileType.Accountant]))
            {
                HeldTile = 5;
                HeldTileIcon.sprite = Resources.Load<Sprite>("Sprites/Accountant");
                RemoveSpores((AccountantPrice + 25 * TileCounts[TileType.Accountant]));
            }
            if (Input.GetKeyDown(KeyCode.Alpha6) && Spores >= (ChurchPrice + 25 * TileCounts[TileType.Church]))
            {
                HeldTile = 6;
                HeldTileIcon.sprite = Resources.Load<Sprite>("Sprites/Church");
                RemoveSpores((ChurchPrice + 25 * TileCounts[TileType.Church]));
            }
        }

        // If spacebar pushed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Sell();
        }
    }

    public void Sell()
    {
        AddSpores(Mushwood + Crystal);
        Mushwood = 0;
        MushwoodCount.SetText(Mushwood.ToString());
        Crystal = 0;
        CrystalCount.SetText(Crystal.ToString());
        SoundManager.PlaySound(SoundManager.SoundType.Sell, soundDataRef.soundFiles);
    }

    public void AddSpores(int coins)
    {
        Spores += coins;
        SporesCount.SetText(Spores.ToString());
    }

    public void RemoveSpores(int coins)
    {
        Spores -= coins;
        SporesCount.SetText(Spores.ToString());
    }

    public void GatherMushwood(int mushwood)
    {
        Mushwood += mushwood;
        MushwoodCount.SetText(Mushwood.ToString());
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

    //Deprecated func
    public void Tax()
    {
        Timer = 10f;
        int taxAmt = (int)Mathf.Floor(0 * (Mathf.Pow(1.01f, Time.time)));
        int taxAmtToSendToPlayer = (int)Mathf.Floor(0 * (Mathf.Pow(1.01f, Time.time + 60f)));

        if (Spores <= taxAmt)
        {
            Debug.Log("0 tax");
            _behavior.isTaxSeason = true;
        }
        else
        {
            RemoveSpores(taxAmt);
        }
        StartCoroutine(Church((int)Mathf.Floor(taxAmt * (0.1f * TileCounts[TileType.Church]))));
    }

    public void TaxThePlayer()
    {
        if(Spores < currentTaxAmt)
        {
            Debug.Log("You failed to pay the Taxes! Fear the King's Dice");
            totalTimeRef.PauseTimer();
            _behavior.isTaxSeason = true;
            
        }
        else
        {
            SoundManager.PlaySound(SoundManager.SoundType.TaxesPaid, soundDataRef.soundFiles);
            RemoveSpores(currentTaxAmt);
            UpdateTaxAmt();
            taxTimeRef.RestartTimer();
            isItTaxTime = true;
        }
        StartCoroutine(Church((int)Mathf.Floor(currentTaxAmt * (0.1f * TileCounts[TileType.Church]))));
    }

    IEnumerator Church(int amt)
    {
        yield return new WaitForSeconds(1f);
        AddSpores(amt);
    }

    public void UpdateTaxAmt()
    {
        taxArrayIndex++;
        if(taxArrayIndex > taxAmtArray.Length)
        {
            // The player wins the game! so a scene change is necessary
        }
        else
        {
            currentTaxAmt = taxAmtArray[taxArrayIndex];
        }
        
        taxAmtTextRef.text = currentTaxAmt.ToString();

    }
}
