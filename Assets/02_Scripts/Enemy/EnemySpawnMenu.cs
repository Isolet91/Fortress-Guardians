using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnMenu : MonoBehaviour {

    public GameObject[] Enemy;                 // 생성할 적의 프리팹들을 저장하는 배열
    public Transform SpawnPoint;               // 적이 생성될 위치
     
    public float TimerMin = 5f;                // 최소 생성 주기
    public float TimerMax = 10f;               // 최대 생성 주기

    private float Timer = 5;                   // 초기 생성 주기

    private float nextTime = 0.5F;             // 다음 생성 시간
    private float myTime = 0.0F;               // 경과 시간


    void Update()
    {

        myTime = myTime + Time.deltaTime; //경과 시간 누적
        if (myTime > nextTime)
        {
            Timer = Random.Range(TimerMin, TimerMax);   //주기 랜덤 설정
            nextTime = myTime + Timer;                  //다음 생성 시간 설정
         
            // 랜덤한 인덱스를 이용하여 Enemy 배열에서 적 프리팹을 선택하여 SpawnPoint 위치에 생성
            Instantiate(Enemy[Random.Range(0, 4)], 
            new Vector2(SpawnPoint.transform.position.x, SpawnPoint.transform.position.y), Quaternion.identity);          
            nextTime = nextTime - myTime;  //다음 생성 시간 계산
            myTime = 0.0F;                 //경과 시간 초기화
        }
    }
}
