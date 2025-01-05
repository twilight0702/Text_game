using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ShowContent
{
    public int id;
    public string text;
    public AaptOptions[] aptOptions;
    public String showPeople;
    public string effect;
    public int next;
}

[System.Serializable]
public class AaptOptions
{
    public string text;
    public int next;
}

[System.Serializable]
public class ShowContentCollection
{
    public ShowContent[] contents;
}

public class Touch1 : MonoBehaviour
{
    //标记这个罐子是否被交互过
    private bool isUsed = false;

    private int StartID = 1;

    private ShowContentCollection ShowContents;

    private bool canGo = false;
    private ShowContent curContent;
    private Coroutine myCoroutine;
    //放置显示文字的实际游戏物品
    public GameObject textContainer;
    public GameObject text;
    public Transform buttonContainer;
    public GameObject buttonPrefab;
    public GameObject message;

    private GameObject[] interactableObjects; // 存储场景中所有可交互物体
    private bool isInteractionDisabled = false; // 标记交互是否被禁用


    // Start is called before the first frame update
    void Start()
    {
        string json = Resources.Load<TextAsset>("bottle1").text;
        ShowContents = JsonUtility.FromJson<ShowContentCollection>(json);

        interactableObjects = GameObject.FindGameObjectsWithTag("Interactable");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (canGo)
            {
                GoToNext(curContent.next);
            }
        }
    }

    /// <summary>
    /// 当鼠标点击罐子1的时候显示对应文字和交互选项
    /// </summary>
    private void OnMouseDown()
    {
        textContainer.SetActive(true);
        text.GetComponent<TextMeshProUGUI>().text = "";
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }

        if (isUsed)
        {
            text.GetComponent<TextMeshProUGUI>().text = "你已经检查过这个罐子了，看看别处吧。";
        }
        else
        {
            loadText(StartID);
        }
    }

    private void loadText(int id)
    {
        text.GetComponent<TextMeshProUGUI>().text = "";
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }
        canGo = false;
        curContent = System.Array.Find(ShowContents.contents, ShowContent => ShowContent.id == id);
        string textString = curContent.text;
        if (text != null)
        {
            myCoroutine = StartCoroutine(ShowText(textString));
        }
        string curEffect = curContent.effect;
        if (curEffect != null && curContent.effect == "get key")
        {
            isUsed = true;
            message.SetActive(true);
            message.GetComponentInChildren<TextMeshProUGUI>().text = "获得了 钥匙！";
            StartCoroutine(HideMessage());
            Player.playerInstance.getKey = true;
        }
    }

    IEnumerator ShowText(string content)
    {
        text.GetComponent<TextMeshProUGUI>().text = ""; // 确保文本清空
        for (int i = 0; i <= content.Length; i++)
        {
            text.GetComponent<TextMeshProUGUI>().text = content.Substring(0, i); // 截取部分字符串显示
            yield return new WaitForSeconds(0.05f);
        }
        Debug.Log("显示文本完毕：" + content);
        ShowBottons();
    }

    private void ShowBottons()
    {
        AaptOptions[] buttons = curContent.aptOptions;
        if (buttons == null)
        {
            Debug.Log("没有选项");
            canGo = true;
            return;
        }
        if (buttons.Length != 0)
        {
            Debug.Log("有选项");
            foreach (var option in buttons)
            {
                Debug.Log("选项：" + option.text);
                GameObject button = Instantiate(buttonPrefab, buttonContainer); // 生成按钮
                button.GetComponentInChildren<TextMeshProUGUI>().text = option.text; // 设置按钮文字
                button.GetComponent<Button>().onClick.AddListener(() => GoToNext(option.next)); // 设置点击事件
            }
        }
        else
        {
            Debug.Log("没有选项");
            canGo = true;
        }
    }

    private void GoToNext(int next)
    {
        if (next != -1)
        {
            loadText(next);
        }
        else
        {
            //textContainer.SetActive(false);
        }
    }

    IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(2f);
        message.SetActive(false);
    }

    public void DestroyMyCourtine()
    {
        if (myCoroutine != null)
        {
            StopCoroutine(myCoroutine);
        }
    }
}
