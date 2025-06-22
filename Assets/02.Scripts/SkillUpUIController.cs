using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUpUIController : MonoBehaviour
{
    private PlayerController thePlayer;

    // GameController
    private GameController gameController;

    // condition
    private Condition condition;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();

        gameController = FindObjectOfType<GameController>();

        condition = FindObjectOfType<Condition>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameController != null && thePlayer != null && condition != null)
        {
            // SkillUpUIが表示されている状態の時
            if (condition.GetbSkill())
            {
                // Spaceキーを押下した時
                if (Input.GetButtonDown("Jump"))
                {
                    // 時間を元に戻す
                    gameController.GameStart();
                    // ゲームの状態をUI非表示状態にする
                    condition.SetbSkill(false);
                    // UIを非表示にする
                    thePlayer.DisplaySkillGetUI(false);
                }
            }
        }
    }
}
