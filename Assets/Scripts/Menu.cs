using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    void Update()
    {
        // 检测是否按下ESC键
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 调用退出游戏的函数
            Exit();
        }
    }

    // 退出游戏的函数
    public void Exit()
    {
        // 如果是在编辑模式中，停止播放
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 如果是在构建版本中，退出游戏
        Application.Quit();
#endif
    }

    public void ReturnToStart()
    {
        SceneManager.LoadScene("Start");
    }
}
