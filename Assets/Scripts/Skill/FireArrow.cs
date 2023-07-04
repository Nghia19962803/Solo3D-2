using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArrow : MonoBehaviour
{
    public List<GameObject> arrowList;
    private float speed = 15;
    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            arrowList.ForEach(t => t.SetActive(true));
            speed = 0;
            Destroy(gameObject, 0.5f);
        }
    }
}
