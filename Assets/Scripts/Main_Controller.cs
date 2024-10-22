using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Player_Menager;

public class Main_Controller : WindowForUi
{
    [SerializeField]
    private WindowForUi m_wiTutor,m_wiDaly,m_wiSettings,m_wiGame_Manager, m_wiUpgrade;
    [SerializeField]
    private Text m_txVisitors, m_txUpgrades,m_txCoinNeed,m_txMain,m_txDescr;
    [SerializeField]
    private Slider m_slUp;
    [SerializeField]
    private Image m_imMain;
    [SerializeField]
    private Button m_btMainUp;

    void Start()
    {
        var P = PlayerPrefs.GetInt("TutorP", 0);
        if (P == 0)
        {

            var g = Main_Menu_Controller.m_sinThis.OpenWindow(m_wiTutor,TypeWindow.NoClosed);
            PlayerPrefs.SetInt("TutorP", 1);
            Bottom_Menu_controller.m_sinThis.Hide(false);
            g.GetComponent<WindowForUi>().m_acOnSDestroyObj += () => Bottom_Menu_controller.m_sinThis.Hide(true);

        }

        Bottom_Menu_controller.m_evChangePage += OnPageCh;
        m_acOnDestroyObj += () =>
        {
            Bottom_Menu_controller.m_evChangePage -= OnPageCh;
        };

        if (m_btMainUp)
        {
            m_btMainUp.onClick.AddListener(UpgradeS);
        }

        if (m_imMain)
        {
            OnRoomCh(m_airCurIRoom);
            m_evOnRoomChg += OnRoomCh;
            m_acOnDestroyObj += () => m_evOnRoomChg -= OnRoomCh;
        }
        if (m_txVisitors)
        {

            OnVisitorsCh(m_inVisitors);
            m_evOnVisitorsChg += OnVisitorsCh;
            m_acOnDestroyObj += () => m_evOnVisitorsChg -= OnVisitorsCh;


        }
        if (m_txUpgrades)
        {
            onUpgradeCh(m_inAlreadyBought);
            m_evOnAlreadyBoughtChg += onUpgradeCh;
            m_acOnDestroyObj += () => m_evOnAlreadyBoughtChg -= onUpgradeCh;
        }
        if (m_txCoinNeed)
        {
            OnCoinCh(m_inCoin);
            m_evOnCoinChg += OnCoinCh;
            m_acOnDestroyObj += () => m_evOnCoinChg -= OnCoinCh;
        }

    }

    public void OnRoomCh(Room si)
    {
        m_imMain.sprite = si.m_spRoomIco;
        if (m_txMain)
        {
            m_txMain.text = si.name.Split('.')[1];
        }
        if (m_txDescr)
        {
            m_txDescr.text = si.m_strDescr;
        }
        if(m_slUp && All_Room.m_lsAllIrRoom.Count > m_inCurLvl + 1)
        {
            m_slUp.maxValue = All_Room.m_lsAllIrRoom[m_inCurLvl + 1].m_inCoinNeed;
        }
        else
        {
            m_btMainUp.interactable = false;
            OnVisitorsCh(m_inVisitors);
            OnCoinCh(m_inCoin);
        }
    }


    public void onUpgradeCh(int index)
    {
        m_txUpgrades.text = $"{index}/5";
    }
    public void OnVisitorsCh(int key)
    {
        if (All_Room.m_lsAllIrRoom.Count > m_inCurLvl + 1)
        {
            var NewRomm = All_Room.m_lsAllIrRoom[m_inCurLvl + 1];
            m_txVisitors.text = $"{key}/{NewRomm.m_inVisitorsNeed}";
            if (key >= NewRomm.m_inVisitorsNeed && m_inCoin >= NewRomm.m_inCoinNeed)
            {
                m_btMainUp.interactable = true;
            }
        }
        else
        {
            m_txVisitors.text = $"{key}/Max";
        }

    }
    private void UpgradeS()
    {
        if (All_Room.m_lsAllIrRoom.Count > m_inCurLvl + 1)
        {
            var New = All_Room.m_lsAllIrRoom[m_inCurLvl + 1];
            if (m_inVisitors >= New.m_inVisitorsNeed && m_inCoin >= New.m_inCoinNeed)
            {
                m_inCoin -= New.m_inCoinNeed;
                m_inCurLvl++;
                m_inIdOfRoom++;
                m_btMainUp.interactable = false;
            }
        }
    }
    public void OnCoinCh(int coin)
    {
        if (All_Room.m_lsAllIrRoom.Count > m_inCurLvl + 1)
        {
            var NewRomm = All_Room.m_lsAllIrRoom[m_inCurLvl + 1];
            m_txCoinNeed.text = $"{coin}/{All_Room.m_lsAllIrRoom[m_inCurLvl + 1].m_inCoinNeed}";
            m_slUp.value = coin;
            if (m_inVisitors >= NewRomm.m_inVisitorsNeed && coin >= NewRomm.m_inCoinNeed)
            {
                m_btMainUp.interactable = true;
            }
        }
        else
        {
            m_txCoinNeed.text = $"Max";
            m_slUp.value = m_slUp.maxValue;
        }
    }

    public void OpenUp(WindowForUi ws)
    {
        var n = Main_Menu_Controller.m_sinThis.OpenWindow(ws, TypeWindow.NoClosed);
        m_gmBlock.SetActive(true);

        Bottom_Menu_controller.m_sinThis.Hide(false);
        n.GetComponent<WindowForUi>().m_acOnSDestroyObj += () =>
        {
            Bottom_Menu_controller.m_sinThis.Hide(true);
            m_gmBlock.SetActive(false);

        };
    }
    public void OnPageCh(int ch)
    {

        Main_Menu_Controller.m_sinThis.CloseAll(TypeWindow.Additional);
        Main_Menu_Controller.m_sinThis.CloseAll(TypeWindow.NoClosed);
        switch (ch)
        {
            case 0:
                break;
            case 1:
                Main_Menu_Controller.m_sinThis.OpenWindow(m_wiUpgrade, TypeWindow.Additional);

                break;
            case 2:
                Main_Menu_Controller.m_sinThis.OpenWindow(m_wiGame_Manager, TypeWindow.Additional);
                break;

            case 3:
                Main_Menu_Controller.m_sinThis.OpenWindow(m_wiDaly, TypeWindow.Additional);
                break;
            case 4:
                Main_Menu_Controller.m_sinThis.OpenWindow(m_wiSettings, TypeWindow.Additional);
                break;
        }
    }
}
