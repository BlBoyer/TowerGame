using UnityEngine;

public class TempMsg : MonoBehaviour
{
    public GameObject messageBox;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            messageBox.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //destroy self after 2 sec
            Destroy(gameObject, 1f);
        }
        
    }
    private void Start()
    {
        messageBox.SetActive(false);
    }
}
