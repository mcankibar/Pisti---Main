using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Script : MonoBehaviour
{

    public CardScript CardScript;

    public DeckScript DeckScript;

    public List<GameObject> hand;

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
        int cardValue = DeckScript.PlayCard(hand[cardIndex].GetComponent<CardScript>());
        hand[cardIndex].GetComponent<Renderer>().enabled = true;
        hand[cardIndex].GetComponent<CardScript>().cardStatusId = (int)CardStatuses.OnPlayer;
        hand[cardIndex].GetComponent<CardScript>().id = cardIndex;
        hand[cardIndex].GetComponent<BoxCollider2D>().enabled = true;
        if (cardValue == 1)
        {
            aceList.Add(hand[cardIndex].GetComponent<CardScript>());
        }
        cardIndex++;
        
    }

    

    


    // Update is called once per frame
    void Update()
    {
        
    }
}
