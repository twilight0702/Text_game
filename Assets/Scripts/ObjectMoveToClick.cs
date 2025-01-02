using UnityEngine;

public class ObjectMoveToClick : MonoBehaviour
{
    public float moveSpeed = 5f; // 初始移动速度
    public float springConstant = 10f; // 弹簧常数，控制弹簧的强度
    public float damping = 0.5f; // 阻尼系数，控制震荡的幅度减少速度
    private Coroutine moveCoroutine; // 存储当前执行的协程
    private Vector3 targetPosition; // 目标位置
    private Vector3 velocity = Vector3.zero; // 物体的当前速度
    void Update()
    {
        // 检测鼠标点击
        if (Input.GetMouseButtonDown(0)) // 鼠标左键点击
        {
            // 获取鼠标点击的屏幕坐标
            Vector3 mousePosition = Input.mousePosition;
            Debug.Log($"鼠标点击屏幕坐标: {mousePosition}");

            // 将屏幕坐标转换为世界坐标
            targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            targetPosition.z = 0; // 确保 Z 轴位置为 0 (2D 游戏)

            // 如果当前有移动协程在执行，先停止它
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }

            // 开始弹簧效果平滑移动
            moveCoroutine = StartCoroutine(MoveToPosition());

            Debug.Log($"物体已开始移动到: {targetPosition}");
        }
    }

    private System.Collections.IEnumerator MoveToPosition()
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f || velocity.magnitude > 0.1f)
        {
            // 计算目标与当前位置的差距
            Vector3 direction = targetPosition - transform.position;

            // 弹簧运动的核心公式：
            // F = -k * x - b * v (弹簧力 + 阻尼力)
            // 其中 k 是弹簧常数，b 是阻尼系数，x 是位置差，v 是速度

            // 计算弹簧力
            Vector3 springForce = springConstant * direction;

            // 计算阻尼力
            Vector3 dampingForce = -damping * velocity;

            // 合力 = 弹簧力 + 阻尼力
            Vector3 force = springForce + dampingForce;

            // 更新速度
            velocity += force * Time.deltaTime;

            // 更新位置
            transform.position += velocity * Time.deltaTime;

            // 每帧等待
            yield return null;
        }

        // 确保物体到达目标位置并停止
        transform.position = targetPosition;
        velocity = Vector3.zero; // 停止震荡
    }
}
