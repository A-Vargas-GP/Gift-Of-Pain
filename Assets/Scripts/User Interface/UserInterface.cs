using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserInterface : MonoBehaviour
{
    [Header("Camera Object")]
    [Tooltip("Camera Reference")]
    public GameObject camera;
    private OrbitCamera camScript;

    [Tooltip("Spawning Reference")]
    public GameObject spawner;

    [Header("Timer - UI Appearance (Active)")]
    [Tooltip("References")]
    public TMP_Text timer;
    [SerializeField] private float time = 400.0f;
    private int minutes;
    private int timeRemaining;

    [Header("UI Appearance - Inactive")]
    [Tooltip("References")]
    public PlayerSettingsScriptableObject user_interface_scriptable_obj;
    public GameObject canvasUI;
    public Toggle toggleObj;
    public Toggle toggleSpawner;
    [SerializeField] private bool turnUI;
    [SerializeField] private bool keyPressed = true;
    // Start is called before the first frame update

    void Start()
    {
        camScript = camera.GetComponent<OrbitCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        TurnOnEscape();
        timerText();
    }

    void timerText()
    {
        if (time <= 0)
        {
            time = 0;
        }
        
        time-=Time.deltaTime;
        minutes = (int)(time / 60);
        timeRemaining = (int)(time % 60);
        timer.text = minutes.ToString("00") + ":" + timeRemaining.ToString("00");
    }

    void AppearUI()
    {
        if (turnUI)
        {
            canvasUI.SetActive(true);
            Time.timeScale = 0; //Pause Scene
        }
        else
        {
            canvasUI.SetActive(false);
            Time.timeScale = 1; //Unpause Scene
        }
    }

    void TurnOnEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {            
            Debug.Log("Key Pressed");

            if (keyPressed) //value is true
            {
                turnUI = true;
                keyPressed = !keyPressed; //value turns false
            }
            else
            {
                turnUI = false;
                keyPressed = !keyPressed; //value turns true
            }
            
            AppearUI();
        }
    }

    public void InvertCameraSettings()
    {
        if (toggleObj.isOn)
        {
            user_interface_scriptable_obj.invertCamera = true;
        }
        else
        {
            user_interface_scriptable_obj.invertCamera = false;
        }
    }

    public void SpawnSettings()
    {
        if (toggleSpawner.isOn)
        {
            user_interface_scriptable_obj.enemySpawning = false;
        }
        else
        {
            user_interface_scriptable_obj.enemySpawning = true;
        }
    }
}
