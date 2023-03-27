using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] private float speed = 15;

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

    }
}
