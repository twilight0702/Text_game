using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Paizi : MonoBehaviour
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
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        textContainer.SetActive(true);
        textContainer.GetComponentInChildren<TextMeshProUGUI>().text = "(指向的是出口……吗？)";
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }
    }

}
