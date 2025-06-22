using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition : MonoBehaviour
{
    // SkillGetUI
    private bool bSkill;
    // PauseUI
    private bool bPause;
    // ExplanationUI
    private bool bExplanation;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ������
    public void Initialize()
    {
        bSkill = false;
        bPause = false;
        bExplanation = false;
    }

    // Player������ł��邩�ǂ����m�F
    // true : Player������ł�����
    // false : Player������ł��Ȃ�
    public bool CheckCondition()
    {
        if(!bSkill && !bPause && !bExplanation)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // bSkill��ύX����
    public void SetbSkill(bool S) { bSkill = S; }
    // bSkill���擾����
    public bool GetbSkill() { return bSkill; }
    // bPause��ύX����
    public void SetbPause(bool P) { bPause = P; }
    // bPause���擾����
    public bool GetbPause() { return bPause; }
    // bExplanation��ύX����
    public void SetbExplanation(bool E) { bExplanation = E; }
    // bExplanation���擾����
    public bool GetbExplanation() { return bExplanation; }
}
