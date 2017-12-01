using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class KBButtonNav : MonoBehaviour
{
    StartOptions startOp;
    QuitApplication quitApp;
    Pause pause;
    ShowPanels showPanels;

    int index = 0;
    int startSelect = 0;

    public int totalButtons;
    public int selection;

    public List<GameObject> hands;

    void Awake()
    {
        startOp = FindObjectOfType<StartOptions>();
        quitApp = FindObjectOfType<QuitApplication>();
        pause = FindObjectOfType<Pause>();
        showPanels = FindObjectOfType<ShowPanels>();

        selection = startSelect % hands.Count;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (index < totalButtons - 1)
            {
                index++;
                selection = (selection + 1) % hands.Count;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (index > 0)
            {
                index--;
                selection = (selection - 1) % hands.Count;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            for (int i = 0; i < hands.Count; i++)
            {
                if (i == selection && hands[i].name == "StartHand")
                {
                    startOp.StartButtonClicked();
                }
                else if (i == selection && hands[i].name == "ControlsHand")
                {
                    showPanels.ShowOptionsPanel();
                }
                else if (i == selection && hands[i].name == "ExitHand")
                {
                    showPanels.HideOptionsPanel();
                }
                else if (i == selection && hands[i].name == "QuitHand")
                {
                    quitApp.Quit();
                }
                else if (i == selection && hands[i].name == "ResumeHand")
                {
                    pause.UnPause();
                }
                else if (i == selection && hands[i].name == "RestartHand")
                {
                    SceneManager.LoadScene(1);
                }
            }
        }

        UpdateSelection();

    }

    void UpdateSelection()
    {
        for (int i = 0; i < hands.Count; i++)
        {
            if (i == selection)
                hands[i].SetActive(true);

            else
                hands[i].SetActive(false);
        }
    }
}
