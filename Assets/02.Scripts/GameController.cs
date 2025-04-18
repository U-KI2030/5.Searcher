using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private bool bDisplayUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 初期化
    private void Initialize()
    {
        bDisplayUI = false;
    }

    public void GameStart()
    {
        Time.timeScale = 1.0f;
    }

    public void GameStop()
    {
        Time.timeScale = 0.0f;
    }

    // ゲームの状態を返す
    // true : Playerを操作できる
    // false : Playerを操作できない
    public bool GetCondition()
    {
        if (!bDisplayUI)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetbDisplayUI(bool D) { bDisplayUI = D; }

    public bool GetbDisplayUI() { return bDisplayUI; }
}
