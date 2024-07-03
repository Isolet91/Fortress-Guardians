using UnityEngine;

public class BowAi : MonoBehaviour
{
  //���� �ڵ� ���� ��ũ��Ʈ
    
    public PlayerStat playerStat;       // �÷��̾� ���� ���� ����

    public GameObject Arrow;            // ȭ�� ������
    public Transform SpawnArrow;        // ȭ���� ������ ��ġ
    public float Force;                 // ȭ�쿡 ������ ��

    private float nextFire = 0.5F;      // ���� ���� ���� �ð�
    private float myTime = 0.0F;        // ��� �ð�

    void Update()
    {

        myTime = myTime + Time.deltaTime;     //��� �ð� ����

        //���� ���� ���� �ð��� �Ǿ���, ������ �� �ִ� ���¶��
        if (myTime > nextFire)
        {
                //���� ���� ���� �ð� ����
                nextFire = myTime + playerStat.speed;

                //ȭ���� SpawnArrow ��ġ�� PlayerStat�� �������� ����
                GameObject newArrow = Instantiate(Arrow, SpawnArrow.transform.position, transform.rotation);

                // ������ ȭ���� �������� �÷��̾��� ���ȿ� �ִ� ���ݷ����� �ʱ�ȭ
                newArrow.GetComponent<Arrow>().damage = playerStat.attack;

                //ȭ���� ũ�� ����
                newArrow.transform.localScale = new Vector2(0.4f, 0.4f);
                //ȭ�쿡 ���� ���Ͽ� �߻�
                newArrow.GetComponent<Rigidbody2D>().AddForce(newArrow.transform.right * Force);
                
                //���� �� ó��
                nextFire = nextFire - myTime;                     //���� ���� ���� �ð� ���
                gameObject.GetComponent<AudioSource>().Play();    //Ȱ �߻� �Ҹ� ���
                myTime = 0.0F;                                    //��� �ð� �ʱ�ȭ
            

        }
    }
}
