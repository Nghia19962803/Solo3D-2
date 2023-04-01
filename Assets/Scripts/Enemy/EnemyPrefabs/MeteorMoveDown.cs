using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorMoveDown : MonoBehaviour
{
    [SerializeField] private float speed = 30;

    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

    }
}
