using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D theRB;

/* ----------------------------------------------------*/
    // 移動・ジャンプ関係変数
    public float moveSpeed;
    public float jumpForce;

    public Transform groundPoint;
    // 地面に付いているかどうか
    private bool bOnGround;
    public LayerMask whatIsGround;

/* ----------------------------------------------------*/
    // Shot関係変数
    // Shotを行ったかどうか
    private bool bShot;
    // Shot待ち状態であるかどうか
    private bool bWaitingShot;
    // Shot状態を保持する時間
    public float shotWaitTime;
    // shot状態を保持している時間
    private float shotWaitingTime;

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
            shotWaitingTime = shotWaitTime;
            SetbWaitingShot(false);
        }
        else
        {
            shotWaitingTime += Time.deltaTime;
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
        }
        else if (theRB.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
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

    // isShotSet
    public void SetbShot(bool Shot) { bShot = Shot; }

    // isWaitingShotSet
    public void SetbWaitingShot(bool WaitingShot) { bWaitingShot = WaitingShot; }
}
