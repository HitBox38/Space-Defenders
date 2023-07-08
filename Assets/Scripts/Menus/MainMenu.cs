using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private RectTransform objToRotate;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private float rotSpeed = 25;
    [SerializeField] private float loadingTime = 5f;

    private void Start()
    {
        StartCoroutine(LoadScreenVanish());
    }

    private void Update()
    {
        objToRotate.localEulerAngles -= Vector3.forward * Time.deltaTime * rotSpeed;
    }

    private IEnumerator LoadScreenVanish()
    {
        yield return new WaitForSeconds(loadingTime);
        loadingScreen.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
