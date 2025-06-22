using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabController : MonoBehaviour
{
    private PlayerController thePlayer;

    // GameController
    private GameController gameController;

    // ゲーム中のコンディション
    private Condition condition;

    /* 移動 */
    public Transform target1;
    public Transform target2;

    // 現在の移動先
    private Transform targetNow;

    // 移動速度
    public float moveSpeed;

    public float WaitTime;
    private float WaitingTime;

    private bool bMove;

    // アニメ関係変数
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        // 初期化処理
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController != null && condition != null && condition.CheckCondition())
        {
            // 移動・向き
            Move();

            // Animation関係
            AnimControl();
        }
    }

    private void Initialize()
    {
        gameController = FindObjectOfType<GameController>();

        condition = FindObjectOfType<Condition>();

        targetNow = target1;

        target1.transform.parent = null;
        target2.transform.parent = null;

        WaitingTime = 0;

        SetbMove(false);

    }

    // 移動させる
    private void Move()
    {
        // まだtargetNowの地点にいない時
        if (transform.position != targetNow.position)
        {
            WaitingTime = 0;
            SetbMove(true);

            // targetNowの位置へ移動する
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetNow.position,
                moveSpeed * Time.deltaTime);
        }
        else
        {
            SetbMove(false);

            WaitingTime += Time.deltaTime;
            if (WaitingTime >= WaitTime)
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
        }

        // 向きを変える
        // 左方向
        if (targetNow == target1)
        {
            transform.localScale = new Vector3(7.5f, 7.5f, 1f);
        }
        // 右方向
        if (targetNow == target2)
        {
            transform.localScale = new Vector3(-7.5f, 7.5f, 1f);
        }

    }

    // Anim関連
    private void AnimControl()
    {
        // 動いているかどうか
        anim.SetBool("bMove", bMove);
    }

    public void SetbMove(bool M) { bMove = M; }

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
