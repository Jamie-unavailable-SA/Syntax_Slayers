using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpenerPanel : MonoBehaviour
{
    public GameObject logo;
    public GameObject startbutton;
    public GameObject quitbutton;
    bool panelshown = false;
    public GameObject panel;

    public void onAwake()
    {
        logo.SetActive(false);
        startbutton.SetActive(false);
        quitbutton.SetActive(false);

        if (panelshown == false)
        {
            panel.SetActive(true);
        }
        else
        {
            Debug.Log("No panel assigned.", this);
        }
    }
}
