using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private PlayerController thePlayer;

    private Transform StartPos;
    private Transform BackPos;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("Player");
        thePlayer = obj.GetComponent<PlayerController>();

        //StartPos = GameObject.Find("StartPos").transform;
        //BackPos = GameObject.Find("BackPos").transform;

        // Playerが別のシーンから移動してきたかどうか
        //if (ES3.KeyExists("Stage"))
        //{
        //    if (thePlayer != null && BackPos != null)
        //    {
        //        thePlayer.transform.position = BackPos.transform.position;
        //    }
        //}
        //else
        //{
        //    if (thePlayer != null && StartPos != null)
        //    {
        //        thePlayer.transform.position = StartPos.transform.position;
        //    }
        //}

        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug
        // Qキーを押すとデータを削除
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // SaveDataを削除する
            ES3.DeleteFile("SaveFile.es3");
        }
    }

    // 初期化
    private void Initialize()
    {
        
    }

    public void GameStart()
    {
        Time.timeScale = 1.0f;
    }

    public void GameStop()
    {
        Time.timeScale = 0.0f;
    }
}
