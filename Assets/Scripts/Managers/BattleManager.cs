using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private static BattleManager _instance;
    public static BattleManager instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject manager = new GameObject("BattleManager");
                manager.AddComponent<BattleManager>();
            }
            return _instance;
        }
    }
    private string nextScene;
    private IEnumerator battleState;
    private bool running;
    private bool atIdle;
    private bool atAttack;
    private Fighter player;
    private Fighter enemy;
    public GameObject playerPrefab; //we put a prefab here so we can instantiate it
    public GameObject enemyPrefab; //this will be a NonSerialized public variable later, or just make a private method
    public Transform playerStart;
    public Transform enemyStart;
    //flesh out the IEnumerator in start with initialized values, such as enemy, etc
    void Awake()
    {
        _instance = this;
    }
    void FixedUpdate()
    {
    //move characters
    //play animations
    //if at players at start positions, running=false, idle for a couple seconds, then set running true coroutine
    // Update is called once per frame
    }
    void Update()
    {
        //process inputs
        //calculate outcome
        //assign animations
    }
        //get enemyPrefab by enemy thac calls battle, we can change this variable from that script directly
        //then we will call this method
    private void SartBattle()
    {
        //get vals that we use in our coroutine
        //get characters
        GameObject loadedPlayer = Instantiate(playerPrefab, playerStart);
        GameObject loadedEnemy = Instantiate(enemyPrefab, enemyStart);
        player = loadedPlayer.GetComponent<Fighter>();
        enemy  = loadedEnemy.GetComponent<Fighter>();
        //instantiate battle
        battleState = new IEnumerator()
        {
            //use values from args
            //movePlayersTowardsOppositeSides()
            if (running)
            {
                //move towards opposite sides
                yield return null; //resume next frame, or wait for frames over time;
            }
            else if (atIdle)
            {
                //play idle animations for parties
                yield return WaitForSeconds(3);
            }
            else if (atAttack)
            {
                yield return WaitForSeconds(attackDur);
            }
        }
    }
    void ProcessInputs() 
    {
        //take variables from ui
        //list possible animations
    }
    void Calculate() 
    {
        //if (battleisover){_nextScene = "VerticalSlice", this.End()}
    }
    void End() 
    {
    //the reason we have a sep method for setting the scene is to be able to check the scene we're in, once we have set it, we can access it
        GameManager.instance.setScene(nextScene);
        GameManager.instance.changeScene();
    }
}