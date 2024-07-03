    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float DammageCastle = 200f; //성에 입힐 데미지
    public float TimeLeft = 2f;        //공격 간격
    private float NewTimeLeft;         //초기 공격 간격 저장 변수

    void Start()
    {
        NewTimeLeft = TimeLeft; //공격 간격 초기화
    }

    void OnTriggerStay2D(Collider2D other)
    {
        //몬스터가 성에 도달하면(설정해놓은 히트포인트와 충돌하면)
        if (other.gameObject.tag == "CastleHitPoint")                      
        {
            //공격 간격을 시간에 따라 감소시킴
            TimeLeft -= Time.deltaTime;                                           

            //공격 간격이 0초 이하가 되면
            if(TimeLeft <= 0)
            {
                //성의 체력 감소
                other.GetComponent<CastleHealth>().HealthCastle -= DammageCastle;
                //적의 이동 속도를 0으로 설정해서 멈춤
                gameObject.GetComponent<AI>().Speed = 0f;
                //다음 공격 간격 초기화
                TimeLeft = NewTimeLeft;                                           
            }
        }
    }
}
