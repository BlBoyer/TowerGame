using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerT;
    private Transform _thisT;
    private void Start()
    {
        _thisT = this.GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        _thisT.position = new Vector3(playerT.position.x, playerT.position.y, _thisT.position.z);
        
    }
}
