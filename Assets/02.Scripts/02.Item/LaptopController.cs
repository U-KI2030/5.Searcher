using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopController : MonoBehaviour
{
    public GameObject Message;

    private PlayerController thePlayer;

    private Condition condition;

 /* ----------------------------------------------------*/
    // GameController関係変数
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

        // GameControllerを取得
        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        // 立札の説明文UIが表示されている時
        if (condition.GetbExplanation())
        {
            // Spaceキーを押したとき
            if (Input.GetButtonDown("Jump"))
            {
                // ページ数があるとき

                // ページ数がないとき
                // ExplanationUIが非表示となっている状態にする
                condition.SetbExplanation(false);
                // ExplanationUIが非表示となる
                this.gameObject.SetActive(false);
                // Game時間を0にする
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
