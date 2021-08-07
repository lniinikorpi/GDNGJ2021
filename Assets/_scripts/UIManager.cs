using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;
    public GameObject mainPanel;
    public GameObject muteButton;
    public GameObject unMuteButton;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        mainPanel.SetActive(true);
        muteButton.SetActive(true);
        unMuteButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void MuteGame()
    {
        AudioManager.instance.Mute();
    }

    public void UnMuteGame()
    {
        AudioManager.instance.UnMute();
    }
}
