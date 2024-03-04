using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform playerT;
    private Transform _thisT;
    private void Start()
    {
        _thisT = this.GetComponent<Transform>();
        //faster than find bc it only looks through objects with specific tag
        playerT = GameObject.FindWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        _thisT.position = new Vector3(playerT.position.x, playerT.position.y, _thisT.position.z);
        
    }
}
