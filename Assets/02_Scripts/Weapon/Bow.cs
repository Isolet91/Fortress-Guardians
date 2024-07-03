using System.Runtime.CompilerServices;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public PlayerStat playerStat;      //플레이어 스텟 변수

    public GameObject Arrow;           //생성할 화살 프리팹
    public Transform SpawnArrow;       //화살 생성 위치
    public float Force;                //화살에 가해질 힘

    private float nextFire = 0.5F;     //다음 발사 시간
    private float myTime = 0.0F;       //경과한 시간

    [SerializeField]
    public bool isPlayer = false;      //이 오브젝트가 플레이어인가?

    void Update()
    {
        myTime += Time.deltaTime;     //경과 시간 증가

        // 경과 시간이 다음 발사 시간보다 크고, (플레이어가 마우스 왼쪽 버튼이 눌린 상태일 때)
        if (myTime > nextFire && (isPlayer ? Input.GetMouseButton(0) : true))
        {
            // 다음 발사 시간을 현재 시간 + 플레이어의 공격 속도로 설정
            nextFire = myTime + playerStat.speed;

            // 생성 위치에 화살 생성
            GameObject newArrow = Instantiate(Arrow, SpawnArrow.transform.position, transform.rotation);

            // 플레이어의 스탯에 있는 공격력으로 초기화
            newArrow.GetComponent<Arrow>().damage = playerStat.attack;

            // 화살의 크기를 조정
            newArrow.transform.localScale = new Vector2(0.4f, 0.4f);

            // 화살에 힘을 가해 발사
            newArrow.GetComponent<Rigidbody2D>().AddForce(newArrow.transform.right * Force);

            // 다음 발사 시간을 초기화
            nextFire -= myTime;

            // 화살 발사 소리 재생
            gameObject.GetComponent<AudioSource>().Play();

            // 경과 시간을 초기화
            myTime = 0.0F;
        }
    }
}
