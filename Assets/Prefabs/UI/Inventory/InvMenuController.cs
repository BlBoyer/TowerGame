using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvMenuController : MonoBehaviour
{
    //We call this from a button in GameMenu that doesn't exist yet
    private InventoryManager manager;
    //public GameObject canvas;
    public TMPro.TMP_Text nameCol;
    public TMPro.TMP_Text descCol;
    public TMPro.TMP_Text amtCol;
    private List<string> names = new();
    private List<string> descriptions = new();
    private List<int> amounts = new();
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        //canvas.SetActive(false);
        manager = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryManager>();
        setItems();
        foreach (string name in names)
        {
            nameCol.text += name + '\n';
        }
        foreach (string descr in descriptions)
        {
            descCol.text += descr + '\n';
        }
        foreach (int amt in amounts)
        {
            amtCol.text += $"{amt} \n";
        }
    }
    private void Update()
    {
        if (manager.dirty) 
        {
            //reset inventory details. need to overide player item to string
            setItems();
            foreach (string name in names) 
            {
                nameCol.text += name+'\n';
            }
            foreach (string descr in descriptions)
            {
                descCol.text += descr+'\n';
            }
            foreach (int amt in amounts)
            {
                amtCol.text += $"{ amt} \n";
            }
            manager.dirty = false;
        }
    }
    private void setItems() 
    {
        //need to reset these, so our text fields only reflect what we have and not multiples
        names.Clear();
        descriptions.Clear();
        amounts.Clear();
        foreach (var item in manager.inventory) 
        {
            names.Add(item.Name);
            descriptions.Add(item.Description);
            amounts.Add(item.Amount);
        }
        nameCol.text = "";
        descCol.text = "";
        amtCol.text = "";
    }
}
