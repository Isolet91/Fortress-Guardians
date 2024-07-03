using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{

    // 타겟 오브젝트를 저장할 변수
    public Transform target;

    // 열거형을 정의하여 상수로 방향을 나타냄
    enum FacingDirection
    {
        UP = 270,    // 위쪽 방향 (270도)
        DOWN = 90,   // 아래쪽 방향 (90도)
        LEFT = 180,  // 왼쪽 방향 (180도)
        RIGHT = 0    // 오른쪽 방향 (0도)
    }

    void Update()
    {
        // 매 프레임마다 LookAt2D 메서드를 호출하여 타겟을 바라보도록 회전시킴
        LookAt2D(target, 17f, FacingDirection.RIGHT);
    }

    // 타겟을 바라보도록 회전시키는 메서드
    void LookAt2D(Transform theTarget, float theSpeed, FacingDirection facing)
    {
        // 타겟과 현재 오브젝트 사이의 벡터를 계산
        Vector3 vectorToTarget = theTarget.position - transform.position;

        // 벡터를 각도로 변환 (라디안을 각도로 변환)
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;

        // 기본 방향에서의 차이를 빼서 실제 각도를 계산
        angle -= (float)facing;

        // 회전할 각도를 Quaternion으로 변환
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

        // 현재 회전을 목표 회전으로 천천히 변화시킴 (보간)
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * theSpeed);
    }
}