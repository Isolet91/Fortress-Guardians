using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class BuyArmy : MonoBehaviour
{
    public GameObject ArmyPrefab;   // 동료 프리팹
    
    public int Price = 500;         // 구매 가격


    
    public void BuyMyArmy() //동료 구매 함수
    {
        GameObject Res = GameObject.Find("Main Camera");

        //골드가 충분하면 구매 진행
        if (Res.GetComponent<ResourcesCastle>().Coins >= Price)
        {
            //골드 차감
            Res.GetComponent<ResourcesCastle>().Coins -= Price;

            //동료 생성
            GameObject friend = Instantiate(ArmyPrefab, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
            
            // 동료 오브젝트에서 자식 오브젝트의 Bow, Button 스크립트를 찾아줌
            Button LevelUpButton = friend.transform.GetChild(1).GetChild(0).GetComponent<Button>();
            Bow bow = friend.transform.GetChild(0).GetChild(0).GetComponent<Bow>();

            /*  람다식 활용 전 코드
            void OnLevelUpButtonClicked() //델리게이트 정의
            {
                StatUpgrade.instance.ActiveUpgradePanel(bow);
            }

            // 동료 생성 시 업그레이드 패널 생성
            LevelUpButton.onClick.AddListener(OnLevelUpButtonClicked);
            */

            // 람다식 사용 코드
            LevelUpButton.onClick.AddListener(() => StatUpgrade.instance.ActiveUpgradePanel(bow));

            // 동료가 생성되서 화살 발사할 때도 발사 효과음이 날 수 있게 Bow 스크립트에 있는 오디오소스를 넣어줌
            GameManager.instance.arrowSounds.Add(bow.gameObject.GetComponent<AudioSource>());

            //동료 구매 버튼 파괴
            Destroy(gameObject); 
        }
    }
}
