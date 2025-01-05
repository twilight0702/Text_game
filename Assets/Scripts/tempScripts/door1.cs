using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class door1 : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject text;
    public Transform buttonContainer;
    public GameObject buttonPrefab;
    public GameObject message;
    public Sprite newSprite;

    private bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void Initialize()
    {
        textContainer.GetComponentInChildren<TextMeshProUGUI>().text = "";
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }
    }
    private void OnMouseDown()
    {
        if (!isOpen)
        {
            textContainer.SetActive(true);
            textContainer.GetComponentInChildren<TextMeshProUGUI>().text = "门上锁了，需要钥匙才能打开。";
            foreach (Transform child in buttonContainer)
            {
                Destroy(child.gameObject);
            }

            GameObject button = Instantiate(buttonPrefab, buttonContainer); // 生成按钮

            if (Player.playerInstance.getKey)
            {
                button.GetComponentInChildren<TextMeshProUGUI>().text = "尝试使用刚刚获得的钥匙开门"; // 设置按钮文字
                button.GetComponent<Button>().onClick.AddListener(F1); // 设置点击事件
            }
            else
            {
                button.GetComponentInChildren<TextMeshProUGUI>().text = "没有钥匙……"; // 设置按钮文字
                button.GetComponent<Button>().onClick.AddListener(F2); // 设置点击事件
            }
        }
        else{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }

    private void F1()
    {
        isOpen = true;
        ChangeObjectSprite(newSprite);
        Initialize();
        textContainer.GetComponentInChildren<TextMeshProUGUI>().text = "门打开了，你可以出去了";
    }

    private void ChangeObjectSprite(Sprite sprite)
    {
        // 获取 SpriteRenderer 组件
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            // 设置新的 Sprite
            spriteRenderer.sprite = sprite;
            Debug.Log("Sprite 已更改为：" + sprite.name);
        }
        else
        {
            Debug.LogError("未找到 SpriteRenderer 组件！");
        }
    }

    private void F2()
    {
        Initialize();
        textContainer.GetComponentInChildren<TextMeshProUGUI>().text = "去找找钥匙吧";
    }

}
