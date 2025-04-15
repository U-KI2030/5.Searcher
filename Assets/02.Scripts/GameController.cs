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

    // ������
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

    // �Q�[���̏�Ԃ�Ԃ�
    // true : Player�𑀍�ł���
    // false : Player�𑀍�ł��Ȃ�
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
