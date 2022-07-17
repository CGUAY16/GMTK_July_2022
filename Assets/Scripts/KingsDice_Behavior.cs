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
    private AudioSource kingSource;

    private void Awake()
    {
        kingSource = GetComponent<AudioSource>();
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
            StartCoroutine(BehaviorLoop());
        }
    }

    public void PickRandomBuilding()
    {
        int randomNum = (int)Mathf.Floor(Random.Range(1f, 6f)) - 1;
        Dice1.sprite = kingsDiceSpriteList[randomNum];
    }
    
    public void PickRandomNum()
    {
        int random = (int)Mathf.Floor(Random.Range(1f, 6f));
        Dice2.sprite = kingsDiceSpriteList2[random];
    }

    public IEnumerator BehaviorLoop()
    {
        KingsDicePanel.SetActive(true);
        kingSource.Play();
        yield return new WaitForSeconds(1f);
        PickRandomBuilding();
        PickRandomNum();

        yield return null;
    }

}
