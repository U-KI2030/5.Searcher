using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    // このアイテムを取得したら得ることができるスキル
    public int SkillNo;

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
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool result = false;
        if (other.gameObject.tag == "Player")
        {
            // Playerのスキルをアンロックする
            result = thePlayer.GetSkill(SkillNo);

            if (result)
            {
                if(gameController != null)
                {
                    // ゲームの時間を止める
                    gameController.GameStop();
                    // スキルアップUIを表示させている状態にする
                    gameController.SetbDisplayUI(true);
                }

                // SkillUpUIを表示させる
                if (thePlayer != null)
                {
                    thePlayer.DisplaySkillGetUI(true);
                }

                Destroy(this.gameObject);
            }
        }
    }
}
