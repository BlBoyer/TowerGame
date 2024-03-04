using UnityEngine;

public class randomEncounter : MonoBehaviour
{

    void Update()
    {
        void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log("Hit!");
        }
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "RandomEncounter")
            {
                Debug.Log("Trigger Random Encounter");
            }
        }
    }
}
