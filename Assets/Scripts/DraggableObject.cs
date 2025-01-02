using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    private Vector3 offset; // 记录鼠标点击物体时的偏移量
    private Camera mainCamera; // 摄像机引用
    private bool isDragging = false; // 判断是否正在拖拽

    void Start()
    {
        mainCamera = Camera.main; // 获取主摄像机
    }

    void Update()
    {
        // 只有在点击物体时才进行拖拽
        if (isDragging)
        {
            DragObject();
        }
    }

    void OnMouseDown()
    {
        // 当鼠标点击物体时，计算鼠标和物体之间的偏移量
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - mousePosition;
        isDragging = true; // 开始拖拽
    }

    void OnMouseUp()
    {
        // 当鼠标释放时，停止拖拽
        isDragging = false;
    }

    void DragObject()
    {
        // 获取鼠标的世界坐标，并加上偏移量
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, transform.position.z);
    }
}
