using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public CardScript CardScript;

    public DeckScript DeckScript;

    public List<GameObject> enemyhand;
    public List<int> myValues = new List<int>() { 0,1, 2, 3 };
    public GroundScript GroundScript;
    
    public int cardIndex = 0;

    public List<CardScript> aceList = new List<CardScript>();
    // Start is called before the first frame update
    public void StartHand()
    {
        getCard();
        getCard();
        getCard();
        getCard();
        cardIndex = 0;
        
    }

    public void getCard()
    {
        int cardValue = DeckScript.PlayCard(enemyhand[cardIndex].GetComponent<CardScript>());
        enemyhand[cardIndex].GetComponent<Renderer>().enabled = true;
        enemyhand[cardIndex].GetComponent<CardScript>().cardStatusId = (int)CardStatuses.OnEnemy;
        enemyhand[cardIndex].GetComponent<CardScript>().id = cardIndex;
        enemyhand[cardIndex].SetActive(true);
        
        if (cardValue == 1)
        {
            aceList.Add(enemyhand[cardIndex].GetComponent<CardScript>());
        }
        cardIndex++;
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}