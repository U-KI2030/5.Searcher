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
            // SkillUpUI���\������Ă����Ԃ̎�
            if (condition.GetbSkill())
            {
                // Space�L�[������������
                if (Input.GetButtonDown("Jump"))
                {
                    // ���Ԃ����ɖ߂�
                    gameController.GameStart();
                    // �Q�[���̏�Ԃ�UI��\����Ԃɂ���
                    condition.SetbSkill(false);
                    // UI���\���ɂ���
                    thePlayer.DisplaySkillGetUI(false);
                }
            }
        }
    }
}
