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
    private void Start()
    {
        //instantiate gameObjects in battle scene
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
        enemyT = enemy.transform;
        playerT = player.transform;
        enemyT.position = enemyStart.position;
        playerT.position =playerStart.position;
    }
    private void FixedUpdate()
    {
        if (playerT.position.x < playerWaypoint.position.x) 
        {
            StartCoroutine("movePlayer");
        }
        if (enemyT.position.x > enemyWaypoint.position.x)
        {
            StartCoroutine("moveEnemy");
        }
    }
    private IEnumerator movePlayer() 
    {
        playerT.position = playerT.position + new Vector3(.1f, 0f, 0f);
        yield return new WaitForSeconds(1);
    }
    private IEnumerator moveEnemy()   
    {
        enemyT.position = enemyT.position - new Vector3(.1f, 0f, 0f);
        yield return new WaitForSeconds(1);
    }
}