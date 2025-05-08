using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageMoveController : MonoBehaviour
{
    public string currentScene;
    public string loadScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            ES3.Save("Stage", currentScene);
            SceneManager.LoadScene(loadScene);
        }
    }
}
