using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D theRB;

    /* ----------------------------------------------------*/
    // �Q�[���R���f�B�V�����ϐ�
    private Condition condition;

/* ----------------------------------------------------*/
    // �ړ��E�W�����v�A����֌W�ϐ�
    public float moveSpeed;
    public float jumpForce;

    public Transform groundPoint;
    // Player���ړ����Ă��邩�ǂ���
    private bool bMove;
    // �n�ʂɕt���Ă��邩�ǂ���
    private bool bOnGround;
    public LayerMask whatIsGround;

    // ��L�[����͂���Ă��邩�ǂ���
    private bool bUp;

    // ����ڂ̃W�����v��
    private int JumpNum;

    // Player�̌����Ă���������i�[
    // -1 : ��
    // 0 : ��
    // 1 : �E
    private int dirPlayer;

/* ----------------------------------------------------*/
    // Shot�֌W�ϐ�
    // ShotPrefab
    public GameObject Pre_Shot;
    // ChargeShot
    public GameObject Pre_ChargeShot;

    // Shot���s�������ǂ���
    private bool bShot;
    // Shot�҂���Ԃł��邩�ǂ���
    private bool bWaitingShot;
    // Shot��Ԃ�ێ����鎞��
    public float shotWaitTime;
    // shot��Ԃ�ێ����Ă��鎞��
    private float shotWaitingTime;

    // �e���o��ʒu(Normal)
    public Transform ShotPoint;
    // �e���o��ʒu(Up)
    public Transform ShotPoint_Up;

    // ChargeShot�̗��ߎ���
    public float ChargeTime;
    // ChargeShot�̗��߂Ă��鎞��
    private float ChargingTime;

    // ChargeEffect
    public GameObject Pre_ChargeEffect;
    // ChargeEffect_UP
    public GameObject Pre_ChargeEffectUp;
    // ChargeEffect_Jump
    public GameObject Pre_ChargeEffectJump;

    // 1�񂾂����삷��
    private int Shot_one;

/* ----------------------------------------------------*/
    // �A�j���֌W�ϐ�
    public Animator anim;

/* ----------------------------------------------------*/
    // Skill�֌W
    // DoubleJump
    private bool bDoubleJump;
    // ChargeShot
    private bool bChargeShot;

    // SkillGetUI
    public GameObject SkillGetUI;

/* ----------------------------------------------------*/
    // GameController�֌W�ϐ�
    private GameController gameController;


/* ----------------------------------------------------*/
    // HP�֌W�ϐ�
    private float hp;

    // �摜�֌W
    public SpriteRenderer sr;

    // �_���[�W���󂯂Ă�����
    private bool bDamage;

    // �_���[�W���󂯂��ۂ̎��Ԍv���p�ϐ�
    private float DamageTime;

/* ----------------------------------------------------*/
    // Explanation�\���֌W
    private bool bExplanation;

    // LaptopUI
    public GameObject ExplanationUI;

    // Start is called before the first frame update
    void Start()
    {
        // ������
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController != null && condition != null)
        {
            // Player���n�ʂɑ������Ă��邩
            bOnGround = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround);

            // Player������\�̏�ԂŁA�_���[�W���󂯂Ă��Ȃ��Ƃ�
            if (condition.CheckCondition())
            {
                if (shotWaitingTime >= shotWaitTime)
                {
                    SetbWaitingShot(false);
                }
                else
                {
                    shotWaitingTime += Time.deltaTime;
                }

                // �ړ��E�L�[����
                Moving();

                // Shot
                Shot();

                // Damage���󂯂Ă��鎞
                if (bDamage)
                {
                    Damage();
                }

                // Comment��\������
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Comment();
                }

                // Space�L�[��������
                if (Input.GetButtonDown("Jump"))
                {
                    // bExplanation���ɂ��鎞
                    if (bExplanation)
                    {
                        // ExplanationUI���\������Ă����Ԃɂ���
                        condition.SetbExplanation(true);
                        // ExplanationUI���\�������
                        ExplanationUI.SetActive(true);
                        // Game���Ԃ�0�ɂ���
                        gameController.GameStop();
                    }
                    else
                    {
                        // �W�����v
                        Jumping();
                    }
                }




                    // �A�j���[�V�����֘A
                    AnimControl();
            }
        }
    }

    // ����������
    private void Initialize()
    {
        // ��������Ă��Ȃ�
        SetbUp(false);
        // �����ĂȂ�
        SetbMove(false);
        // Player�̌����Ă�������͉E(1)
        dirPlayer = 1;

        // Shot��Ԃł͂Ȃ�
        SetbShot(false);
        // Shot�҂���Ԃł͂Ȃ�
        SetbWaitingShot(false);
        shotWaitingTime = 0f;

        // �`���[�W�^�C����������
        ChargingTime = 0f;

        // Effect�֌W���\��
        Pre_ChargeEffect.SetActive(false);
        Pre_ChargeEffectUp.SetActive(false);
        Pre_ChargeEffectJump.SetActive(false);

        Shot_one = 0;

        // Jump�񐔂�0��
        JumpNum = 0;

        // �X�L�������b�N
        bDoubleJump = false;
        bChargeShot = false;

        // SkillGetUI���\��
        SkillGetUI.SetActive(false);

        // GameController���擾
        gameController = FindObjectOfType<GameController>();

        // condition���擾
        condition = FindObjectOfType<Condition>();
        // HP�������l(100)�ɕύX
        hp = 100;

        // �_���[�W���󂯂Ă��Ȃ�
        bDamage = false;
        DamageTime = 0;

        // Explanation���\���ł��Ȃ�
        bExplanation = false;
        // ExplanationUI���\��
        ExplanationUI.SetActive(false);

    }

    // Moving
    private void Moving()
    {
        // ��L�[����͂��Ă��邩�ǂ����m�F
        if (Input.GetAxisRaw("Vertical") > 0 && Mathf.Abs(theRB.velocity.x) == 0)
        {
            SetbUp(true);
            dirPlayer = 0;
        }
        else
        {
            SetbUp(false);

            // ���������Ă���
            if (transform.localScale.x < 0f)
            {
                dirPlayer = -1;
            }
            if (transform.localScale.x > 0f)
            {
                dirPlayer = 1;
            }
        }

        // ���ړ��̑�����擾���APlayer�̓����Ɋi�[����
        theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, theRB.velocity.y);

        // �������ɃL�[���͂���Ă�����A��������
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
        SetbWaitingShot(false);
        SetbShot(false);
        // ���ڂ̃W�����v
        if (bOnGround && JumpNum == 0)
        {
            if (bDoubleJump)
            {
                JumpNum++;
            }
                
            // Jump������
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
        }else if(JumpNum == 1 && bDoubleJump)
        {
            JumpNum = 0;

            // Jump������
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
        }
    }

    // Shot
    private void Shot()
    {
        // Shot�{�^���𗣂���
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
                // �E
                case 1:
                    x = 1;
                    y = 0;
                    break;
                // ��
                case -1:
                    x = -1;
                    y = 0;

                    rot = Quaternion.AngleAxis(180.0f, new Vector3(0.0f, 0.0f, 1.0f));
                    break;
                // ��
                case 0:
                    x = 0;
                    y = 1;

                    rot = Quaternion.AngleAxis(90.0f, new Vector3(0.0f, 0.0f, 1.0f));
                    break;
            }

            // �e�̕�����ݒ肷��
            Shot.GetComponent<ShotController>().SetMoveDir(x, y);

            Vector3 shotPos = ShotPoint.position;
            if (bUp)
            {
                shotPos = ShotPoint_Up.position;
            }

            // �e�𐶂ݏo��
            Instantiate(Shot, shotPos, rot);

            SetbShot(true);
            if (bMove)
            {
                SetbShot(false);
            }
            SetbWaitingShot(false);

            // Shot�{�^�����������񐔂����Z�b�g����
            Shot_one = 0;

            shotWaitingTime = 0;

            ChargingTime = 0f;
            Pre_ChargeEffect.SetActive(false);
            Pre_ChargeEffectUp.SetActive(false);
            Pre_ChargeEffectJump.SetActive(false);

        }

        // �V���b�g�{�^�����������ςȂ��ɂ��Ă��鎞
        if (Input.GetMouseButton(0))
        {
            if (bChargeShot)
            {
                if (ChargingTime < ChargeTime)
                {
                    // �`���[�W�G�t�F�N�g��\������
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

                    // �`���[�W��
                    ChargingTime += Time.deltaTime;

                    if (Shot_one == 0)
                    {
                        Shot_one = 1;
                    }
                }
                else
                {
                    // �`���[�W����
                    Pre_ChargeEffect.SetActive(false);
                    Pre_ChargeEffectUp.SetActive(false);
                    Pre_ChargeEffectJump.SetActive(false);
                }
            }

            SetbWaitingShot(true);
            SetbShot(true);
        }
    }

    // Damage���󂯂��ۂ̓_�ŏ���
    private void Damage()
    {
        float transparency = 1.0f;

        DamageTime += Time.deltaTime;

        transparency = GetTransparency(DamageTime);

        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, transparency);

        if (DamageTime >= 1.0f)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1.0f);
            DamageTime = 0f;
            SetbDamage(false);
        }
    }

    // Comment��\��������
    private void Comment()
    {
        Debug.Log(1);
    }

    // Anim�֘A
    private void AnimControl()
    {
        // Player���n�ʂɑ������Ă��邩
        anim.SetBool("bOnGround", bOnGround);
        // Player�̑��x���i�[
        anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
        // Player���ړ����Ă��邩�ǂ���
        anim.SetBool("bMove", bMove);
        // Player����������Ă��邩�ǂ���
        anim.SetBool("bUp", bUp);
        // ShotAnim
        anim.SetBool("bShot", bShot);
        // ShotWaiting
        anim.SetBool("bWaitingShot", bWaitingShot);
    }

    // ShotAnim���I���ƌĂ΂��
    public void ShotFinish()
    {
        SetbShot(false);
    }

    // �X�L�����擾����
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

    // SkillGetUI��\��������
    public void DisplaySkillGetUI(bool D)
    {
        SkillGetUI.SetActive(D);
    }

    // Hp�𑝌�������
    public void AddHP(float h) { hp += h; }

    // Damage�̎��Ԃɉ����ē����x�̒l��Ԃ�
    private float GetTransparency(float DamageTime)
    {
        if(DamageTime <= 0.1f)
        {
            return 1.0f;
        }
        else if(DamageTime <= 0.2f)
        {
            return 0.0f;
        }
        else if (DamageTime <= 0.3f)
        {
            return 1.0f;
        }
        else if (DamageTime <= 0.4f)
        {
            return 0.0f;
        }
        else if (DamageTime <= 0.5f)
        {
            return 1.0f;
        }
        else if (DamageTime <= 0.6f)
        {
            return 0.0f;
        }
        else if (DamageTime <= 0.7f)
        {
            return 1.0f;
        }
        else if (DamageTime <= 0.8f)
        {
            return 0.0f;
        }
        else if (DamageTime <= 0.9f)
        {
            return 1.0f;
        }
        else if (DamageTime <= 1.0f)
        {
            return 0.0f;
        }
        return 1.0f;
    }

    //bUp
    public void SetbUp(bool A_bUp) { bUp = A_bUp; }

    // bMove
    public void SetbMove(bool A_bMove) { bMove = A_bMove; }

    // bShotSet
    public void SetbShot(bool Shot) { bShot = Shot; }

    // bWaitingShotSet
    public void SetbWaitingShot(bool WaitingShot) { bWaitingShot = WaitingShot; }

    // bDamage
    public void SetbDamage(bool d) { bDamage = d; }

    public bool GetbDamage() { return bDamage; }

    public void SetbExplanation(bool L) { bExplanation = L; }

    // bLaptop
    public bool GetbExplanation() { return bExplanation; }

    public GameObject GetExplanation() { return ExplanationUI; }
}
