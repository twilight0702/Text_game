using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollider : MonoBehaviour
{
    public GameObject obj; // 需要检查碰撞的物体
    private Collider2D objCollider; // 存储 obj 的 Collider2D 组件

    // Start is called before the first frame update
    void Start()
    {
        // 获取 obj 的 Collider2D 组件
        objCollider = obj.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 在这里可以做一些其他的检查或逻辑
    }

    // 使用正确的碰撞事件方法
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 如果碰撞的对象是我们想要检查的 objCollider
        if (collision.collider == objCollider)
        {
            Debug.Log("碰撞了");
        }
    }
}
