using System;
using System.IO;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Block
{
    public int id;
    public string text;
    public String name;
}

[System.Serializable]
public class BlockCollection
{
    public Block[] blocks;
}

public class ObjectClickHandler : MonoBehaviour
{
    public int FindID = 12;

    public string jsonFileName = "blocks.json";  // 假设你的 JSON 文件叫 data.json
    public GameObject ShowObjectContent;
    private void OnMouseDown()
    {
        Debug.Log($"你点击了物体：{gameObject.name}");
        Interact();
    }

    private void Interact()
    {
        // 实现交互逻辑
        Debug.Log($"与 {gameObject.name} 进行交互！");

        ShowObjectContent.SetActive(true);

        // 读取 JSON 文件
        string jsonFilePath = Path.Combine("./Assets/Resources/", jsonFileName);
        if (File.Exists(jsonFilePath))
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            BlockCollection blockCollection = JsonUtility.FromJson<BlockCollection>(jsonData);

            // 查找 ID 为 12 的区块
            Block block = FindBlockById(blockCollection, FindID);
            if (block != null)
            {
                CentrolControl.ControlInstance.curObjectContent = block;
                CentrolControl.ControlInstance.curObject = gameObject;
                Debug.Log("ID为12的区块文本: " + block.text);
                GameObject name = GameObject.Find("ObjectName");
                name.GetComponent<TextMeshProUGUI>().text = block.name;
                GameObject content = GameObject.Find("Content");
                content.GetComponent<TextMeshProUGUI>().text = block.text;
            }
            else
            {
                Debug.Log("没有找到ID为12的区块");
            }
        }
        else
        {
            Debug.LogError("文件未找到: " + jsonFilePath);
        }

    }




    // 查找指定 ID 的区块
    Block FindBlockById(BlockCollection collection, int id)
    {
        foreach (Block block in collection.blocks)
        {
            if (block.id == id)
            {
                return block;
            }
        }
        return null;  // 如果没有找到返回 null
    }
}
