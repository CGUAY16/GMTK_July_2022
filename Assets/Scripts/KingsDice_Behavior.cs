using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KingsDice_Behavior : MonoBehaviour
{
    public bool isTaxSeason = false;
    public GameObject KingsDicePanel;
    public Image Dice1;
    public Image Dice2;

    public GameWarden gameWarden;

    public Timer TotalTimeRef;
    public Timer TaxTimeRef;

    /* indexes:
     * 0 = logging cabin
     * 1 = quarry
     * 2 = tavern
     * 3 = stable
     * 4 = bank/accountant
     * 5 = church
     */
    public Sprite[] kingsDiceSpriteList;

    /*
     * numbers from 1 to 6
     */ 
    public Sprite[] kingsDiceSpriteList2;

    public Sprite defaultEmpty;
    public SoundData soundDataRef;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTaxSeason)
        {
            // if tile count in dictionary is == 0, then we game over the player.
            isTaxSeason = false;
            gameWarden._grid.ToggleSprites();
            StartCoroutine(BehaviorLoop());
        }
    }

    public int PickRandomBuilding()
    {
        int randomNum = (int)Mathf.Floor(Random.Range(1f, 6f)) - 1;
        Dice1.sprite = kingsDiceSpriteList[randomNum];
        return randomNum;
    }
    
    public int PickRandomNum()
    {
        int random = (int)Mathf.Floor(Random.Range(1f, 6f)) - 1;
        Dice2.sprite = kingsDiceSpriteList2[random];
        return random + 1;
    }

    public IEnumerator BehaviorLoop()
    {
        KingsDicePanel.SetActive(true);
        SoundManager.PlayMusic(SoundManager.MusicType.KingsPunishment, soundDataRef.musicFiles);
        SoundManager.PlaySound(SoundManager.SoundType.KingsDiceRoll, soundDataRef.soundFiles);
        yield return new WaitForSeconds(4f);

        int randBuilding = PickRandomBuilding();
        TileType KingsChoice = FromIntToEnum(randBuilding);
        int randNum = PickRandomNum();
        yield return new WaitForSeconds(2f);

        gameWarden._grid.RemoveTiles(randNum, KingsChoice);
        // code that applies the removal of buildings
        yield return new WaitForSeconds(2.25f);

        SoundManager.PlayMusic(SoundManager.MusicType.GameplayLoop, soundDataRef.musicFiles);
        gameWarden._grid.ToggleSprites();
        KingsDicePanel.SetActive(false);
        TotalTimeRef.UnPauseTimer();
        TaxTimeRef.RestartTimer();
        gameWarden.isItTaxTime = true;
    }

    public TileType FromIntToEnum(int num)
    {
        TileType result;

        switch (num){
            case 0:
                {
                    result = TileType.Lumberjack;
                    break;
                }
            case 1:
                {
                    result = TileType.Quarry;
                    break;
                }
            case 2:
                {
                    result = TileType.Tavern;
                    break;
                }
            case 3:
                {
                    result = TileType.Stable;
                    break;
                }
            case 4: 
                {
                    result = TileType.Accountant;
                    break;
                }
            case 5:
                {
                    result = TileType.Church;
                    break;
                }
            default:
                {
                    Debug.Log("error");
                    result = TileType.Grassland;
                    break;
                }
        }

        return result;

    }
}
