using UnityEngine;

/*
 * we will make a pickupable script that does this for any object that we can pick up and have the text whatever we want.
 * then we will make a notification variable on the pickup script, and call the keyBuilder add method on pickup here, maybe with an override method or this
 */

public class PickUpable : MonoBehaviour
{
    //public GameObject that renders the text for picking up object, we could getComponentInChildren or use a tag, but this is easier right now, bc I don't know if we'll have other textrenderers
    public GameObject textRenderer;
    [System.NonSerialized] public bool pickedUp;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //make a new coroutine that displays the message
            textRenderer.SetActive(true);

            //masterKey.GetComponent<KeyBuilder>().addKey(gameObject);
            //GetComponent<Collider2D>().enabled = false;
            //gameObject.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //make a new coroutine that displays the message
            textRenderer.SetActive(false);
        }

    }
    private void Start()
    {
        textRenderer.SetActive(false);
    }
    private void Update()
    {
        ProcessInputs();
        //since we set the prop active onTrigger, we use that boolean
        if (pickedUp && textRenderer.activeSelf == true) 
        {
            onPickUp();
        }
    }
    private void ProcessInputs() 
    {
        pickedUp = Input.GetButtonDown("Interact");
    }
    private void onPickUp() 
    {
        //stop rendering object
        this.gameObject.SetActive(false);
    }
}
