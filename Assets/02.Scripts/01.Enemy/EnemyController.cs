using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // GameController
    private GameController gameController;

    /* �̗� */
    public float hp;

    // �摜�֌W
    public SpriteRenderer sr;

    // �_���[�W���󂯂��ۂ̎��Ԍv���p�ϐ�
    private float DamageTime;

    private bool bDamage;

    // �Q�[�����̃R���f�B�V����
    private Condition condition;

    // Start is called before the first frame update
    void Start()
    {
        // ����������
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController != null && condition.CheckCondition())
        {
            // ���g��HP���`�F�b�N����
            CheckHP();

            // Damage���󂯂Ă��鎞�ɓ_�ł���
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

    // HP�𑝌�������
    public void AddHP(float h) { 
        hp += h;
        // Damage��Ԃɂ���
        SetbDamage(true);
    }

    // Damage���󂯂��ۂ̓_�ŏ���
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

    // Damage�̎��Ԃɉ����ē����x�̒l��Ԃ�
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
