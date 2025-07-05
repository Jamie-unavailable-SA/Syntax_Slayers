using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialPanel : MonoBehaviour
{
    public GameObject panel;
    public GameObject WordManager;

    public void Start()
    {
        panel.SetActive(true);
        WordManager.SetActive(false);
    }

}
