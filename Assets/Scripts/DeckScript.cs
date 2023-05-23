using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;
public class DeckScript : MonoBehaviour
{
    public Sprite[] cardSprites;

    public int[] cardValues = new int[53];

    public int currentIndex = 0;
    public Sprite Club02;

    public Sprite Diamond10;
    // Start is called before the first frame update
    void Start()
    {
        getCardValues();
        Club02 = cardSprites[2];
        Diamond10 = cardSprites[23];
    }

    // Update is called once per frame
    void getCardValues()
    {
        int num = 0;
        for (int i = 1; i < cardSprites.Length-1; i++)
        {
            num = i;
            num = num %13;
            if ( num == 0)
            {
                num = 13;
            }

            cardValues[i] = num++;
        }

        currentIndex = 1;
    }
    public void Shuffle()
    {
        for (int i = cardSprites.Length - 1; i > 0; i--)
        {
            int j = Mathf.FloorToInt(UnityEngine.Random.Range(0.0f, 0.5f) * (cardSprites.Length - 1)+1) ;
            Sprite face = cardSprites[i];
            cardSprites[i] = cardSprites[j];
            cardSprites[j] = face;

            int value = cardValues[i];
            cardValues[i] = cardValues[j];
            cardValues[j] = value;


        }
    }

    public int PlayCard(CardScript cardScript)
    {
        cardScript.SetSprite(cardSprites[currentIndex]);
        cardScript.SetValue(cardValues[currentIndex]);
        currentIndex++;
        return cardScript.GetValueOfCard();
        
    }

    public Sprite GetCardBack()
    {
        return cardSprites[0];
    }
}
