using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    // ���̃A�C�e�����擾�����瓾�邱�Ƃ��ł���X�L��
    public int SkillNo;

    private PlayerController thePlayer;

    // GameController
    private GameController gameController;

    // �Q�[�����̃R���f�B�V����
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
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool result = false;
        if (other.gameObject.tag == "Player")
        {
            // Player�̃X�L�����A�����b�N����
            result = thePlayer.GetSkill(SkillNo);

            if (result)
            {
                if(gameController != null && condition != null)
                {
                    // �Q�[���̎��Ԃ��~�߂�
                    gameController.GameStop();
                    // �X�L���A�b�vUI��\�������Ă����Ԃɂ���
                    condition.SetbSkill(true);
                }

                // SkillUpUI��\��������
                if (thePlayer != null)
                {
                    thePlayer.DisplaySkillGetUI(true);
                }

                Destroy(this.gameObject);
            }
        }
    }
}
