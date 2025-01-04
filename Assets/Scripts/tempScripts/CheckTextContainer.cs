using System.Collections;
using System.Collections.Generic;
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
            }
        }
        lastCheck=temp;
    }
}
