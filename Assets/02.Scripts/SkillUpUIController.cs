using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUpUIController : MonoBehaviour
{
    private PlayerController thePlayer;

    // GameController
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();

        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameController != null && thePlayer != null)
        {
            if (gameController.GetbDisplayUI())
            {
                if (Input.GetButtonDown("Jump"))
                {
                    // ŠÔ‚ğŒ³‚É–ß‚·
                    gameController.GameStart();
                    // ƒQ[ƒ€‚Ìó‘Ô‚ğUI”ñ•\¦ó‘Ô‚É‚·‚é
                    gameController.SetbDisplayUI(false);
                    // UI‚ğ”ñ•\¦‚É‚·‚é
                    thePlayer.DisplaySkillGetUI(false);
                }
            }
        }
    }
}
