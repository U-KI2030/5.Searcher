using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabController : MonoBehaviour
{
    public Rigidbody2D theRB;

    // GameController
    private GameController gameController;

    /* �̗� */
    public float hp;

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

        WaitingTime = 0;

        SetbMove(false);

    }

    // �ړ�������
    private void Move()
    {
        // �܂�targetNow�̒n�_�ɂ��Ȃ���
        if (transform.position != targetNow.position) {

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

    // HP�𑝌�������
    public void AddHP(float h) { hp += h; }

    public void SetbMove(bool M) { bMove = M; }
}
