using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CentrolControl : MonoBehaviour
{
    public static CentrolControl ControlInstance;
    public GameObject ShowObjectContent;
    // Start is called before the first frame update
    void Start()
    {
        if(ControlInstance == null)
        {
            ControlInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    
        ShowObjectContent.SetActive(false);
        
    }

    public Block curObjectContent;
    public GameObject curObject;

    // Update is called once per frame
    void Update()
    {
        if(!HasCollectionObject())
        {
            SceneManager.LoadScene("GameScene");
        }
    }

        // 检查场景中是否有标签为 "Collection" 的物体
    bool HasCollectionObject()
    {
        // 使用标签查找场景中所有符合条件的物体
        GameObject[] collectionObjects = GameObject.FindGameObjectsWithTag("Collection");
        
        // 如果找不到物体，返回 false
        return collectionObjects.Length > 0;
    }
}
