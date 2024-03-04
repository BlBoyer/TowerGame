using UnityEngine;
using UnityEngine.UI;

public class GameMenuController : MonoBehaviour
{
    public GameObject canvas;
    public Button invBtn;
    public Button saveBtn;
    private GameManager manager;
    private GameObject invMenu;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        canvas.SetActive(false);
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        invMenu = GameObject.FindGameObjectWithTag("InvMenu").GetComponentInChildren<Canvas>().gameObject;
        invMenu.SetActive(false);
        //add interactions, the inventory needs to have a special click handler for the consumable items that have onInteract methods to them,
        //so the onclick will need to find those methods from a static method list and run them based on item name

        invBtn.onClick.AddListener(() => invMenu.SetActive(!invMenu.activeSelf));
        saveBtn.onClick.AddListener(() => manager.SavePlayerData());

    }
    void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            //get player position when opening menu, it's not performance intense
            Vector3 pos = GameObject.FindGameObjectWithTag("Player").transform.position;
            manager.ReplaceData("Position", new float[3] { pos.x, pos.y, pos.z });
            canvas.SetActive(!canvas.activeSelf);
        }
    }
}
