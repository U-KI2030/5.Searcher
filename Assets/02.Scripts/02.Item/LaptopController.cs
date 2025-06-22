using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopController : MonoBehaviour
{
    public GameObject Message;

    private PlayerController thePlayer;

    private Condition condition;

 /* ----------------------------------------------------*/
    // GameController�֌W�ϐ�
    private GameController gameController;


 /* ----------------------------------------------------*/

    // Start is called before the first frame update
    void Start()
    {
        if(Message != null)
        {
            Message.SetActive(false);
        }

        thePlayer = GameObject.Find("Player").GetComponent<PlayerController>();

        condition = FindObjectOfType<Condition>();

        // GameController���擾
        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        // ���D�̐�����UI���\������Ă��鎞
        if (condition.GetbExplanation())
        {
            // Space�L�[���������Ƃ�
            if (Input.GetButtonDown("Jump"))
            {
                // �y�[�W��������Ƃ�

                // �y�[�W�����Ȃ��Ƃ�
                // ExplanationUI����\���ƂȂ��Ă����Ԃɂ���
                condition.SetbExplanation(false);
                // ExplanationUI����\���ƂȂ�
                this.gameObject.SetActive(false);
                // Game���Ԃ�0�ɂ���
                gameController.GameStart();

                thePlayer.SetbExplanation(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Message != null)
            {
                Message.SetActive(true);
            }

            if(thePlayer != null)
            {
                // Explanation��\��������
                thePlayer.SetbExplanation(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Message != null)
            {
                Message.SetActive(false);
            }
        }
    }
}
