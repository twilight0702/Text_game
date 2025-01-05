using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class guancai : MonoBehaviour
{
    private GameObject textContainer;
    private GameObject text;
    private Transform buttonContainer;
    private GameObject buttonPrefab;
    private GameObject message;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("bottle1");
        Touch1 t = obj.GetComponent<Touch1>();
        textContainer = t.textContainer;
        text = t.text;
        buttonContainer = t.buttonContainer;
        buttonPrefab = t.buttonPrefab;
        message = t.message;

        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        StartCoroutine(MoveUpAndDown());
        textContainer.SetActive(true);
        textContainer.GetComponentInChildren<TextMeshProUGUI>().text = "（里面好像有什么东西？）";
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }
    }

    // 移动参数
    public float moveDistance = 0.1f; // 移动的距离
    public float moveSpeed = 10f;    // 移动的速度
    public int moveCount = 2;       // 移动的次数（上下为一次）

    private Vector3 startPosition;  // 初始位置

    private IEnumerator MoveUpAndDown()
    {
        for (int i = 0; i < moveCount; i++)
        {
            yield return MoveToPosition(startPosition + Vector3.up * moveDistance);

            yield return MoveToPosition(startPosition);
        }
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        // 当前物体移动到目标位置
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null; // 等待下一帧
        }
    }
}

