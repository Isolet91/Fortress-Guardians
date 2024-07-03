using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    public GameObject[] Enemy;        //적 오브젝트 배열
    public Transform SpawnPoint;      //적 스폰 위치

    public float TimerMin = 2f;       //최소 생성 간격
    public float TimerMax = 5f;      //최대 생성 간격

    private float Timer = 5;          //현재 생성 간격

    private float nextTime = 0.5F;    //다음 생성 시간
    private float myTime = 0.0F;      //경과 시간

    public int enemyCount = 0;        //생성된 적의 수


    void Update () {
        myTime = myTime + Time.deltaTime;   //경과 시간 업데이트

        //다음 생성 시간이 되고 , 현재 웨이브가 진행 중 + 남은 적 수가 웨이브에 설정된 적 수보다 적을 때
        if(myTime > nextTime && ResourcesCastle.instance.isWave && ResourcesCastle.instance.stageEnemyCount[ResourcesCastle.instance.Wave-1] > enemyCount)
        {
            Timer = Random.Range(TimerMin, TimerMax); //랜덤한 생성 간격 설정
            nextTime = myTime + Timer;                //다음 생성 시간 설정


            //웨이브에 따라 배열에 담아놓은 적 생성
            if(GetComponent<ResourcesCastle>().Wave == 1)
            {
                EnemySpawnPunc(Enemy[0], new Vector2(SpawnPoint.transform.position.x, SpawnPoint.transform.position.y));
            }
            if (GetComponent<ResourcesCastle>().Wave == 2)
            {
                EnemySpawnPunc(Enemy[Random.Range(0, 2)], new Vector2(SpawnPoint.transform.position.x, SpawnPoint.transform.position.y));
            }
            if (GetComponent<ResourcesCastle>().Wave == 3)
            {
                EnemySpawnPunc(Enemy[Random.Range(0, 3)], new Vector2(SpawnPoint.transform.position.x, SpawnPoint.transform.position.y));
            }
            if (GetComponent<ResourcesCastle>().Wave == 4)
            {
                EnemySpawnPunc(Enemy[Random.Range(0, 4)], new Vector2(SpawnPoint.transform.position.x, SpawnPoint.transform.position.y));
            }
            if (GetComponent<ResourcesCastle>().Wave == 5)
            {
                EnemySpawnPunc(Enemy[Random.Range(0, 5)], new Vector2(SpawnPoint.transform.position.x, SpawnPoint.transform.position.y));
            }

            enemyCount++;                       //생성된 적 수 증가
            nextTime = nextTime - myTime;       //다음 생성 시간 계산
            myTime = 0.0F;                      //생성 경과 시간 초기화
        }
    }

    public void EnemySpawnPunc(GameObject enemyPrefab, Vector2 spawnPoint)
    {
        //적을 생성하고 저장
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);

        // 생성된 적의 타입에 따라 위치를 조정  (문제점 해결 부분)
        switch (enemy.GetComponent<AI>().enemyType)               
        {
            case EnemyType.Normal:
                break;
            case EnemyType.Flying: //비행하는 적의 경우 높은 곳에서 생성
                enemy.transform.position = new Vector2(spawnPoint.x, spawnPoint.y + 4.0f);
                break;
        }
    }
}