using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperController : MonoBehaviour
{
    private PlayerController thePlayer;

    // GameController
    private GameController gameController;

    private bool bMove;

    // 移動速度
    public float moveSpeed;

    /* 移動 */
    public Transform target1;

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
        if (gameController != null && gameController.GetCondition())
        {
            if (bMove)
            {
                // 移動・向き
                Move();
            }

            // Animation関係
            AnimControl();
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
                thePlayer.AddHP(-5);
                // PlayerがDamageを受けた状態にする
                thePlayer.SetbDamage(true);
            }
        }
        bMove = false;
        // 破壊する
        Destroy(this.gameObject);
    }

    // 初期化
    private void Initialize()
    {
        bMove = false;

        gameController = FindObjectOfType<GameController>();

        target1.transform.parent = null;
    }

    // 移動
    private void Move()
    {
        // targetNowの位置へ移動する
        transform.position = Vector3.MoveTowards(
            transform.position,
            target1.position,
            moveSpeed * Time.deltaTime);
    }

    // Anim関連
    private void AnimControl()
    {
        if (anim != null)
        {
            // 動いているかどうか
            anim.SetBool("bMove", bMove);
        }
    }

    public void SetbMove(bool M) { bMove = M; }
}
