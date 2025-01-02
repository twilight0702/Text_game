using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class ObjectContentControl : MonoBehaviour
{
    public GameObject ShowObjectContent;

    public GameObject showText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Quit()
    {
        ShowObjectContent.SetActive(false);
    }

    public void Select()
    {
        CanCollect c = CentrolControl.ControlInstance.curObject.GetComponent<CanCollect>();
        if (c != null)
        {
            Destroy(CentrolControl.ControlInstance.curObject);
            ShowObjectContent.SetActive(false);
        }
        else
        {
            Debug.Log("Can't collect");
            Canvas canvas = FindFirstObjectByType<Canvas>();
            GameObject st = Instantiate(showText, canvas.transform);
            RectTransform rectTransform = st.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = Vector2.zero;
            Transform t = st.GetComponent<Transform>();
            foreach (RectTransform child in t)
            {
                // 尝试获取TextMeshPro组件
                TextMeshProUGUI tmp = child.GetComponent<TextMeshProUGUI>();
                if (tmp != null)
                {
                    tmp.text = "不能收集！";
                    StartCoroutine(SleepAndDestroy(1, st));
                }
                else
                {
                    Debug.Log($"子物体 {child.name} 没有TMP组件。");
                }

            }
        }
    }

    IEnumerator SleepAndDestroy(float second, GameObject obj)
    {
        yield return new WaitForSeconds(second);
        Destroy(obj);
    }
}
