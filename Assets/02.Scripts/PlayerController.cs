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

    // 何回目のジャンプか
    private int JumpNum;

    // Playerの向いている向きを格納
    // -1 : 左
    // 0 : 上
    // 1 : 右
    private int dirPlayer;

/* ----------------------------------------------------*/
    // Shot関係変数
    // ShotPrefab
    public GameObject Pre_Shot;
    // ChargeShot
    public GameObject Pre_ChargeShot;

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

    // ChargeShotの溜め時間
    public float ChargeTime;
    // ChargeShotの溜めている時間
    private float ChargingTime;

    // ChargeEffect
    public GameObject Pre_ChargeEffect;
    // ChargeEffect_UP
    public GameObject Pre_ChargeEffectUp;
    // ChargeEffect_Jump
    public GameObject Pre_ChargeEffectJump;

    // 1回だけ動作する
    private int Shot_one;

/* ----------------------------------------------------*/
    // アニメ関係変数
    public Animator anim;

/* ----------------------------------------------------*/
    // Skill関係
    // DoubleJump
    private bool bDoubleJump;
    // ChargeShot
    private bool bChargeShot;

    // SkillGetUI
    public GameObject SkillGetUI;

/* ----------------------------------------------------*/
    // GameController関係変数
    private GameController gameController;

/* ----------------------------------------------------*/

/* ----------------------------------------------------*/
    // HP関係変数
    private float hp;

/* ----------------------------------------------------*/

    // Start is called before the first frame update
    void Start()
    {
        // 初期化
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController != null)
        {
            if (gameController.GetCondition())
            {
                if (shotWaitingTime >= shotWaitTime)
                {
                    SetbWaitingShot(false);
                }
                else
                {
                    shotWaitingTime += Time.deltaTime;
                }



                // 移動・キー操作
                Moving();

                // ジャンプ
                Jumping();

                // Shot
                Shot();








                // アニメーション関連
                AnimControl();
            }
        }
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
        // Shot待ち状態ではない
        SetbWaitingShot(false);
        shotWaitingTime = 0f;

        // チャージタイムを初期化
        ChargingTime = 0f;

        // Effect関係を非表示
        Pre_ChargeEffect.SetActive(false);
        Pre_ChargeEffectUp.SetActive(false);
        Pre_ChargeEffectJump.SetActive(false);

        Shot_one = 0;

        // Jump回数を0回
        JumpNum = 0;

        // スキルをロック
        bDoubleJump = false;
        bChargeShot = false;

        // SkillGetUIを非表示
        SkillGetUI.SetActive(false);

        // GameControllerを取得
        gameController = FindObjectOfType<GameController>();

        // HPを初期値(100)に変更
        hp = 100;
    }

    // Moving
    private void Moving()
    {
        // 上キーを入力しているかどうか確認
        if (Input.GetAxisRaw("Vertical") > 0 && Mathf.Abs(theRB.velocity.x) == 0)
        {
            SetbUp(true);
            dirPlayer = 0;
        }
        else
        {
            SetbUp(false);

            // 左を向いている
            if (transform.localScale.x < 0f)
            {
                dirPlayer = -1;
            }
            if (transform.localScale.x > 0f)
            {
                dirPlayer = 1;
            }
        }

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
            SetbShot(false);
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
        if (Input.GetButtonDown("Jump"))
        {
            SetbWaitingShot(false);
            SetbShot(false);
            // 一回目のジャンプ
            if (bOnGround && JumpNum == 0)
            {
                if (bDoubleJump)
                {
                    JumpNum++;
                }
                
                // Jumpさせる
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            }else if(JumpNum == 1 && bDoubleJump)
            {
                JumpNum = 0;

                // Jumpさせる
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            }
            
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
        // Playerが上を向いているかどうか
        anim.SetBool("bUp", bUp);
        // ShotAnim
        anim.SetBool("bShot", bShot);
        // ShotWaiting
        anim.SetBool("bWaitingShot", bWaitingShot);
    }

    // Shot
    private void Shot()
    {
        // Shotボタンを離すと
        if (Input.GetMouseButtonUp(0))
        {
            GameObject Shot = Pre_Shot;

            if (ChargingTime >= ChargeTime && bChargeShot)
            {
                Shot = Pre_ChargeShot;
            }

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
            Shot.GetComponent<ShotController>().SetMoveDir(x, y);

            Vector3 shotPos = ShotPoint.position;
            if (bUp)
            {
                shotPos = ShotPoint_Up.position;
            }

            // 弾を生み出す
            Instantiate(Shot, shotPos, rot);

            SetbShot(true);
            if (bMove)
            {
                SetbShot(false);
            }
            SetbWaitingShot(false);

            // Shotボタンを押した回数をリセットする
            Shot_one = 0;

            shotWaitingTime = 0;

            ChargingTime = 0f;
            Pre_ChargeEffect.SetActive(false);
            Pre_ChargeEffectUp.SetActive(false);
            Pre_ChargeEffectJump.SetActive(false);

        }

        // ショットボタンを押しっぱなしにしている時
        if (Input.GetMouseButton(0))
        {
            if (bChargeShot)
            {
                if (ChargingTime < ChargeTime)
                {
                    // チャージエフェクトを表示する
                    if (dirPlayer == 0)
                    {
                        Pre_ChargeEffect.SetActive(false);
                        Pre_ChargeEffectUp.SetActive(true);
                        Pre_ChargeEffectJump.SetActive(false);
                    }
                    else if (!bOnGround)
                    {
                        Pre_ChargeEffect.SetActive(false);
                        Pre_ChargeEffectUp.SetActive(false);
                        Pre_ChargeEffectJump.SetActive(true);
                    }
                    else
                    {
                        Pre_ChargeEffect.SetActive(true);
                        Pre_ChargeEffectUp.SetActive(false);
                        Pre_ChargeEffectJump.SetActive(false);
                    }

                    // チャージ中
                    ChargingTime += Time.deltaTime;

                    if (Shot_one == 0)
                    {
                        Shot_one = 1;
                    }
                }
                else
                {
                    // チャージ完了
                    Pre_ChargeEffect.SetActive(false);
                    Pre_ChargeEffectUp.SetActive(false);
                    Pre_ChargeEffectJump.SetActive(false);
                }
            }

            SetbWaitingShot(true);
            SetbShot(true);
        }
    }

    // ShotAnimが終わると呼ばれる
    public void ShotFinish()
    {
        SetbShot(false);
    }

    //bUp
    public void SetbUp(bool A_bUp) { bUp = A_bUp; }

    // bMove
    public void SetbMove(bool A_bMove) { bMove = A_bMove; }

    // bShotSet
    public void SetbShot(bool Shot) { bShot = Shot; }

    // bWaitingShotSet
    public void SetbWaitingShot(bool WaitingShot) { bWaitingShot = WaitingShot; }

    // スキルを取得する
    public bool GetSkill(int SkillNo)
    {
        switch (SkillNo)
        {
            case 1:
                bDoubleJump = true;
                return true;

            case 2:
                bChargeShot = true;
                return true;

            default:
                return false;
        }
    }

    // SkillGetUIを表示させる
    public void DisplaySkillGetUI(bool D)
    {
        SkillGetUI.SetActive(D);
    }

    // Hpを増減させる
    public void AddHP(float h) { hp += h; }
}
