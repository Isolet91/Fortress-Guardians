using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centr : MonoBehaviour
{

    // 마우스와 객체 사이의 거리
    public float distance = 10f;

    void Start()
    {
       
    }

    void Update()
    {
        // 마우스의 화면 위치를 world 좌표로 변환하여 객체 위치로 설정하는 코드

        // 현재 마우스의 화면 좌표를 Vector3로 저장
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);

        // Camera.main.ScreenToWorldPoint 함수를 사용하여 화면 좌표를 world 좌표로 변환
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // 객체의 위치를 변환된 world 좌표로 설정
        transform.position = objPosition;
    }
}