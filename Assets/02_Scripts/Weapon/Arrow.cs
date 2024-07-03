using UnityEngine;

public class Arrow : MonoBehaviour
{
    public bool fly = true;          //화살이 날아가는지 여부
    public float damage = 20f;       // 화살의 데미지
    public static Arrow instance;    // 싱글톤 인스턴스

    private void Start()
    {
        //비어 있으면 현재 인스턴스를 할당
        if (instance == null)
        {
            instance = this;
        }
    }
    //Update는 매 프레임 호출되고, FixedUpdate는 고정된 시간 간격으로 호출됨(물리 연산 및 리지드바디 관련 코드를 배치하는 데 유용)
    void FixedUpdate()
    {
        
        ArrowEnd();                  //화살의 비행 상태를 확인하고 끝내는 메서드 호출
        ArrowRot();                  // 화살의 회전 처리 메서드 호출
        Destroy(gameObject, 4);      //4초후에 화살 파괴
    }

    void ArrowRot() //화살의 회전 처리 메서드
    {
        if (GetComponent<Rigidbody2D>() != null)                                 //리지드바디 컴포넌트가 있으면
        {
            Vector2 v = GetComponent<Rigidbody2D>().velocity;                    //화살의 속도를 가져옴
            var angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;                   //속도 벡터의 각도를 계산해서 회전 각도로 변환
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);   //계산된 각도로 화살의 회전을 설정
        }
    }

    void ArrowEnd() //화살의 비행 상태를 확인하고 끝내는 메서드
    {
        if (fly == false)                                    //화살이 다 날아갔으면
        {
            Destroy(gameObject.GetComponent<Rigidbody2D>()); //리지드바디 파괴
            Destroy(gameObject.GetComponent<Collider2D>());  //콜라이더 파괴
        }
    }

    void OnTriggerEnter2D(Collider2D other) //다른 콜라이더와 충돌할 때 호출
    {
        //아군이랑 화살이 충돌하면
        if (other.gameObject.tag == "Arrow" || other.gameObject.tag == "Player" || other.gameObject.tag == "Friend" || other.gameObject.tag == "Castle" || other.gameObject.tag == "CastleHitPoint")
        {
            //충돌 무시
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.transform.GetComponent<Collider2D>());
        }
        else
        {
            fly = false;
        }
        //적과 충돌하면
        if (other.gameObject.tag == "Enemy")
        {
            //화살을 파괴
            Destroy(gameObject);
            //적에게 데미지를 줌
            other.gameObject.GetComponent<HealthEnemy>().HitEnemy(damage / 3.01f);
        }
    }
}
