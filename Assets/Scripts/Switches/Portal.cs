using UnityEngine;

public class Portal : MonoBehaviour
{
    private ExitManager manager;
    private void Start()
    {
        manager = GameObject.FindWithTag("ExitController").GetComponent<ExitManager>();

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            manager.SetScene("Procedural_Earth");
            manager.ChangeScene("VerticalSlice");
        }
    }
}
