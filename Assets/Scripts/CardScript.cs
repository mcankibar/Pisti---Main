using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class CardScript : MonoBehaviour
{
    public EnemyScript EnemyScript;
    public GameManager GameManager;
    public GroundScript GroundScript;
    public Player1Script Player1Script;
    public int id = 0;
    
    public int value = 0;

    public int cardStatusId = 0;
    
    public int GetValueOfCard()
    {
        return value;
    }
    
    public void SetValue(int newValue)
    {
        value = newValue;
        
    }

    public string GetSpriteName()
    {
        return GetComponent<SpriteRenderer>().sprite.name;
        
    }

    public void SetSprite(Sprite newSprite)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
    }

    public void ResetCard()
    {
        Sprite back = GameObject.Find("DeckController").GetComponent<DeckScript>().GetCardBack();
        gameObject.GetComponent<SpriteRenderer>().sprite = back;
        value = 0;
    }




    private bool isClickable = true;
    public void OnMouseDown()
    {
        if (!isClickable)
            return;
        
        if (cardStatusId == (int)CardStatuses.OnPlayer&& GameManager.playCount%2==0)
        {
            GameObject card = Instantiate(Player1Script.hand.FirstOrDefault(x => x.GetComponent<CardScript>().id == id));
            Player1Script.hand[card.GetComponent<CardScript>().id].GetComponent<BoxCollider2D>().enabled=false;
            
           
            if (card.GetComponent<CardScript>().value!=0)
            {
                GroundScript.getCard(card, this);
                card.GetComponent<CardScript>().cardStatusId = (int)CardStatuses.OnGround;
            }

            GameManager.playCount += 1;
        }
        if(GameManager.playCount%2!=0)
        {

            StartCoroutine(Bekleme());
        }
        IEnumerator Bekleme()
        {
            isClickable = false;
            yield return new WaitForSeconds(1);
            isClickable = true;
            
            System.Random random = new System.Random();

            int num = random.Next(0,EnemyScript.myValues.Count);
            Debug.Log("NUM"+num);
        
            
            for (int i = 0; i < EnemyScript.enemyhand.Count; i++)
            {
                
                if(!EnemyScript.enemyhand[i].activeInHierarchy)
                    continue;
                
                if (EnemyScript.enemyhand[i].GetComponent<CardScript>().value ==
                    GroundScript.groundhand[GroundScript.groundhand.Count-1].GetComponent<CardScript>().value
                    ||
                    EnemyScript.enemyhand[i].GetComponent<CardScript>().value == 11 && GroundScript.groundhand[GroundScript.groundhand.Count-2].GetComponent<CardScript>().value!=0)
                {
                    if (i < EnemyScript.myValues.Count)
                    {
                        num = i;
                        
                    }
                }
            }
            Debug.Log("NUMSON"+num);
            
            GameObject card2 = Instantiate(EnemyScript.enemyhand[EnemyScript.myValues[num]]);
            if (card2 is not null)
            {
         
                GroundScript.getCardFromEnemy(card2, card2.GetComponent<CardScript>());
                card2.GetComponent<CardScript>().cardStatusId = (int)CardStatuses.OnGround;
            }

            EnemyScript.myValues.RemoveAt(num);
            
            
            GameManager.playCount += 1;
        
        
        }


    }

    

    // Start is called before the first frame update
    void Start()
    {
        cardStatusId = (int)CardStatuses.OnDeck;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum CardStatuses
{
    //Kart destede
    OnDeck = 10,
    
    //Oyuncunun Elinde
    OnPlayer = 20,
    
    //Rakipte Olabilir
    OnEnemy = 30,
    
    //Yerde
    OnGround = 40
}