using UnityEngine;

public class BowAi : MonoBehaviour
{
  //동료 자동 공격 스크립트
    
    public PlayerStat playerStat;       // 플레이어 스텟 저장 변수

    public GameObject Arrow;            // 화살 프리팹
    public Transform SpawnArrow;        // 화살이 생성될 위치
    public float Force;                 // 화살에 가해질 힘

    private float nextFire = 0.5F;      // 다음 공격 가능 시간
    private float myTime = 0.0F;        // 경과 시간

    void Update()
    {

        myTime = myTime + Time.deltaTime;     //경과 시간 누적

        //다음 공격 가능 시간이 되었고, 공격할 수 있는 상태라면
        if (myTime > nextFire)
        {
                //다음 공격 가능 시간 설정
                nextFire = myTime + playerStat.speed;

                //화살을 SpawnArrow 위치에 PlayerStat의 방향으로 생성
                GameObject newArrow = Instantiate(Arrow, SpawnArrow.transform.position, transform.rotation);

                // 생성된 화살의 데미지를 플레이어의 스탯에 있는 공격력으로 초기화
                newArrow.GetComponent<Arrow>().damage = playerStat.attack;

                //화살의 크기 설정
                newArrow.transform.localScale = new Vector2(0.4f, 0.4f);
                //화살에 힘을 가하여 발사
                newArrow.GetComponent<Rigidbody2D>().AddForce(newArrow.transform.right * Force);
                
                //공격 후 처리
                nextFire = nextFire - myTime;                     //다음 공격 가능 시간 계산
                gameObject.GetComponent<AudioSource>().Play();    //활 발사 소리 재생
                myTime = 0.0F;                                    //경과 시간 초기화
            

        }
    }
}
