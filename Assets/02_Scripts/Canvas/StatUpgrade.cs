using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;

// 플레이어의 스탯 구조체
[Serializable]
public struct PlayerStat
{
    public float attack;         //공격력
    public float speed;          //공속
    public float attackGold;     //공격력 업그레이드 비용
    public float speedGold;      //공속 업그레이드 비용
}

public class StatUpgrade : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static StatUpgrade instance;

    [SerializeField]
    private GameObject statPanel;
    [SerializeField]
    private TextMeshProUGUI attackText;
    [SerializeField]
    private TextMeshProUGUI speedText;
    [SerializeField]
    private TextMeshProUGUI attackGoldText;
    [SerializeField]
    private TextMeshProUGUI speedGoldText;

    // 현재 선택되어 있는 캐릭터의 Bow 스크립트
    private Bow currentBow;

    private void Awake()
    {
        // 이미 인스턴스가 존재하는지 확인하고 없으면 현재 인스턴스를 할당
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        // 업그레이드 패널이 활성화되어 있고 currentBow가 null이 아닌 경우 스탯 정보를 업데이트
        if (statPanel.activeSelf && currentBow != null)
        {
            attackText.text = $"Attack : {currentBow.playerStat.attack}";
            speedText.text = $"Speed : {currentBow.playerStat.speed.ToString("F3")}";
            attackGoldText.text = $"$ :{currentBow.playerStat.attackGold}";
            speedGoldText.text = $"$ :{currentBow.playerStat.speedGold}";
        }
    }

    public void UpgradeAttack()  //공격력 업그레이드 메서드
    {
            //코인이 충분하다면 업그레이드
        if (ResourcesCastle.instance.Coins >= currentBow.playerStat.attackGold)
        {
            // 현재 플레이어의 데미지 증가 비용 증가 & 현재 코인 감소
            currentBow.playerStat.attackGold += 10;
            ResourcesCastle.instance.Coins -= currentBow.playerStat.attackGold;

            // 현재 플레이어의 데미지 증가
            currentBow.playerStat.attack++;
        }
    }

    public void UpgradeSpeed () //공속 업그레이드
    {
        if (ResourcesCastle.instance.Coins >= currentBow.playerStat.speedGold && currentBow.playerStat.speed > 0.1f)
        {
            // 현재 플레이어의 공속 증가 비용 증가 & 현재 코인 감소
            currentBow.playerStat.speedGold += 10;
            ResourcesCastle.instance.Coins -= currentBow.playerStat.speedGold;

            // 현재 플레이어의 공속 증가
            currentBow.playerStat.speed -= 0.05f;
        }
    }

    //업그레이드 패널 활성화 메서드
    public void ActiveUpgradePanel(Bow bow) 
    {
        statPanel.SetActive(true);          //업그레이드 패널을 활성화하고
        currentBow = bow;                   //업그레이드된 스텟을 저장한다
        Time.timeScale = 0;                 //게임 일시 정지
    }

    //게임 화면으로 돌아가기 메서드
    public void Back()
    {
        statPanel.SetActive(false);         //업그레이드 패널을 비활성화하고
        Time.timeScale = 1;                 //게임 재개
    }
}