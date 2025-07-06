using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HowToPanel : MonoBehaviour
{
    public GameObject logo;
    public Button startbutton;
    public Button howbutton;
    public Button quitbutton;
    public GameObject howpanel;

    public void Start()
    {
        howbutton = GetComponent<Button>();

        if (howbutton != null)
        {
            howbutton.onClick.AddListener(showHowTo);
        }
        else
        {
            Debug.Log("No how to button assigned", this);
        }
    }

    void showHowTo()
    {
        logo.SetActive(false);
        startbutton.gameObject.SetActive(false);
        howbutton.gameObject.SetActive(false);
        quitbutton.gameObject.SetActive(false);
        howpanel.SetActive(true);

    }

    void onDestroy()
    {
        howbutton.onClick.RemoveListener(showHowTo);
    }
}
