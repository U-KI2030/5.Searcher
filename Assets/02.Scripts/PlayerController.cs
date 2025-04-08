using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D theRB;

/* ----------------------------------------------------*/
    // 移動・ジャンプ、操作関係変数
    public float moveSpeed;
    public float jumpForce;

    public Transform groundPoint;
    // Playerが移動しているかどうか
    private bool bMove;
    // 地面に付いているかどうか
    private bool bOnGround;
    public LayerMask whatIsGround;

    // 上キーを入力されているかどうか
    private bool bUp;

    // Playerの向いている向きを格納
    // -1 : 左
    // 0 : 上
    // 1 : 右
    private int dirPlayer;
    // 一時的に向きを

    /* ----------------------------------------------------*/
    // Shot関係変数
    // ShotPrefab
    public GameObject Pre_Shot;

    // Shotを行ったかどうか
    private bool bShot;
    // Shot待ち状態であるかどうか
    private bool bWaitingShot;
    // Shot状態を保持する時間
    public float shotWaitTime;
    // shot状態を保持している時間
    private float shotWaitingTime;

    // 弾が出る位置(Normal)
    public Transform ShotPoint;
    // 弾が出る位置(Up)
    public Transform ShotPoint_Up;

/* ----------------------------------------------------*/
    // アニメ関係変数
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        // 初期化
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if(shotWaitingTime >= shotWaitTime)
        {
            SetbWaitingShot(false);
        }
        else
        {
            shotWaitingTime += Time.deltaTime;
        }

        // 上キーを入力しているかどうか確認
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            SetbUp(true);
            dirPlayer = 0;
        }
        else
        {
            SetbUp(false);

            // 左を向いている
            if(transform.localScale.x < 0f)
            {
                dirPlayer = -1;
            }
            if(transform.localScale.x > 0f)
            {
                dirPlayer = 1;
            }
        }

        // 移動
        Moving();

        // ジャンプ
        Jumping();

        // Shot
        Shot();








        // アニメーション関連
        AnimControl();
    }

    // 初期化処理
    private void Initialize()
    {
        // 上を向いていない
        SetbUp(false);
        // 動いてない
        SetbMove(false);
        // Playerの向いている向きは右(1)
        dirPlayer = 1;

        // Shot状態ではない
        SetbShot(false);
        SetbWaitingShot(false);
        shotWaitingTime = 0f;
    }

    // Moving
    private void Moving()
    {
        // 横移動の操作を取得し、Playerの動きに格納する
        theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, theRB.velocity.y);

        // 左方向にキー入力されていたら、左を向く
        if (theRB.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);

            SetbMove(true);
            SetbShot(false);
            SetbWaitingShot(false);
        }
        else if (theRB.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
            SetbMove(true);
            SetbWaitingShot(false);
        }
        else
        {
            SetbMove(false);
        }
    }

    //Jumping
    private void Jumping()
    {
        // Playerが地面に足がついているか
        bOnGround = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround);

        // Jumpキー入力がされ、Playerが地面についている時
        if (Input.GetButtonDown("Jump") && bOnGround)
        {
            SetbWaitingShot(false);
            SetbShot(false);
            // Jumpさせる
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
        }
    }

    // Anim関連
    private void AnimControl()
    {
        // Playerが地面に足がついているか
        anim.SetBool("bOnGround", bOnGround);
        // Playerの速度を格納
        anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
        // Playerが移動しているかどうか
        anim.SetBool("bMove", bMove);
        // ShotAnim
        anim.SetBool("bShot", bShot);
        // ShotWaiting
        anim.SetBool("bWaitingShot", bWaitingShot);
    }

    // Shot
    private void Shot()
    {
        // Shotボタンを押すと
        if (Input.GetMouseButtonDown(0))
        {
            float x = 0, y = 0;
            Quaternion rot = Quaternion.identity;
            switch (dirPlayer)
            {
                // 右
                case 1:
                    x = 1;
                    y = 0;
                    break;
                // 左
                case -1:
                    x = -1;
                    y = 0;

                    rot = Quaternion.AngleAxis(180.0f, new Vector3(0.0f, 0.0f, 1.0f));
                    break;
                // 上
                case 0:
                    x = 0;
                    y = 1;

                    rot = Quaternion.AngleAxis(90.0f, new Vector3(0.0f, 0.0f, 1.0f));
                    break;
            }

            // 弾の方向を設定する
            Pre_Shot.GetComponent<ShotController>().SetMoveDir(x, y);

            // 弾を生み出す
            Instantiate(Pre_Shot, ShotPoint.position, rot);

            SetbShot(true);
            SetbWaitingShot(false);
            shotWaitingTime = 0;

        }

    }

    // ShotAnimが終わると呼ばれる
    public void ShotFinish()
    {
        SetbShot(false);
        SetbWaitingShot(true);
    }

    //bUp
    public void SetbUp(bool A_bUp) { bUp = A_bUp; }

    // bMove
    public void SetbMove(bool A_bMove) { bMove = A_bMove; }

    // bShotSet
    public void SetbShot(bool Shot) { bShot = Shot; }

    // bWaitingShotSet
    public void SetbWaitingShot(bool WaitingShot) { bWaitingShot = WaitingShot; }
}
