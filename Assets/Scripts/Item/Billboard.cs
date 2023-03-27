using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//make sure item have nice view when play game
public class Billboard : MonoBehaviour
{
    private Camera _camera;
    private void Start()
    {
        _camera = Camera.main;
    }
    private void LateUpdate()
    {
        transform.forward = _camera.transform.forward;
    }
}
