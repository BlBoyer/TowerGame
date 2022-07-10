using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public GameObject masterKey;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {

            //we may change this from gameObject to gameObject.name
            masterKey.GetComponent<KeyBuilder>().addKey(gameObject);
            //GetComponent<Collider2D>().enabled = false;
            gameObject.SetActive(false);
        }
    }
}
