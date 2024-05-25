using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject santa;
    public GameObject platform;
    public GameObject menuMain;
    public GameObject menuCustomize;
    public RelativeMovement relMove;

    // Start is called before the first frame update
    void Start()
    {
        relMove.canMove = false;
        //santa.SetActive(false);
        //platform.SetActive(false);
        menuCustomize.SetActive(false);
        DontDestroyOnLoad(santa);
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        santa.SetActive(true);
        SceneManager.LoadScene("MainScene");
        relMove.GameStart();
    }

    public void CustomizeButton()
    {
        santa.SetActive(true);
        platform.SetActive(true);
        menuCustomize.SetActive(true);
        menuMain.SetActive(false);
    }
    public void QuitButton()
    {
        Application.Quit();
        
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
