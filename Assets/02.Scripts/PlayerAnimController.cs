using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    private PlayerController Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ShotAnimationが終わると呼ばれる
    public void ShotFinish()
    {
        if (Player != null)
        {
            Player.ShotFinish();
        }
    }
}
