using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    public RectTransform title;
    public RectTransform mainButtons;
    public RectTransform scrollLevels;

    RectTransform rectTransformCanvas;

    public Button buttonTutorial;
    public Button buttonLevel_1;
    public Button buttonLevel_2;
    public Button buttonLevel_3;
    
    float animationTime =2f;

    bool isMovingObjects = false;
    void Start()
    {
        rectTransformCanvas=FindObjectOfType<Canvas>().GetComponent<RectTransform>();
        //PlayerPrefs.DeleteAll();
        ReadPlayerData();
    }
    public void ReadPlayerData()
    {
        int level_1 = PlayerPrefs.GetInt("Level_1");
        int level_2 = PlayerPrefs.GetInt("Level_2");
        int level_3 = PlayerPrefs.GetInt("Level_3");

        //
        if (level_1 == 1)
        {
            buttonLevel_1.enabled = true;
            ChangeColor(buttonLevel_1.gameObject, Color.white);
        }
        else
        {
            buttonLevel_1.enabled = false;
            ChangeColor(buttonLevel_1.gameObject, Color.gray);
        }
        //
        if (level_2 == 1)
        {
            buttonLevel_2.enabled = true;
            ChangeColor(buttonLevel_2.gameObject, Color.white);
        }
        else
        {
            buttonLevel_2.enabled = false;
            ChangeColor(buttonLevel_2.gameObject, Color.gray);
        }
        //
        if (level_3 == 1)
        {
            buttonLevel_3.enabled = true;
            ChangeColor(buttonLevel_3.gameObject, Color.white);
        }
        else
        {
            buttonLevel_3.enabled = false;
            ChangeColor(buttonLevel_3.gameObject, Color.gray);
        }
    }
    public void ChangeColor(GameObject button, Color color)
    {
        button.GetComponent<Image>().color = color;
    }
    public void LoadLevelSelector()
    {
        if (!isMovingObjects)
        {
            isMovingObjects = true;
            StartCoroutine(MoveObjectOutsideCanvasLeft(title));
            StartCoroutine(MoveObjectOutsideCanvasLeft(mainButtons));
            StartCoroutine(MoveObjectCenterCanvas(scrollLevels));
        }
    }
    public void LoadOptions()
    {
        
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ReturnToMain()
    {
        if (!isMovingObjects)
        {
            isMovingObjects = true;
            StartCoroutine(MoveObjectCenterCanvas(title));
            StartCoroutine(MoveObjectCenterCanvas(mainButtons));
            StartCoroutine(MoveObjectOutsideCanvasRight(scrollLevels));
        }
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadLevel1()
    {
        SceneManager.LoadScene(2);
    }
    public void LoadLevel2()
    {
        SceneManager.LoadScene(3);
    }
    public void LoadLevel3()
    {
        SceneManager.LoadScene(4);
    }


    IEnumerator MoveObjectOutsideCanvasLeft(RectTransform rectTransform)
    {
        float elapsedTime = 0;
        //Vector2 endPosition = new Vector2(rectTransform.anchoredPosition.x - outsideOffset, rectTransform.anchoredPosition.y);
        Vector2 endPosition = new Vector2(rectTransform.rect.width-rectTransformCanvas.rect.width, rectTransform.anchoredPosition.y);
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime / animationTime;
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, endPosition, elapsedTime);
            yield return null;
        }

        rectTransform.anchoredPosition = endPosition;

    }
    IEnumerator MoveObjectOutsideCanvasRight(RectTransform rectTransform)
    {
        float elapsedTime = 0;
        //Vector2 endPosition = new Vector2(rectTransform.anchoredPosition.x + outsideOffset, rectTransform.anchoredPosition.y);
        Vector2 endPosition = new Vector2(rectTransformCanvas.rect.width+rectTransform.rect.width, rectTransform.anchoredPosition.y);
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime/animationTime;
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, endPosition, elapsedTime);
            yield return null;
        }

        rectTransform.anchoredPosition = endPosition;
    }
    IEnumerator MoveObjectCenterCanvas(RectTransform rectTransform)
    {
        float elapsedTime = 0;
        Vector2 endPosition = new Vector2(0, rectTransform.anchoredPosition.y);
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime/animationTime;
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, endPosition, elapsedTime);
            yield return null;
        }
        rectTransform.anchoredPosition = endPosition;
        isMovingObjects = false;
    }
}
