using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    private Rigidbody2D theRB;

    public float moveSpeed;

    public Vector2 moveDir;

    public GameObject Pre_HitAnim;

    // �����c���Ă��鎞��
    private float survivalTime;
    // ������܂ł̎���
    public float vanishTime;

    private PlayerController thePlayer;

    private GameController gameController;

    // �e�̈З�
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody2D���擾����
        theRB = this.GetComponent<Rigidbody2D>();

        thePlayer = FindObjectOfType<PlayerController>();

        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Shot�̌��ݒn���擾
        //Transform tran = this.transform;
        //Vector3 pos = tran.position;

        //// x���W�ɉ��Z����
        //pos.x += moveSpeed;

        //// �ʒu��ύX����@
        //transform.position = pos;

        if (gameController != null)
        {
            if (gameController.GetCondition())
            {
                theRB.velocity = moveDir * moveSpeed;

                survivalTime += Time.deltaTime;
                if (survivalTime >= vanishTime)
                {
                    // HitAnim��\��������
                    Instantiate(Pre_HitAnim, transform.position, Quaternion.identity);

                    // ����Shot���폜����
                    Destroy(gameObject);
                }
            }
        }

    }

    // �����ƂԂ������Ƃ�
    private void OnTriggerEnter2D(Collider2D other)
    {

        // �����������̂�Wall�̎�
        if (other.gameObject.tag == "Wall")
        {
            // HitAnim��\��������
            Instantiate(Pre_HitAnim, transform.position, Quaternion.identity);

            // ����Shot���폜����
            Destroy(gameObject);
        }
    }

    public void SetMoveDir(float x,float y)
    {
        moveDir = new Vector2(x, y);
    }
}
