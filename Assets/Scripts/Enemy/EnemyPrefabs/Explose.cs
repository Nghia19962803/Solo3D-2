using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explose : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DeactiveByTime());
    }
    // after 1.5s deactive this bullet
    IEnumerator DeactiveByTime()
    {
        yield return new WaitForSeconds(0.5f);
        //FXManager.Instance.BulletExp(transform);
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }

    //this trigger for meteos when collision with ground. just make particle effect
}
