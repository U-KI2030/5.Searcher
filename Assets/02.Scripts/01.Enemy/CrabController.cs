using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabController : MonoBehaviour
{
    public Rigidbody2D theRB;

    // GameController
    private GameController gameController;

    /* 体力 */
    public float hp;

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

        WaitingTime = 0;

        SetbMove(false);

    }

    // 移動させる
    private void Move()
    {
        // まだtargetNowの地点にいない時
        if (transform.position != targetNow.position) {

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
            if(WaitingTime >= WaitTime)
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

    // HPを増減させる
    public void AddHP(float h) { hp += h; }

    public void SetbMove(bool M) { bMove = M; }
}
