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
    // GameController関係変数
    private GameController gameController;


 /* ----------------------------------------------------*/
    // Textファイルを読み込む関係
    // 行、列
    int textNum = 0, count = 0;

    // 表示させるテキスト
    [SerializeField] TextMeshProUGUI Text;
    // 読み込むテキスト
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

        // GameControllerを取得
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
        // 立札の説明文UIが表示されている時
        if (condition.GetbExplanation() && thePlayer != null)
        {
            if (Message != null)
            {
                Message.SetActive(false);
            }

            string Times = TextData[textNum][count].ToString();

            // ファイルの中身がまだあるとき
            if(Times != "ENDTEXT")
            {
                // 行内がまだ終わりでないとき
                if (Times != "END")
                {
                    // Spaceキーを押したとき
                    if (Input.GetButtonDown("Jump"))
                    {
                        count++;
                    }

                    Text.text = Times;
                }
                else
                {
                    // Spaceキーを押したとき
                    if (Input.GetButtonDown("Jump"))
                    {
                        count = 0;
                        textNum++;
                    }
                }
            }
            // ファイルの中身がもうないとき
            else
            {
                // Spaceキーを押したとき
                if (Input.GetButtonDown("Jump"))
                {
                    // ExplanationUIが非表示となっている状態にする
                    condition.SetbExplanation(false);
                    // ExplanationUIが非表示となる
                    thePlayer.GetExplanation().SetActive(false);
                    thePlayer.SetbExplanation(false);

                    // Game時間を0にする
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
                // Explanationを表示させる
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
