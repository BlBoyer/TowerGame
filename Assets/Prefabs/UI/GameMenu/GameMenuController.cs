using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuController : MonoBehaviour
{
    public GameObject canvas;
    public Button saveBtn;
    private GameManager manager;
    void Start()
    {
        canvas.SetActive(false);
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        //add interactions, the inventory needs to have a special click handler for the consumable items that have onInteract methods to them,
        //so the onlcick will need to find those methods from a static method list and run them based on item name
        saveBtn.onClick.AddListener(() => manager.SavePlayerData());

    }
    void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            canvas.SetActive(!canvas.activeSelf);
        }
    }
}
