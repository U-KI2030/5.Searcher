using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    private Rigidbody2D theRB;

    public float moveSpeed;

    public Vector2 moveDir;

    public GameObject Pre_HitAnim;

    // 生き残っている時間
    private float survivalTime;
    // 消えるまでの時間
    public float vanishTime;

    private PlayerController thePlayer;

    // 弾の威力
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody2Dを取得する
        theRB = this.GetComponent<Rigidbody2D>();

        thePlayer = FindObjectOfType<PlayerController>();
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

        survivalTime += Time.deltaTime;
        if(survivalTime >= vanishTime)
        {
            // HitAnimを表示させる
            Instantiate(Pre_HitAnim, transform.position, Quaternion.identity);

            // このShotを削除する
            Destroy(gameObject);
        }

    }

    // 何かとぶつかったとき
    private void OnTriggerEnter2D(Collider2D other)
    {

        // 当たったものがPlayer以外の時
        if (other.gameObject.tag == "Wall")
        {
            // HitAnimを表示させる
            Instantiate(Pre_HitAnim, transform.position, Quaternion.identity);

            // このShotを削除する
            Destroy(gameObject);
        }
    }

    public void SetMoveDir(float x,float y)
    {
        moveDir = new Vector2(x, y);
    }
}
