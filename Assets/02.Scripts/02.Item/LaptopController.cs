using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;
using System.Text;
using Unity.VisualScripting;

public class LaptopController : MonoBehaviour
{
    public GameObject Message;

    private PlayerController thePlayer;

    private Condition condition;

 /* ----------------------------------------------------*/
    // GameController�֌W�ϐ�
    private GameController gameController;


 /* ----------------------------------------------------*/
    // Text�t�@�C����ǂݍ��ފ֌W
    // �s�A��
    int textNum = 0, count = 0;

    // �\��������e�L�X�g
    [SerializeField] TextMeshProUGUI Text;
    // �ǂݍ��ރe�L�X�g
    [SerializeField] TextAsset TextFile;

    List<string[]> TextData = new List<string[]>();

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

        StringReader reader = new StringReader(TextFile.text);

        while(reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            TextData.Add(line.Split(','));
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ���D�̐�����UI���\������Ă��鎞
        if (condition.GetbExplanation() && thePlayer != null)
        {
            if (Message != null)
            {
                Message.SetActive(false);
            }

            string Times = TextData[textNum][count].ToString();

            // �t�@�C���̒��g���܂�����Ƃ�
            if(Times != "ENDTEXT")
            {
                // �s�����܂��I���łȂ��Ƃ�
                if (Times != "END")
                {
                    // Space�L�[���������Ƃ�
                    if (Input.GetButtonDown("Jump"))
                    {
                        count++;
                    }

                    Text.text = Times;
                }
                else
                {
                    // Space�L�[���������Ƃ�
                    if (Input.GetButtonDown("Jump"))
                    {
                        count = 0;
                        textNum++;
                    }
                }
            }
            // �t�@�C���̒��g�������Ȃ��Ƃ�
            else
            {
                // Space�L�[���������Ƃ�
                if (Input.GetButtonDown("Jump"))
                {
                    // ExplanationUI����\���ƂȂ��Ă����Ԃɂ���
                    condition.SetbExplanation(false);
                    // ExplanationUI����\���ƂȂ�
                    thePlayer.GetExplanation().SetActive(false);
                    thePlayer.SetbExplanation(false);

                    // Game���Ԃ�0�ɂ���
                    gameController.GameStart();
                }
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
