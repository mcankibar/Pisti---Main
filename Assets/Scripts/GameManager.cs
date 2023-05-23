using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button playbtn;
    public Button restartbtn;
    public GroundScript GroundScript;
    public Player1Script _player1Script;
    public EnemyScript enemyScript;
    public int playCount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        playbtn.onClick.AddListener(() => PLAYClicked());
        restartbtn.onClick.AddListener(()=>RESTARTClicked());

    }

    private void PLAYClicked()
    {
        GameObject.Find("Deck").GetComponent<DeckScript>().Shuffle();
        GroundScript.StartHand();
        _player1Script.StartHand();
        enemyScript.StartHand();
        
        
    }
    private void RESTARTClicked()
    {
        ResetTheGame();
        
        
        
    }
    // Update is called once per frame
    void Update()
    {
        
           
        

    }

    public void ResetTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
