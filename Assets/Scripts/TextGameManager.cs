using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using TMPro;

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

    void Start()
    {
        LoadGameData(); // 加载游戏数据
        LoadScene(1);   // 从第一个场景开始
    }

    void LoadGameData()
    {
        // 从 Resources 文件夹中加载 JSON 文件
        string json = Resources.Load<TextAsset>("gameData").text;
        gameData = JsonUtility.FromJson<GameData>(json);
    }

    void LoadScene(int id)
    {
        // 找到当前场景
        currentScene = System.Array.Find(gameData.scenes, scene => scene.id == id);

        // 更新文本
        dialogueText.text = currentScene.text;

        // 清空之前的按钮
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }

        // 根据选项生成按钮
        foreach (var option in currentScene.options)
        {
            GameObject button = Instantiate(buttonPrefab, buttonContainer); // 生成按钮
            button.GetComponentInChildren<TextMeshProUGUI>().text = option.text; // 设置按钮文字
            button.GetComponent<Button>().onClick.AddListener(() => LoadScene(option.next)); // 设置点击事件
        }
    }
}
