using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemyController : MonoBehaviour
{
    private PlayerController thePlayer;

    // GameController
    private GameController gameController;

    /* 移動 */
    public Transform target1;
    public Transform target2;

    // 現在の移動先
    private Transform targetNow;

    // 移動速度
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // 初期化処理
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController != null && gameController.GetCondition())
        {
            // 移動・向き
            Move();
        }
    }

    private void Initialize()
    {
        gameController = FindObjectOfType<GameController>();

        targetNow = target1;

        target1.transform.parent = null;
        target2.transform.parent = null;
    }

    // 移動させる
    private void Move()
    {
        // まだtargetNowの地点にいない時
        if (transform.position != targetNow.position)
        {
            // targetNowの位置へ移動する
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetNow.position,
                moveSpeed * Time.deltaTime);
        }
        else
        {
            if (targetNow == target1)
            {
                targetNow = target2;
            }
            else
            {
                targetNow = target1;
            }
        }

        // 向きを変える
        // 左方向
        if (targetNow == target1)
        {
            transform.localScale = Vector3.one;
        }
        // 右方向
        if (targetNow == target2)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 当たったものがPlayerの時
        if (other.gameObject.tag == "Player")
        {
            thePlayer = FindObjectOfType<PlayerController>();

            if (thePlayer != null && !thePlayer.GetbDamage())
            {
                // Playerにダメージ処理を行う
                thePlayer.AddHP(-10);
                // PlayerがDamageを受けた状態にする
                thePlayer.SetbDamage(true);
            }
        }
    }

}
