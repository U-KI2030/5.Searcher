using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemyController : MonoBehaviour
{
    private PlayerController thePlayer;

    // GameController
    private GameController gameController;

    /* �ړ� */
    public Transform target1;
    public Transform target2;

    // ���݂̈ړ���
    private Transform targetNow;

    // �ړ����x
    public float moveSpeed;

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
            // �ړ��E����
            Move();
        }
    }

    private void Initialize()
    {
        gameController = FindObjectOfType<GameController>();

        targetNow = target1;

        target1.transform.parent = null;
        target2.transform.parent = null;
    }

    // �ړ�������
    private void Move()
    {
        // �܂�targetNow�̒n�_�ɂ��Ȃ���
        if (transform.position != targetNow.position)
        {
            // targetNow�̈ʒu�ֈړ�����
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetNow.position,
                moveSpeed * Time.deltaTime);
        }
        else
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

        // ������ς���
        // ������
        if (targetNow == target1)
        {
            transform.localScale = Vector3.one;
        }
        // �E����
        if (targetNow == target2)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
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
                thePlayer.AddHP(-10);
                // Player��Damage���󂯂���Ԃɂ���
                thePlayer.SetbDamage(true);
            }
        }
    }

}
