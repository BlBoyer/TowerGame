using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public GameObject interactionBox;
    [System.NonSerialized] public bool dirty;
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //make a new coroutine that displays the message
            interactionBox.SetActive(true);
        }
    }
    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //make a new coroutine that displays the message
            interactionBox.SetActive(false);
        }

    }
    protected void Start()
    {
        interactionBox.SetActive(false);
    }
    protected void Update()
    {
        ProcessInputs();
        //since we set the prop active onTrigger, we use that boolean
        if (dirty && interactionBox.activeSelf == true)
        {
            onInteract();
        }
    }
    protected void ProcessInputs()
    {
        dirty = Input.GetButtonDown("Interact");
    }
    /*
     * our override method
     * -- make inactive and add to inventory (Inventory item)
     * -- build to master key (Key item)
     * -- run specific animation (animated interactable)
     * -- etc
     */
    protected abstract void onInteract();
}
