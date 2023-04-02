using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    //public Vector3 startPointCamera = new Vector3(-27, 8, -9.4f);
    public Vector3 endPointCamera = new Vector3(-9.4f, 8, -9.4f);
    public Camera cam;
    public float smoothing = 1;
    public GameObject gateObject;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Debug.Log(" Play !!!");

           StartCoroutine(CloseTheDoor());
        }
    }
    IEnumerator CloseTheDoor()
    {
        transform.GetComponent<Collider>().enabled = false;
        gateObject.SetActive(true);
        while (Vector3.Distance(cam.transform.position, endPointCamera) > 0.2f)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, endPointCamera, smoothing * Time.deltaTime);
            yield return null;
        }
        //yield return new WaitForSeconds(1);
        //transform.GetComponent<MeshRenderer>().enabled = true;

        GameManager.instance.startGame = true;
        //EnemyBossAttack.Instance.CallOnlyStartGame();
        EnemyController.Instance._enemyBossAttack.CallOnlyStartGame();
    }
}
