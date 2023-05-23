using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GroundScript : MonoBehaviour
{

    public int PlayerScore;
    public int EnemyScore;
    public CardScript CardScript;
    public Player1Script Player1Script;
    public DeckScript DeckScript;
    public EnemyScript EnemyScript;
    public List<GameObject> groundhand;
    int playerscore=0;
    public int enemyscore = 0;
    public int sortOrder = 1;
    public int cardIndex = 0;
    public GameManager GameManager;
    public List<CardScript> groundList = new List<CardScript>();
    // Start is called before the first frame update

    

    public void StartHand()
    {
        
        getCard();
        getCard();
        getCard();
        getCard();
        
        
    }

    public void getCard()
    {
        int cardValue = DeckScript.PlayCard(groundhand[cardIndex].GetComponent<CardScript>());
        groundhand[cardIndex].GetComponent<Renderer>().enabled = true;
        groundhand[cardIndex].GetComponent<CardScript>().cardStatusId = (int)CardStatuses.OnGround;
        groundhand[cardIndex].GetComponent<SpriteRenderer>().sortingOrder = sortOrder;
        
        
        if (cardValue == 1)
        {
            groundList.Add(groundhand[cardIndex].GetComponent<CardScript>());
        }
        
        Debug.LogError(groundhand[cardIndex].GetComponent<CardScript>().value);
        
        cardIndex++;
        sortOrder++;
    }
    
    public void getCard(GameObject card, CardScript cardScript)
    {
        groundhand.Add(card);
        groundList.Add(cardScript);
        card.transform.SetParent(transform);
        card.GetComponent<CardScript>().id = cardScript.id;
        card.GetComponent<CardScript>().value = cardScript.value;
        card.GetComponent<CardScript>().cardStatusId = (int)CardStatuses.OnGround;
        card.GetComponent<SpriteRenderer>().sortingOrder = sortOrder;
        card.GetComponent<Renderer>().enabled = true;
        card.GetComponent<Transform>().position = groundhand[0].transform.position;
        System.Random random = new System.Random(); 
        
        int num = random.Next(-15,15);
        int num2 = random.Next(-1,1);
        
        float shiftingCardValue = 0.0f * num2;
        card.transform.position += new Vector3(num2, 0, 0);
        card.transform.Rotate(0,0,num);
        sortOrder++;
        
        int uzunluk = groundhand.Count;
        if (uzunluk > 1)
        {
            
            
            bool isSame = groundhand[uzunluk - 1].GetComponent<CardScript>().value == groundhand[uzunluk - 2].GetComponent<CardScript>().value;
            bool Joker = card.GetComponent<CardScript>().value == 11;
            Debug.LogError(isSame);
            bool Pisti = groundhand[uzunluk - 3].GetComponent<CardScript>().value == 0;
            if (isSame && Pisti)
            {
                playerscore += 10;
            }
            if (isSame||Joker)
            {

                
                for (int k = 0; k < groundhand.Count; k ++)
                {
                    if( groundhand[k ].GetComponent<CardScript>().value==1||groundhand[k ].GetComponent<CardScript>().value==11)
                    {
                        playerscore += 1;
                    }
                    else if (groundhand[k ].GetComponent<SpriteRenderer>().sprite==DeckScript.Club02)
                    {
                        playerscore += 2;

                    }
                    else if (groundhand[k ].GetComponent<SpriteRenderer>().sprite==DeckScript.Diamond10)
                    {
                        playerscore += 3;

                    }
                    

                }

                PlayerScore = playerscore;
                GameObject.Find("PlayerScore").GetComponent<TextMeshProUGUI>().SetText(PlayerScore.ToString());
                Debug.Log("SCORE:"+playerscore.ToString());

                
                for (int k = 0; k < groundhand.Count; k ++)
                {
                    groundhand[k].GetComponent<SpriteRenderer>().sprite=null;
                    groundhand[k].GetComponent<CardScript>().value = 0;
                    groundhand[k].GetComponent<CardScript>().SetSprite(null);
                }

                
               
                
                

            }


            
        }
        Player1Script.hand[card.GetComponent<CardScript>().id].GetComponent<CardScript>().value=0;
        Player1Script.hand[card.GetComponent<CardScript>().id].GetComponent<CardScript>().GetComponent<SpriteRenderer>()
            .enabled = false;

        int toplam = 0;
        for (int i = 0; i < Player1Script.hand.Count; i++)
        {
            toplam += Player1Script.hand[i].GetComponent<CardScript>().value;
        }
        for (int l = 0; l < EnemyScript.enemyhand.Count; l++)
        {
            toplam += EnemyScript.enemyhand[l].GetComponent<CardScript>().value;
        }

        if (toplam == 0 && DeckScript.currentIndex==53)
        {
            
            
            if(playerscore>enemyscore)
            {
                string finish = "PLAYER WİN!";
                GameObject.Find("Finish").GetComponent<TextMeshProUGUI>().SetText(finish); 

            }
            else
            {
                string finish = "ENEMY WİN!";
                GameObject.Find("Finish").GetComponent<TextMeshProUGUI>().SetText(finish);
            }
            
            
        }
        else if ( toplam==0 && DeckScript.currentIndex!=53)
        {
            Player1Script.StartHand();
            EnemyScript.StartHand();
            
        }
        
       

    }
    public void getCardFromEnemy(GameObject card, CardScript cardScript)
    {
        groundhand.Add(card);
        
        groundList.Add(cardScript);
        card.transform.SetParent(transform);
        card.GetComponent<CardScript>().id = cardScript.id;
        card.GetComponent<CardScript>().value = cardScript.value;
        card.GetComponent<CardScript>().cardStatusId = (int)CardStatuses.OnGround;
        card.GetComponent<SpriteRenderer>().sortingOrder = sortOrder;
        card.GetComponent<Renderer>().enabled = true;
        card.GetComponent<Transform>().position = groundhand[0].transform.position;
        System.Random random = new System.Random(); 
        
        int num = random.Next(-15,15);
        int num2 = random.Next(-1,1);
        
        float shiftingCardValue = 0.0f * num2;
        card.transform.position += new Vector3(num2, 0, 0);
        card.transform.Rotate(0,0,num);
        
        
        sortOrder++;
        
        int uzunluk = groundhand.Count;
        if (uzunluk > 1)
        {

            
            bool isSame = groundhand[uzunluk - 1].GetComponent<CardScript>().value == groundhand[uzunluk - 2].GetComponent<CardScript>().value;
            
            bool Joker = card.GetComponent<CardScript>().value==11&&groundhand[groundhand.Count-1].GetComponent<CardScript>().value!=0;
            Debug.LogError(isSame);
            bool Pisti = groundhand[uzunluk - 3].GetComponent<CardScript>().value == 0;
            if (isSame && Pisti)
            {
                enemyscore += 10;
            }
            if (isSame||Joker)
            {

                
                for (int k = 0; k < groundhand.Count; k ++)
                {
                    if( groundhand[k ].GetComponent<CardScript>().value==1||groundhand[k ].GetComponent<CardScript>().value==11)
                    {
                        enemyscore += 1;
                    }
                    else if (groundhand[k ].GetComponent<SpriteRenderer>().sprite==DeckScript.Club02)
                    {
                        enemyscore += 2;

                    }
                    else if (groundhand[k ].GetComponent<SpriteRenderer>().sprite==DeckScript.Diamond10)
                    {
                        enemyscore += 3;

                    }
                    

                }
                
                Debug.Log("ENEMY KAZANDI:"+enemyscore);
                
                EnemyScore = enemyscore;
                GameObject.Find("Textim").GetComponent<TextMeshProUGUI>().SetText(EnemyScore.ToString());
                
                

                
                for (int k = 0; k < groundhand.Count; k ++)
                {
                    groundhand[k].GetComponent<Renderer>().enabled=false;
                    groundhand[k].GetComponent<CardScript>().value = 0;
                    groundhand[k].GetComponent<CardScript>().SetSprite(null);
                }

                
              
                
                

            }


            
        }
        EnemyScript.enemyhand[card.GetComponent<CardScript>().id].GetComponent<CardScript>().value=0;
        EnemyScript.enemyhand[card.GetComponent<CardScript>().id].GetComponent<CardScript>().GetComponent<SpriteRenderer>()
            .enabled = false;
        EnemyScript.enemyhand[card.GetComponent<CardScript>().id].SetActive(false);

        int toplam = 0;
        for (int i = 0; i < Player1Script.hand.Count; i++)
        {
            toplam += Player1Script.hand[i].GetComponent<CardScript>().value;
        }
        for (int l = 0; l < EnemyScript.enemyhand.Count; l++)
        {
            toplam += EnemyScript.enemyhand[l].GetComponent<CardScript>().value;
        }

        if (toplam == 0 && DeckScript.currentIndex==53)
        {
            
            
            if(playerscore>enemyscore)
            {
                string finish = "PLAYER WIN!";
                GameObject.Find("Finish").GetComponent<TextMeshProUGUI>().SetText(finish); 
                GameObject.Find("GroundDeck").SetActive(false);

            }
            
            else
            {
                string finish = "ENEMY WIN!";
                GameObject.Find("Finish").GetComponent<TextMeshProUGUI>().SetText(finish);
                GameObject.Find("GroundDeck").SetActive(false);
            }
            
            
        }
        else if ( toplam==0 && DeckScript.currentIndex!=53)
        {
            Player1Script.StartHand();
            EnemyScript.StartHand();
            EnemyScript.myValues.Add(0);
            EnemyScript.myValues.Add(1);
            EnemyScript.myValues.Add(2);
            EnemyScript.myValues.Add(3);
        }
       

    }
    
    
    // Update is called once per frame
    void Update()
    {
        
    }
    
}