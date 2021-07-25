using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public static PlayerManager instance;

    public GameObject PlayerLook;
    public GameObject DesktopCanvas;
    public GameObject DoorWithPassword;

    GameObject Crosshair;

    public bool isMasterClient;
    public bool PVisMine;
    public string chatText;

    ChatManager _chatManager;

    private void Awake()
    {
        instance = this;

        /*if(MenuController._CurrentPlayer == 0)
        {
            //this.photonView.name = "PlayerPresent";
            GetComponent<FloorPuzzleController>().playerIsOnPresent = true;
        }
        else
        {
            //this.photonView.name = "PlayerFuture";
            GetComponent<FloorPuzzleController>().playerIsOnPresent = false;
        }*/

    }
    void Start()
    {
        if (!photonView.IsMine)
        {
            this.enabled = false;
            transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Camera>().enabled = false;
            GetComponent<PlayerMovement>().enabled = false;
        }
        else
        {

            if (PhotonNetwork.IsMasterClient)
            {
                isMasterClient = true;
                
            }
            else
            {
                isMasterClient = false;
                
            }
                

            instance = this;
            PVisMine = true;

           
            Debug.Log(isMasterClient);
        }

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
        else
        {
            DesktopPuzzle();
        }

        //call te credits and icrease his opacity
        if(GameObject.Find("END GAME FADE") != null)
        {


            if (GameObject.Find("END GAME FADE").activeInHierarchy)
            {
                

                GameObject.Find("END GAME FADE").GetComponent<Image>().color = new Color(GameObject.Find("END GAME FADE").GetComponent<Image>().color.r, GameObject.Find("END GAME FADE").GetComponent<Image>().color.g,
                    GameObject.Find("END GAME FADE").GetComponent<Image>().color.b, opacity);

                opacity += 0.3f * Time.deltaTime;

                if (explosion && opacity >= 0.8f)
                {
                    GameObject.Find("Explosion").GetComponent<AudioSource>().Play();
                    explosion = false;
                }
                

                // return to menu after presses left mouse button
                if (Input.GetMouseButton(0))
                {
                    PhotonNetwork.Disconnect();
                    SceneManager.LoadScene(0);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }

                
            }
        }
        

    }

    void DesktopPuzzle()
    {
        RaycastHit hit;
        if (Physics.Raycast(PlayerLook.transform.position, PlayerLook.transform.TransformDirection(Vector3.forward), out hit))
        {
            if (Input.GetButtonDown("Fire1") && hit.collider.name == "PC_Raycast")
            {
                DesktopCanvas.SetActive(true);

                if(MenuController._CurrentPlayer == 0)
                {
                    DoorWithPassword = GameObject.Find("Door0");
                }
                else
                {
                    DoorWithPassword = GameObject.Find("Door1");
                }
            }
        }


        // check if Desktop is enabled;
        if (DesktopCanvas.activeInHierarchy)
        {
            GetComponent<PlayerMovement>().canWalk = false;
            GetComponent<PlayerMovement>().canMoveCamera = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Crosshair.SetActive(false);
        }
        else
        {
            GetComponent<PlayerMovement>().canWalk = true;
            GetComponent<PlayerMovement>().canMoveCamera = true;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Crosshair.SetActive(true);
        }
    }
}