using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public GameObject masterKey;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            //process getting key, notification and logic
            masterKey.GetComponent<KeyBuilder>().addKey(gameObject);
            //GetComponent<Collider2D>().enabled = false;
            gameObject.SetActive(false);
        }
    }
}
