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

    // 初期化
    public void Initialize()
    {
        bSkill = false;
        bPause = false;
        bExplanation = false;
    }

    // Playerが操作できるかどうか確認
    // true : Playerが操作できる状態
    // false : Playerが操作できない
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

    // bSkillを変更する
    public void SetbSkill(bool S) { bSkill = S; }
    // bSkillを取得する
    public bool GetbSkill() { return bSkill; }
    // bPauseを変更する
    public void SetbPause(bool P) { bPause = P; }
    // bPauseを取得する
    public bool GetbPause() { return bPause; }
    // bExplanationを変更する
    public void SetbExplanation(bool E) { bExplanation = E; }
    // bExplanationを取得する
    public bool GetbExplanation() { return bExplanation; }
}
