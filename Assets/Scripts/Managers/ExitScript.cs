using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            GameManager.instance.setScene("level2");
            GameManager.instance.changeScene();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
