using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private float timeCount = 0;

    public bool startGame { get;set; }
    public int gameRound = 0;
    public bool check;
    public bool openMap;

    public int currentPlayerHP;
    public GameObject weapon;
    public GameObject armor;
    private void Awake()
    {
        // sau khi reload lại main scene thì sẻ tạo thêm 1 game manager
        // điều kiện này để ngăn điều đó xảy ra
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
        startGame = true;
    }
    private void Start()
    {
        TriggerMap.instance.AppearTrigger();
    }

    private void FixedUpdate()
    {
        if (!startGame) return;

        if (TriggerMap.instance.CheckTriggerMap())
        {
            ReloadScene();
            StartCoroutine(EnableEnemyTrigg());
            check = false;
            openMap = false;
        }

        if (TriggerEnemy.instance.CheckTriggerEnemy() && !check)
        {
            StartCoroutine(SpawnEnemy());
            check = true;
        }

        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && check && openMap)
        {
            TriggerMap.instance.AppearTrigger();
        }
    }
    public bool StartGame()
    {
        return startGame;
    }
    public void StopGame()
    {
        startGame = false ;
        //Debug.Log("Stop game");
        StartCoroutine(BackToMenu());
        WinLoseManager.instance.ApearEndGamePanel("lose");
    }
    public void EndGame()
    {
        startGame = false;
        //Debug.Log("End game and WIN");
        StartCoroutine(BackToMenu());
        WinLoseManager.instance.ApearEndGamePanel("win");
    }
    public void SaveTime()
    {
        SaveData data = new SaveData();
        data._timeCount = timeCount;
        
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void ReloadScene()
    {
        CurrentHealthPlayer();
        gameRound++;
        SceneManager.LoadScene(1);
    }
    IEnumerator SpawnEnemy()
    {
        TriggerEnemy.instance.HideTrigger();
        yield return new WaitForSeconds(1);
        if (gameRound < 4)
        {
            for (int i = 0; i < (gameRound * 3 + 5); i++)
            {
                EnemySpawner.Instance.SpawnEnemy(0);
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < (gameRound * 3); i++)
            {
                EnemySpawner.Instance.SpawnEnemy(1);
            }
        }
        if (gameRound == 4)
        {
            EnemySpawner.Instance.SpawnBoss();

        }
        yield return new WaitForSeconds(1);
        openMap = true;
    }
    IEnumerator EnableEnemyTrigg()
    {
        yield return new WaitForSeconds(1);
        TriggerEnemy.instance.AppearTrigger();
        //set lai hp cho player
        PlayerControllerISO.Instance._stats.SetHP(currentPlayerHP);

        // spawn item when round 4
        if(gameRound == 4)
        {
            Instantiate(weapon,weapon.transform.position,Quaternion.identity);
            Instantiate(armor, armor.transform.position, Quaternion.identity);
        }
    }
    IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(0);
    }
    public void CurrentHealthPlayer()
    {
        currentPlayerHP = PlayerControllerISO.Instance._stats.GetCurrentHealh();
    }
}
[System.Serializable]
public class SaveData
{
    public float _timeCount;

}
