using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float scrollSpeed = 5f; // 鼠标滚轮控制相机移动的速度，可在Unity编辑器中调整
    public float minY = 1f; // 摄像机Y轴坐标下限，可在Unity编辑器中调整
    public float maxY = 24f; // 摄像机Y轴坐标上限，可在Unity编辑器中调整

    void CameraConcle()
    {
        // 获取鼠标滚轮的滚动值，该值为正表示向前滚动（向上），为负表示向后滚动（向下）
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");

        // 根据滚动值计算相机在Y轴方向上的偏移量
        Vector3 moveDir = new Vector3(0, scrollDelta, 0) * scrollSpeed;

        // 临时计算移动后的相机位置
        Vector3 newPosition = transform.position + moveDir;

        // 检查并限制Y轴坐标在设定范围内
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // 更新相机的位置
        transform.position = newPosition;
    }

    void Update()
    {
        CameraConcle();
    }
}
