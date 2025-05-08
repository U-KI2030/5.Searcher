using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperController : MonoBehaviour
{
    private PlayerController thePlayer;

    // GameController
    private GameController gameController;

    private bool bMove;

    // �ړ����x
    public float moveSpeed;

    /* �ړ� */
    public Transform target1;

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
        if (gameController != null && gameController.GetCondition())
        {
            if (bMove)
            {
                // �ړ��E����
                Move();
            }

            // Animation�֌W
            AnimControl();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �����������̂�Player�̎�
        if (other.gameObject.tag == "Player")
        {
            thePlayer = FindObjectOfType<PlayerController>();

            if (thePlayer != null && !thePlayer.GetbDamage())
            {
                // Player�Ƀ_���[�W�������s��
                thePlayer.AddHP(-5);
                // Player��Damage���󂯂���Ԃɂ���
                thePlayer.SetbDamage(true);
            }
        }
        bMove = false;
        // �j�󂷂�
        Destroy(this.gameObject);
    }

    // ������
    private void Initialize()
    {
        bMove = false;

        gameController = FindObjectOfType<GameController>();

        target1.transform.parent = null;
    }

    // �ړ�
    private void Move()
    {
        // targetNow�̈ʒu�ֈړ�����
        transform.position = Vector3.MoveTowards(
            transform.position,
            target1.position,
            moveSpeed * Time.deltaTime);
    }

    // Anim�֘A
    private void AnimControl()
    {
        if (anim != null)
        {
            // �����Ă��邩�ǂ���
            anim.SetBool("bMove", bMove);
        }
    }

    public void SetbMove(bool M) { bMove = M; }
}
