using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerGame : MonoBehaviour
{
    public static UIManagerGame instance = null;
    public GameObject pausePanel;
    public GameObject muteButton;
    public GameObject unMuteButton;
    public GameObject diePanel;
    public GameObject completeGamePanel;
    public Slider healthSlider;
    [HideInInspector]
    public bool isPaused = false;

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
        pausePanel.SetActive(false);
        diePanel.SetActive(false);
        completeGamePanel.SetActive(false);
        if(AudioManager.instance.isMuted)
        {
            muteButton.SetActive(false);
            unMuteButton.SetActive(true);
        }
        else
        {
            muteButton.SetActive(true);
            unMuteButton.SetActive(false);
        }
    }

    public void PauseGame()
    {
        isPaused = !isPaused;
        if(isPaused)
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }
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

    public void UpdateHealthSlider(float value)
    {
        healthSlider.value = value;
    }

    public void RestartGame()
    {
        GameManager.instance.RestartGame();
    }
}
