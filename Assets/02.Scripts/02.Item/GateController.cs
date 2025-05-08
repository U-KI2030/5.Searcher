using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    private bool bOpen;
    private bool bOpening;
    private bool bClose;
    private bool bClosing;

    // ÉAÉjÉÅä÷åWïœêî
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        AnimControl();
    }

    private void Initialize()
    {
        bOpen = false;
        bOpening = false;
        bClose = false;
        bClosing = false;
    }

    private void AnimControl()
    {
        if (anim != null)
        {
            anim.SetBool("bOpen", bOpen);
            anim.SetBool("bOpening", bOpening);
            anim.SetBool("bClose", bClose);
            anim.SetBool("bClosing", bClosing);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            bClosing = false;
            bOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            bOpening = false;
            bClose = true;
        }
    }

    public void FinshOpen()
    {
        bOpen = false;
        bOpening = true;
    }
    
    public void FinshClose()
    {
        bClose = false;
        bClosing = true;
    }
}
