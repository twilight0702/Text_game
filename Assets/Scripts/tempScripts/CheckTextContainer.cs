using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckTextContainer : MonoBehaviour
{
    public GameObject textContainer;
    private GameObject[] interactableObjects; // 存储场景中所有可交互物体
    public bool lastCheck;
    public GameObject message;
    // Start is called before the first frame update
    void Start()
    {
        message.SetActive(false);
        textContainer.SetActive(false);
        lastCheck=false;
        interactableObjects = GameObject.FindGameObjectsWithTag("Interactable");
        StartCoroutine(ShowMessage());
    }

    // Update is called once per frame
    void Update()
    {
        bool temp = textContainer.activeSelf;
        if (lastCheck != temp)
        {
            if (temp)
            {
                foreach (GameObject obj in interactableObjects)
                {
                    if (obj.TryGetComponent<Collider2D>(out Collider2D collider))
                    {
                        collider.enabled = false;
                    }
                }
            }
            else
            {
                foreach (GameObject obj in interactableObjects)
                {
                    if (obj.TryGetComponent<Collider2D>(out Collider2D collider))
                    {
                        collider.enabled = true;
                    }
                }
                GameObject bottle1= GameObject.Find("bottle1");
                if(bottle1!=null)
                {
                    Touch1 t= bottle1.GetComponent<Touch1>();
                    t.DestroyMyCourtine();
                }   
            }
        }
        lastCheck=temp;
    }

    public void setMessage(string text)
    {
        message.SetActive(true);
        message.GetComponentInChildren<TextMeshProUGUI>().text = text;
        StartCoroutine(HideMessage());
    }

    IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(2f);
        message.SetActive(false);
    }

    IEnumerator ShowMessage()
    {
        yield return new WaitForSeconds(1f);
        setMessage("点击场景中的物体试试看吧");
    }
}
