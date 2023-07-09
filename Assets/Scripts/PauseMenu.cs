using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject loseMenu;

    [Header("Slider")]
    [SerializeField] private Slider volumeSlider;

    private bool isInPauseMenu = false;
    private bool isInWinLoseMenu = false;

    public static PauseMenu Instance;

    private void Awake()
    {
        if(!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SoundManager.Instance.PlayMusic("GameTheme");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isInWinLoseMenu)
        {
            if(!isInPauseMenu)
            {
                pauseMenu.SetActive(true);
                isInPauseMenu = true;
                Time.timeScale = 0;
            }
            else
            {
                pauseMenu.SetActive(false);
                isInPauseMenu = false;
                Time.timeScale = 1;
            }
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ReturnToGame()
    {
        pauseMenu.SetActive(false);
        isInPauseMenu = false;
        Time.timeScale = 1;
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowLoseMenu()
    {
        loseMenu.SetActive(true);
        isInWinLoseMenu = true;
    }

    public void ShowWinMenu()
    {
        winMenu.SetActive(true);
        isInWinLoseMenu = true;
    }

    public void ChangeVolume()
    {
        SoundManager.Instance.SFXVolume(volumeSlider.value / 5);
        SoundManager.Instance.MusicVolume(volumeSlider.value / 5);
    }
}
