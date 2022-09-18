using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This class is for a persistent gameobject that allows battles to start and stop in game*/
public class BattleManager : MonoBehaviour
{
    public Transform playerStart;
    public Transform playerWaypoint;
    public Transform enemyStart;
    public Transform enemyWaypoint;
    //for now we'll leave this serialized for testing, it is a field bc we only need to have one instance at a time, if we need multiple enemies, we just make this an array.
    //public GameObject _enemy { get; set; }
    private GameObject enemy;
    private GameObject player;
    private Transform playerT;
    private Transform enemyT;
    private float playerSpeed = 1f;
    private float enemySpeed = 1f;
    private void Start()
    {
        //instantiate gameObjects in battle scene
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
        enemyT = enemy.transform;
        playerT = player.transform;
        enemyT.position = enemyStart.position;
        playerT.position =playerStart.position;
        StartCoroutine("playerActions");
    }
    private void FixedUpdate()
    {
        if (playerT.position.x < playerWaypoint.position.x)
        {
            playerT.position = playerT.position + new Vector3(playerSpeed, 0f, 0f) * Time.deltaTime;
        }
/*        if (playerT.position.x == playerStart.position.x) 
        {
            StartCoroutine("playerActions");
        }*/
        if (enemyT.position.x > enemyWaypoint.position.x)
        {
            StartCoroutine("moveEnemy");
        }
    }
    private IEnumerator playerActions() 
    {
        //we should probably put menu access/prompts here and obviously name it something other than movePlayer
        //each step of the battle should be here as part of the enumerator
        //we can change speed here as well
        yield return new WaitForSeconds(1.5f);
        Debug.Log("speed increased to player");
        playerSpeed += 1f;
        yield return new WaitForSeconds(1f);
        Debug.Log("speed decreased to player");
        playerSpeed -= 1.2f;
    }
    private IEnumerator moveEnemy()   
    {
        enemyT.position = enemyT.position - new Vector3(enemySpeed, 0f, 0f) * Time.deltaTime;
        yield return null;
    }
}