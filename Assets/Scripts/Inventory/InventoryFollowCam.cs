using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryFollowCam : MonoBehaviour
{
    public Camera cam;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // hold inventory alway appear in center screen
        transform.position = cam.transform.position;
    }
}
