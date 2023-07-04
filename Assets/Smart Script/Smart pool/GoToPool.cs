using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToPool : MonoBehaviour
{
    public float delaytime = 2;
    public float count = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 10);
        count += Time.deltaTime;
        if(count > delaytime)
            gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        transform.position = transform.parent.position;
        count = 0;
    }
}
