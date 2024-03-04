using UnityEngine;

public class RandomEncounterMovement : MonoBehaviour
{
    Rigidbody2D rb;
    private float speed = 400;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(20 * Time.deltaTime * speed, 20 * Time.deltaTime * speed));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
