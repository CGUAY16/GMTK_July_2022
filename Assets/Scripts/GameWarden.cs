using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public int churchLives;
    public float Timer = 5f;
    public float TimerReset = 1f;

    //max amt of certain buildings.
    public int churchBuildMax = 1;
    public int churchesBoughtMultiplier = 0;
    public int accountantBuildMax = 10;
    

    public int[] taxAmtArray = {150, 300, 450, 600, 750, 900, 1050, 1200, 1350, 1500};
    private int taxArrayIndex = 0;
    public int currentTaxAmt;

    public TextMeshProUGUI MushwoodCount;
    public TextMeshProUGUI CrystalCount;
    public TextMeshProUGUI SporesCount;
    public Image HeldTileVisual;
    public Color32 nullLook;
    public Color32 fixingAlpha;

    public Dictionary<TileType,int> TileCounts = new Dictionary<TileType,int>();

    public Grid _grid;
    public Transform gridIniPos;

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
        HeldTileVisual.color = nullLook;
        SoundManager.PlayMusic(SoundManager.MusicType.GameplayLoop, soundDataRef.musicFiles);
        
        Spores = StartingSpores;
        SporesCount.SetText(Spores.ToString());

        // Grid && tile info setup
        float cellSize = Screen.height / 50 / 4.75f;
        this._grid = new Grid(10,6,cellSize,this.gameObject.transform,this, gridIniPos.position);
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
            // cabin
            if (Input.GetKeyDown(KeyCode.Alpha1) && Spores >= (LumberjackPrice + 5 * TileCounts[TileType.Lumberjack]))
            {
                Debug.Log("hello");
                HeldTile = 1;
                HeldTileVisual.color = fixingAlpha;
                HeldTileVisual.sprite = Resources.Load<Sprite>("Sprites/Lumberjack");
                RemoveSpores((LumberjackPrice + 5 * TileCounts[TileType.Lumberjack]));
            }
            // quarry
            if (Input.GetKeyDown(KeyCode.Alpha2) && Spores >= (QuarryPrice + 5 * TileCounts[TileType.Quarry]))
            {
                HeldTile = 2;
                HeldTileVisual.color = fixingAlpha;
                HeldTileVisual.sprite = Resources.Load<Sprite>("Sprites/Quarry");
                RemoveSpores((QuarryPrice + 5 * TileCounts[TileType.Quarry]));
            }
            // tavern
            if (Input.GetKeyDown(KeyCode.Alpha3) && Spores >= (TavernPrice + 5 * TileCounts[TileType.Tavern]))
            {
                HeldTile = 3;
                HeldTileVisual.color = fixingAlpha;
                HeldTileVisual.sprite = Resources.Load<Sprite>("Sprites/Tavern");
                RemoveSpores((TavernPrice + 5 * TileCounts[TileType.Tavern]));
            }
            // stable
            if (Input.GetKeyDown(KeyCode.Alpha4) && Spores >= (StablePrice + 5 * TileCounts[TileType.Tavern]))
            {
                HeldTile = 4;
                HeldTileVisual.color = fixingAlpha;
                HeldTileVisual.sprite = Resources.Load<Sprite>("Sprites/Stable");
                RemoveSpores((StablePrice + 5 * TileCounts[TileType.Stable]));
            }
            // accountant
            if (Input.GetKeyDown(KeyCode.Alpha5) 
                && Spores >= (AccountantPrice + 25 * TileCounts[TileType.Accountant]) 
                && (TileCounts[TileType.Accountant] < accountantBuildMax))
            {
                HeldTile = 5;
                HeldTileVisual.color = fixingAlpha;
                HeldTileVisual.sprite = Resources.Load<Sprite>("Sprites/Accountant");
                RemoveSpores((AccountantPrice + 25 * TileCounts[TileType.Accountant]));
            }
            // church
            if (Input.GetKeyDown(KeyCode.Alpha6) 
                && Spores >= (ChurchPrice + 100 * churchesBoughtMultiplier) 
                && (TileCounts[TileType.Church] < churchBuildMax))
            {
                HeldTile = 6;
                HeldTileVisual.color = fixingAlpha;
                HeldTileVisual.sprite = Resources.Load<Sprite>("Sprites/Church");
                RemoveSpores((ChurchPrice + 100 * churchesBoughtMultiplier));
            }
        }

        // If spacebar pushed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Sell();
        }
    }

    public void ClearHeld()
    {
        HeldTile = -1;
        HeldTileVisual.sprite = null;
        HeldTileVisual.color = nullLook;
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

    public int Register(TileType tile)
    {
        if (tile == TileType.Church)
        {
            churchLives++;
            Debug.Log(churchLives);
        }
        return ++TileCounts[tile];
    }

    // three states
    // - player cant pay taxes
    // - player cant pay taxes but has lives
    // - player pays the taxes
    public void TaxThePlayer()
    {
        if (Spores < currentTaxAmt
            && churchLives <= 0)
        {
            Debug.Log("You failed to pay the Taxes! Fear the King's Dice");
            totalTimeRef.PauseTimer();
            _behavior.isTaxSeason = true;
            
        }
        if(Spores < currentTaxAmt && churchLives > 0)
        {
            // THE CHURCH SAVES YOU THIS TIME MAYOR!! SMITE THAT CHURCH!!
            _grid.GrayOutChurchTiles();
            churchLives--;
            Debug.Log(churchLives);
            _grid.RemoveTiles(1, TileType.Church);

            UpdateTaxAmt();
            isItTaxTime = true;
        }
        else
        {
            SoundManager.PlaySound(SoundManager.SoundType.TaxesPaid, soundDataRef.soundFiles);
            RemoveSpores(currentTaxAmt);
            StartCoroutine(Accountant(AccountantBehavior()));
            UpdateTaxAmt();
            
            isItTaxTime = true;
        }
    }

    public int AccountantBehavior()
    {
        int result = (int)Mathf.Floor(currentTaxAmt * (0.1f * TileCounts[TileType.Accountant]));
        Debug.Log(currentTaxAmt * (0.1f * TileCounts[TileType.Accountant]));
        return result;
    }

    IEnumerator Accountant(int amt)
    {
        yield return new WaitForSeconds(1f);
        AddSpores(amt);
    }

    public void UpdateTaxAmt()
    {
        taxArrayIndex++;
        if(taxArrayIndex > taxAmtArray.Length - 1)
        {
            // The player wins the game! so a scene change is necessary
            SceneManager.LoadScene(SceneChange.WinScreen.ToString());
        }
        else
        {
            currentTaxAmt = taxAmtArray[taxArrayIndex];
            taxTimeRef.RestartTimer();
        }
        taxAmtTextRef.text = currentTaxAmt.ToString();

    }

    public bool IsTheGridEmptyOfBuildings()
    {
        bool result = false;

        if (TileCounts[TileType.Lumberjack] == 0 
            && TileCounts[TileType.Quarry] == 0
            && TileCounts[TileType.Tavern] == 0
            && TileCounts[TileType.Stable] == 0
            && TileCounts[TileType.Accountant]== 0
            && TileCounts[TileType.Church] == 0)
        {
            result = true;
            return result;
        }

        return result;
    }

}
