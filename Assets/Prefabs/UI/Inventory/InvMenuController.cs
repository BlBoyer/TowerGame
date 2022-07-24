using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvMenuController : MonoBehaviour
{
    //we need to do all the grouping of items and set them to respective ui components here, this would be a great place for react to come in, if we could
    private InventoryManager manager;
    public Button exitBtn;
    public TMPro.TMP_Text nameCol;
    public TMPro.TMP_Text descCol;
    public TMPro.TMP_Text amtCol;
    private List<string> names = new();
    private List<string> descriptions = new();
    private List<int> amounts = new();
    //we should change the descriptions field to be a hover element that displays on hovering over the item, so we can view longer descriptions and not gum up the ui
    //needs weapon pane
    //needs item pane
    //has gold pane
    //has key pane
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        //canvas.SetActive(false);
        manager = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryManager>();
        //set this menu inactive
        exitBtn.onClick.AddListener(() => gameObject.GetComponentInChildren<Canvas>().gameObject.SetActive(false));
        setItems();
        //Type.GetType(name) use a switch and map the cases
        /*public static Orientation ToOrientation(Direction direction) => direction switch
        {
            Direction.Up => Orientation.North,
            Direction.Right => Orientation.East,
            Direction.Down => Orientation.South,
            Direction.Left => Orientation.West,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), $"Not expected direction value: {direction}"),
        };*/
        foreach (string name in names)
        {
            nameCol.text += name + '\n';
        }
        foreach (string descr in descriptions)
        {
            /*Let's prefab a card object and bootstrap the menu*/
            //store in element hover child, or like an item card when selecting the item, which would also allow us to use or equip the item in its options
            //descCol.text += descr + '\n';
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
                descCol.text += $"{ descr.Substring(0, 4)}...'\n'";
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
