using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : MonoBehaviour
{
    private static BossSkill _instance;
    public static BossSkill Instance { get { return _instance; } }

    public List<Transform> bullets;
    [SerializeField]private GameObject enemyBullet;

    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        LoadBullets();
    }
    public void LoadBullets()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject goj = Instantiate(enemyBullet);
            Transform parent = transform.Find("EnemyBullets");
            goj.transform.SetParent(parent);
            bullets.Add(goj.transform);
        }
    }
    //set damage for bullet before perform skill
    public void SetDmgForObject(int dmg)
    {
        foreach (Transform t in bullets)
        {
            t.GetComponent<DamageSender>().SendDamage(dmg);
        }
    }
    #region Skill "Lung Tung Quyền"
    // quyền ảnh tung hoành tạo ra 5 quyền phong bay 5 hướng
    public void GreaterMultibleProjectiles(Transform _transform)
    {
        for (int i = 0; i < 5; i++)
        {
            bullets[i].gameObject.SetActive(true);
            float angleOffset = Mathf.Atan2(_transform.right.z, _transform.right.x) * Mathf.Rad2Deg;

            //set position for each bullet
            bullets[i].position = new Vector3(_transform.position.x, _transform.position.y + 1, _transform.position.z);

            //set rotation for each bullet
            bullets[i].rotation = Quaternion.Euler(0f, (45 - (22.5f * i)) - angleOffset, 0f);
            bullets.RemoveAt(i);
        }
    }
    #endregion
    //============================================================================================

    //============================================================================================
    #region skill "Địa bộc thiên tinh"
    // gọi thiên thạch từ trên trời xuống
    public void Meteor(Transform _transform)
    {
        StartCoroutine(WaitForIt(_transform));
    }

    IEnumerator WaitForIt(Transform _transform)
    {
        // after 1s, a meteos will fall down to player's position
        // number of meteos depent how many meteos in list<bullets>
        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].gameObject.SetActive(true);

            //set meteos position on player head
            bullets[i].position = new Vector3(_transform.position.x, 10, _transform.position.z);  

            //set direction for each meteos
            bullets[i].LookAt(_transform.position);
            yield return new WaitForSeconds(1f);
        }
    }
    #endregion
    //#region Skill "Uế Thổ Chuyển Sinh"
    //// gọi các con đệ đã chết hồi sinh
    //public void EdoTensei()
    //{
    //    foreach (Transform golem in Golems)
    //    {
    //        if (golem.gameObject.activeSelf)
    //            return;
    //    }
    //    foreach (Transform golem in Golems)
    //    {
    //        golem.gameObject.SetActive(true);
    //    }
    //}
    //#endregion
}