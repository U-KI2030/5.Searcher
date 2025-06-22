using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabController : MonoBehaviour
{
    private PlayerController thePlayer;

    // GameController
    private GameController gameController;

    // �Q�[�����̃R���f�B�V����
    private Condition condition;

    /* �ړ� */
    public Transform target1;
    public Transform target2;

    // ���݂̈ړ���
    private Transform targetNow;

    // �ړ����x
    public float moveSpeed;

    public float WaitTime;
    private float WaitingTime;

    private bool bMove;

    // �A�j���֌W�ϐ�
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        // ����������
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController != null && condition != null && condition.CheckCondition())
        {
            // �ړ��E����
            Move();

            // Animation�֌W
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

    // �ړ�������
    private void Move()
    {
        // �܂�targetNow�̒n�_�ɂ��Ȃ���
        if (transform.position != targetNow.position)
        {
            WaitingTime = 0;
            SetbMove(true);

            // targetNow�̈ʒu�ֈړ�����
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

        // ������ς���
        // ������
        if (targetNow == target1)
        {
            transform.localScale = new Vector3(7.5f, 7.5f, 1f);
        }
        // �E����
        if (targetNow == target2)
        {
            transform.localScale = new Vector3(-7.5f, 7.5f, 1f);
        }

    }

    // Anim�֘A
    private void AnimControl()
    {
        // �����Ă��邩�ǂ���
        anim.SetBool("bMove", bMove);
    }

    public void SetbMove(bool M) { bMove = M; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �����������̂�Player�̎�
        if (other.gameObject.tag == "Player")
        {
            thePlayer = FindObjectOfType<PlayerController>();

            if (thePlayer != null && !thePlayer.GetbDamage())
            {
                // Player�Ƀ_���[�W�������s��
                thePlayer.AddHP(-10);
                // Player��Damage���󂯂���Ԃɂ���
                thePlayer.SetbDamage(true);
            }
        }
    }
}
