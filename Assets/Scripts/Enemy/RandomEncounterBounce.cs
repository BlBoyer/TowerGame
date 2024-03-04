using UnityEngine;

public class RandomEncounterBounce : MonoBehaviour
{
    Rigidbody2D rb;

    Vector3 LastVelocity;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        LastVelocity = rb.velocity;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        // if (other.gameObject.CompareTag("Player"))
        // {
        //     Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        // }

        var speed = LastVelocity.magnitude;
        var direction = Vector3.Reflect(LastVelocity.normalized, other.contacts[0].normal);
        rb.velocity = direction * Mathf.Max(speed, 0f);

    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("encounterTrigger"))
        {
            Debug.Log("Random Encounter Triggered");

        }

    }
}
