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
        // Rigidbody2Dを取得する
        theRB = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Shotの現在地を取得
        //Transform tran = this.transform;
        //Vector3 pos = tran.position;

        //// x座標に加算する
        //pos.x += moveSpeed;

        //// 位置を変更する　
        //transform.position = pos;

        theRB.velocity = moveDir * moveSpeed;

    }

    // 何かとぶつかったとき
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 当たったものがPlayer以外の時
        if (other.gameObject.tag != "Player")
        {
            // このShotを削除する
            Destroy(gameObject);
        }
    }

    public void SetMoveDir(float x,float y)
    {
        moveDir = new Vector2(x, y);
    }
}
