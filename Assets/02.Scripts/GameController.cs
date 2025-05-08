using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private bool bDisplayUI;

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

        // Player���ʂ̃V�[������ړ����Ă������ǂ���
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
        // Q�L�[�������ƃf�[�^���폜
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // SaveData���폜����
            ES3.DeleteFile("SaveFile.es3");
        }
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
