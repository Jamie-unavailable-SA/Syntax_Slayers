using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BeginButton : MonoBehaviour
{
    public GameObject panel;
    public Button button;
    public GameObject WordManager;

    public void start()
    {
        WordManager.SetActive(false);
        button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(begin);
        }
        else
        {
            Debug.Log("No button component found", this);
        }
    }

    public void begin()
    {
        Debug.Log("Game starting", this);
        panel.SetActive(false);
        WordManager.SetActive(true);
    }

    void onDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(begin);
        }
    }
}
