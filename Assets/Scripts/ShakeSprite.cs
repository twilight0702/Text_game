using UnityEngine;

public class ShakeSprite : MonoBehaviour
{
    public float shakeAmount = 0.1f; // 抖动的幅度
    public float shakeDuration = 0.5f; // 抖动持续时间
    public int shakeTimes = 10; // 抖动次数

    private Vector3 originalPosition; // 记录初始位置
    private float shakeTimer; // 计时器
    private int shakeCount; // 已经抖动的次数
    private bool isShaking = false; // 是否正在抖动

    void Update()
    {
        if (isShaking)
        {
            // 计时器递增
            shakeTimer += Time.deltaTime;

            // 每次抖动的间隔时间
            if (shakeTimer >= (shakeDuration / shakeTimes))
            {
                shakeTimer = 0f;
                shakeCount++;

                // 随机抖动位置
                float shakeX = Random.Range(-shakeAmount, shakeAmount);
                float shakeY = Random.Range(-shakeAmount, shakeAmount);
                transform.position = originalPosition + new Vector3(shakeX, shakeY, 0);

                // 检查是否已经完成指定次数的抖动
                if (shakeCount >= shakeTimes)
                {
                    transform.position = originalPosition; // 恢复原位置
                    isShaking = false; // 停止抖动
                }
            }
        }
    }

    // 公开的Shake方法，让外部脚本调用
    public void Shake()
    {
        originalPosition = transform.position; // 记录初始位置
        shakeCount = 0; // 重置抖动次数
        shakeTimer = 0f; // 重置计时器
        isShaking = true; // 启动抖动状态
    }
}
