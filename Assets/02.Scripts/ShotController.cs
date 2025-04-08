using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    private Rigidbody2D theRB;

    public float moveSpeed;

    public Vector2 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody2D���擾����
        theRB = this.GetComponent<Rigidbody2D>();
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

        theRB.velocity = moveDir * moveSpeed;

    }

    // �����ƂԂ������Ƃ�
    private void OnTriggerEnter2D(Collider2D other)
    {
        // �����������̂�Player�ȊO�̎�
        if (other.gameObject.tag != "Player")
        {
            // ����Shot���폜����
            Destroy(gameObject);
        }
    }

    public void SetMoveDir(float x,float y)
    {
        moveDir = new Vector2(x, y);
    }
}
