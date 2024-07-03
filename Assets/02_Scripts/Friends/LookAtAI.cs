using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LookAtAI : MonoBehaviour
    //동료들이 몬스터 타겟을 찾아서 바라보도록 하는 함수 
{

    
    public Transform Target;                   //타겟 지정 Transform 변수
    
    private GameObject MyOneTarget;            //현재 선택된 하나의 타겟
    
    private GameObject[] MyTargets;            //모든 타겟 배열

    // 객체가 바라보는 방향을 정의하는 열거형
    enum FacingDirection
    {
        UP = 270,
        DOWN = 90,
        LEFT = 180,
        RIGHT = 0
    }

    void Update()
    {
       
        DistanceObjects();
    }

    void DistanceObjects()
    {
        // "Enemy" 태그를 가진 모든 게임 오브젝트를 찾음
        MyTargets = GameObject.FindGameObjectsWithTag("Enemy");

        // 찾은 모든 타겟 중 첫 번째 타겟을 선택
        for (var i = 0; i < MyTargets.Length; i += 1)
        {
            MyOneTarget = MyTargets[0];
            Target = MyOneTarget.transform;
        }

        // 타겟이 존재할 경우 LookAt2D 함수를 호출하여 타겟을 바라보도록 함
        if (Target != null)
        {
            LookAt2D(Target, 17f, FacingDirection.RIGHT);
        }
    }

    // 2D 객체가 타겟을 바라보도록 하는 함수
    void LookAt2D(Transform theTarget, float theSpeed, FacingDirection facing)
    {
        // 타겟과 객체 사이의 벡터를 계산
        Vector3 vectorToTarget = theTarget.position - transform.position;

        // 벡터를 각도로 변환 (라디안 -> 도)
        float angle = Mathf.Atan2(vectorToTarget.y + 30, vectorToTarget.x) * Mathf.Rad2Deg;

        // 객체의 바라보는 방향을 고려하여 각도를 조정
        angle -= (float)facing;

        // 새로운 회전값을 계산
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

        // 객체의 회전을 점진적으로 변경하여 타겟을 바라보게 함
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * theSpeed);
    }
}