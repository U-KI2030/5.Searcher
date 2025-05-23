using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUpUIController : MonoBehaviour
{
    private PlayerController thePlayer;

    // GameController
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();

        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameController != null && thePlayer != null)
        {
            if (gameController.GetbDisplayUI())
            {
                if (Input.GetButtonDown("Jump"))
                {
                    // 時間を元に戻す
                    gameController.GameStart();
                    // ゲームの状態をUI非表示状態にする
                    gameController.SetbDisplayUI(false);
                    // UIを非表示にする
                    thePlayer.DisplaySkillGetUI(false);
                }
            }
        }
    }
}
