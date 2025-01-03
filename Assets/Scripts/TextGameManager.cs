using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using System;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

[System.Serializable]
public class Option
{
    public string text;
    public int next;
}

[System.Serializable]
public class Scene
{
    public int id;
    public string text;
    public Option[] options;
    public int next;
}

[System.Serializable]
public class GameData
{
    public Scene[] scenes;
}

public class TextGameManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // 用于显示场景文字
    public GameObject buttonPrefab; // 按钮预制体
    public Transform buttonContainer; // 按钮父对象容器
    private GameData gameData;
    private Scene currentScene;
    public String file = "gameData";

    private float revealSpeed = 0.05f;
    private State curState = State.IsShowing;
    private Coroutine curCoroutine; // 用来存储协程的引用

    private enum State
    {
        IsShowing,
        ShowAll,
        NotStart,
        CanGo
    }
    void Start()
    {
        curState = State.NotStart;
        LoadGameData(); // 加载游戏数据
        LoadScene(1);   // 从第一个场景开始
    }
    void Update()
    {
        // 鼠标左键点击且鼠标指针在UI上时
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject())
        {
            switch (curState)
            {
                case State.CanGo:
                    curState = State.NotStart;
                    GoToNext(currentScene.next);
                    break;

                case State.IsShowing:
                    StopCoroutine(curCoroutine);
                    dialogueText.text = currentScene.text;
                    curState = State.ShowAll;
                    createBotton();
                    break;
            }
        }
    }

    void LoadGameData()
    {
        // 从 Resources 文件夹中加载 JSON 文件
        string json = Resources.Load<TextAsset>(file).text;
        gameData = JsonUtility.FromJson<GameData>(json);
    }

    void LoadScene(int id)
    {
        curState = State.NotStart;
        // 清空之前的按钮
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }

        // 找到当前场景
        currentScene = System.Array.Find(gameData.scenes, scene => scene.id == id);

        // 更新文本
        curCoroutine = StartCoroutine(ShowText(currentScene.text));
    }

    private void createBotton()
    {
        if (currentScene.options.Length == 0)
        {
            Debug.Log("没有选项");
            curState = State.CanGo;
        }
        else
        {
            Debug.Log("有选项");
            // 根据选项生成按钮
            foreach (var option in currentScene.options)
            {
                GameObject button = Instantiate(buttonPrefab, buttonContainer); // 生成按钮
                button.GetComponentInChildren<TextMeshProUGUI>().text = option.text; // 设置按钮文字
                button.GetComponent<Button>().onClick.AddListener(() => GoToNext(option.next)); // 设置点击事件
            }
        }
    }

    IEnumerator ShowText(string content)
    {
        dialogueText.text = ""; // 确保文本清空
        curState = State.IsShowing;
        for (int i = 0; i <= content.Length; i++)
        {
            dialogueText.text = content.Substring(0, i); // 截取部分字符串显示
            yield return new WaitForSeconds(revealSpeed); // 等待指定时间
        }
        curState = State.ShowAll;
        createBotton();
    }

    public Image fadeImage;  // 黑色图像
    public float fadeDuration = 1f;  // 渐变持续时间
    private void GoToNext(int next)
    {
        if (next != -1)
        {
            LoadScene(next);
        }
        else
        {
            StartCoroutine(Fade(1f));
        }
    }

    private IEnumerator Fade(float targetAlpha)
    {
        fadeImage.color = new Color(0, 0, 0, 0);
        fadeImage.gameObject.SetActive(true);
        
        float startAlpha = fadeImage.color.a;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timeElapsed / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // 确保最终 alpha 值正确
        fadeImage.color = new Color(0, 0, 0, targetAlpha);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
