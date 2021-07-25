using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public GameObject PlayerLook;
    public GameObject DesktopCanvas;
    public GameObject DoorWithPassword;

    GameObject Crosshair;

    public bool isMasterClient;
    public bool PVisMine;
    public string chatText;


    private void Awake()
    {
        instance = this;



    }
    void Start()
    {
        Crosshair = GameObject.Find("CrossHair"); 
    }

    [SerializeField]float opacity = 0;
    bool explosion = true;
    void Update()
    {
  

        if (GameObject.Find("Pause_Panel") != null)
        {
            GetComponent<PlayerMovement>().canWalk = false;
            GetComponent<PlayerMovement>().canMoveCamera = false;
            Crosshair.SetActive(false);
        }
        

    }
}