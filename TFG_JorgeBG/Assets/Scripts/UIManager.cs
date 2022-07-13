using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public Image imageObject;
    public Animator transition;

    public GameObject pauseMenu;
    CanvasGroup transitionCanvasGroup;
    public Text hintText;

    playerController playerControllerScript;
    float timeToDisable;

    private void OnEnable()
    {
        playerControllerScript = FindObjectOfType<playerController>();
        EventManager.CompleteLevel += LevelCompleted;
        playerControllerScript.playerInputActions.characterControls.PauseMenu.started += OpenPause;
    }

    private void OnDisable()
    {
        EventManager.CompleteLevel -= LevelCompleted;
        playerControllerScript.playerInputActions.characterControls.PauseMenu.started -= OpenPause;
    }
    private void Awake()
    {
        transitionCanvasGroup = FindObjectOfType<CanvasGroup>();
    }

    private void Update()
    {
        hintText.color = new Color(hintText.color.r, hintText.color.g, hintText.color.b, 1 - transitionCanvasGroup.alpha);

        timeToDisable += Time.deltaTime;
        if(timeToDisable > 10)
        {
            hintText.enabled = false;
        }
    }

    public void LevelCompleted()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(buildIndex + " " + SceneManager.sceneCountInBuildSettings);
        if (buildIndex >= (SceneManager.sceneCountInBuildSettings-1))
        {
            Debug.Log("TO main");
            StartCoroutine(LoadNewLevel(0));
        }
        else
        {
            Debug.Log("To next");
            SaveLevelCompleted(buildIndex);
            StartCoroutine(LoadNewLevel(buildIndex + 1));
        }
    }


    IEnumerator LoadNewLevel(int sceneIndex)
    {
        transition.SetTrigger("FadeIn");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(sceneIndex);
    }

    void SaveLevelCompleted(int sceneIndex)
    {
        //Tutorial unlock level 1
        if (sceneIndex == 1)
        {
            PlayerPrefs.SetInt("Level_1",1);
        }else if (sceneIndex == 2)
        {
            PlayerPrefs.SetInt("Level_2", 1);
        }
        else if (sceneIndex == 3)
        {
            PlayerPrefs.SetInt("Level_3", 1);
        }
    }


    private void OpenPause(InputAction.CallbackContext obj)
    {
        if (pauseMenu.active)
            pauseMenu.SetActive(false);
        else
            pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
    }
    public void Restart()
    {
        Debug.Log("Restart");
        StartCoroutine(LoadNewLevel(SceneManager.GetActiveScene().buildIndex));
    }
    public void MainMenu()
    {

        StartCoroutine(LoadNewLevel(0));
    }

}
