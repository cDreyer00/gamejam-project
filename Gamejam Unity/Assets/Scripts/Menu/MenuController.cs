using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    #region Variables
    //Public
    [Space]
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject connectionsPanel;
    public GameObject optionsPanel;

    //Private

    #endregion

    private void Start()
    {

    }


    //Change menu according to the string set in the button
    public void MenuTransition(string button)
    {
        //Main menu buttons
        if (button == "Play")
        {
            connectionsPanel.SetActive(true);
            optionsPanel.SetActive(false);
            mainMenuPanel.SetActive(false);
        }
        else if (button == "Options")
        {
            connectionsPanel.SetActive(false);
            optionsPanel.SetActive(true);
            mainMenuPanel.SetActive(false);
        }
        else if (button == "Return")
        {
            connectionsPanel.SetActive(false);
            optionsPanel.SetActive(false);
            mainMenuPanel.SetActive(true);
        }
        else if (button == "Quit")
        {
            Application.Quit();
        }
    }
}


