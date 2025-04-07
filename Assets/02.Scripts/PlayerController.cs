using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D theRB;

/* ----------------------------------------------------*/
    // �ړ��E�W�����v�֌W�ϐ�
    public float moveSpeed;
    public float jumpForce;

    public Transform groundPoint;
    // Player���ړ����Ă��邩�ǂ���
    private bool bMove;
    // �n�ʂɕt���Ă��邩�ǂ���
    private bool bOnGround;
    public LayerMask whatIsGround;

/* ----------------------------------------------------*/
    // Shot�֌W�ϐ�
    // Shot���s�������ǂ���
    private bool bShot;
    // Shot�҂���Ԃł��邩�ǂ���
    private bool bWaitingShot;
    // Shot��Ԃ�ێ����鎞��
    public float shotWaitTime;
    // shot��Ԃ�ێ����Ă��鎞��
    private float shotWaitingTime;

/* ----------------------------------------------------*/
    // �A�j���֌W�ϐ�
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        // ������
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

        // �ړ�
        Moving();

        // �W�����v
        Jumping();

        // Shot
        Shot();








        // �A�j���[�V�����֘A
        AnimControl();
    }

    // ����������
    private void Initialize()
    {
        SetbMove(false);
        // Shot��Ԃł͂Ȃ�
        SetbShot(false);
        SetbWaitingShot(false);
        shotWaitingTime = 0f;
    }

    // Moving
    private void Moving()
    {
        // ���ړ��̑�����擾���APlayer�̓����Ɋi�[����
        theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, theRB.velocity.y);

        // �������ɃL�[���͂���Ă�����A��������
        if (theRB.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            SetbMove(true);
        }
        else if (theRB.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
            SetbMove(true);
        }
        else
        {
            SetbMove(false);
        }
    }

    //Jumping
    private void Jumping()
    {
        // Player���n�ʂɑ������Ă��邩
        bOnGround = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround);

        // Jump�L�[���͂�����APlayer���n�ʂɂ��Ă��鎞
        if (Input.GetButtonDown("Jump") && bOnGround)
        {
            SetbWaitingShot(false);
            SetbShot(false);
            // Jump������
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
        }
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
        // ShotAnim
        anim.SetBool("bShot", bShot);
        // ShotWaiting
        anim.SetBool("bWaitingShot", bWaitingShot);
    }

    // Shot
    private void Shot()
    {
        // Shot�{�^����������
        if (Input.GetMouseButtonDown(0))
        {
            SetbShot(true);
            SetbWaitingShot(false);
            shotWaitingTime = 0;
        }

    }

    // ShotAnim���I���ƌĂ΂��
    public void ShotFinish()
    {
        SetbShot(false);
        SetbWaitingShot(true);
    }

    // bMove
    public void SetbMove(bool A_bMove) { bMove = A_bMove; }

    // bShotSet
    public void SetbShot(bool Shot) { bShot = Shot; }

    // bWaitingShotSet
    public void SetbWaitingShot(bool WaitingShot) { bWaitingShot = WaitingShot; }
}
