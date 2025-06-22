using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // GameController
    private GameController gameController;

    /* 体力 */
    public float hp;

    // 画像関係
    public SpriteRenderer sr;

    // ダメージを受けた際の時間計測用変数
    private float DamageTime;

    private bool bDamage;

    // ゲーム中のコンディション
    private Condition condition;

    // Start is called before the first frame update
    void Start()
    {
        // 初期化処理
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController != null && condition.CheckCondition())
        {
            // 自身のHPをチェックする
            CheckHP();

            // Damageを受けている時に点滅する
            if (bDamage)
            {
                Damage();
            }
        }
    }

    private void Initialize()
    {
        gameController = FindObjectOfType<GameController>();

        condition = FindObjectOfType<Condition>();

        DamageTime = 0f;

        SetbDamage(false);
    }

    private void CheckHP()
    {
        if(hp <= 0f)
        {
            Destroy(this.gameObject);
        }
    }

    // HPを増減させる
    public void AddHP(float h) { 
        hp += h;
        // Damage状態にする
        SetbDamage(true);
    }

    // Damageを受けた際の点滅処理
    private void Damage()
    {
        if (sr != null)
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
    }

    // Damageの時間に応じて透明度の値を返す
    private float GetTransparency(float DamageTime)
    {
        if (DamageTime <= 0.1f)
        {
            return 1.0f;
        }
        else if (DamageTime <= 0.2f)
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

    public void SetbDamage(bool D) { bDamage = D; }

}
