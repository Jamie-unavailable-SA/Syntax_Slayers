using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    public GameObject logo;
    public Button startbutton;
    public Button howbutton;
    public Button quitbutton;
    public GameObject howpanel;
    public Button backbutton;

    public void Start()
    {
        backbutton = GetComponent<Button>();

        if (backbutton != null)
        {
            backbutton.onClick.AddListener(goback);
        }
        else
        {
            Debug.Log("No button assigned", this);
        }
    }

    void goback()
    {
        logo.SetActive(true);
        startbutton.gameObject.SetActive(true);
        howbutton.gameObject.SetActive(true);
        quitbutton.gameObject.SetActive(true);
        howpanel.SetActive(false);
    }

    void onDestroy()
    {
        backbutton.onClick.RemoveListener(goback);
    }
}
