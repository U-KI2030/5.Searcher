using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosController : MonoBehaviour
{
    private PlayerController thePlayer;

    private Transform StartPos;
    public Transform[] BackPos;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("Player");
        thePlayer = obj.GetComponent<PlayerController>();

        StartPos = GameObject.Find("StartPos").transform;

        // セーブデータを保持しているかどうか確認
        // Playerが別のシーンから移動してきたかどうか
        if (ES3.KeyExists("Stage"))
        {
            // 保持している前シーンによって初期位置を変更する
            ChangePos();
        }
        else
        {
            if (thePlayer != null && StartPos != null)
            {
                thePlayer.transform.position = StartPos.transform.position;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangePos()
    {
        switch (ES3.Load<string>("Stage"))
        {
            case "1-1to1-2": // 1-1→1-2(0)
                ChangeBackPos(0);
                break;
            case "1-2to1-1": // 1-2→1-1(0)
                ChangeBackPos(0);
                break;
            case "1-2to1-3": // 1-2→1-3(0)
                ChangeBackPos(0);
                break;
            case "1-3to1-2": // 1-3→1-2(1)
                ChangeBackPos(1);
                break;
            case "1-3to1-4": // 1-3→1-4(0)
                ChangeBackPos(0);
                break;
            case "1-4to1-3": // 1-4→1-3(1)
                ChangeBackPos(1);
                break;
            case "1-4to2-1": // 1-4→2-1(0)
                ChangeBackPos(0);
                break;
        }
    }

    private void ChangeBackPos(int i)
    {
        if (thePlayer != null && BackPos != null)
        {
            thePlayer.transform.position = BackPos[i].transform.position;
        }
    }
}
