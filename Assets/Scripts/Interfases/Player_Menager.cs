using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Menager
{
    const string PlayerPropertyCoin = "Player_coin";
    const string PlayerPropertyVisitors = "Player_Vistors";
    const string PlayerPropertyRoom = "Player_curent_Room";
    const string PlayerPropertyRoomBought = "Player_bought_Room";
    const string PlayerPropertyRoomUpgradeBought = "Player_bought_upgrade_Room";

    private static int _m_inCoin;
    public static int m_inCoin
    {
        get
        {
            Init();
            return _m_inCoin;
        }
        set
        {
            Init();
            var t = value - m_inCoin;
            _m_inCoin = value;

            if(t != 0)
                PlayerPrefs.SetInt(PlayerPropertyCoin, _m_inCoin);

            m_evOnCoinChg?.Invoke(_m_inCoin);
        }
    }


    private static int _m_inVisitors;
    public static int m_inVisitors
    {
        get
        {
            Init();
            return _m_inVisitors;
        }
        set
        {
            Init();
            var t = value - m_inVisitors;
            _m_inVisitors = value;

            if (t != 0)
                PlayerPrefs.SetInt(PlayerPropertyVisitors, _m_inVisitors);

            m_evOnVisitorsChg?.Invoke(_m_inVisitors);
        }
    }


    private static int _m_inAlreadyBought;
    public static int m_inAlreadyBought
    {
        get 
        {
            Init();
            return _m_inAlreadyBought;
        } 
        set
        {
            Init();
            _m_inAlreadyBought = value;
            m_evOnAlreadyBoughtChg?.Invoke(_m_inAlreadyBought);
            PlayerPrefs.SetInt(PlayerPropertyRoomUpgradeBought + m_inIdOfRoom, _m_inAlreadyBought);


        }
    }


    private static int _m_inIdOfIRoom =-1;
    private static Room _m_airCurRoom;
    public static Room m_airCurIRoom
    {
        get{
            Init();
            return _m_airCurRoom;
        }
        private set
        {
            Init();
            _m_airCurRoom = value;
            m_evOnRoomChg?.Invoke(_m_airCurRoom);
        }
    }
    public static int m_inIdOfRoom
    {
        get
        {
          Init();
          return  _m_inIdOfIRoom;
        }
        set
        {
            Init();
            if (value >= All_Room.m_lsAllIrRoom.Count) return;

            var t = value - _m_inIdOfIRoom;
            _m_inIdOfIRoom = value;
            if(t != 0)
            {
                m_inAlreadyBought = PlayerPrefs.GetInt(PlayerPropertyRoomUpgradeBought + m_inIdOfRoom, 0);
                m_airCurIRoom = All_Room.m_lsAllIrRoom[_m_inIdOfIRoom];
                PlayerPrefs.SetInt(PlayerPropertyRoom, _m_inIdOfIRoom);
            }


        }
    }
    private static int _m_inCurLvl = -1;
    public static int m_inCurLvl
    {
        get
        {
            Init();
            return _m_inCurLvl;
        }
        set
        {
            Init();
            var t = value - _m_inCurLvl;
            _m_inCurLvl = value;
            if (_m_inCurLvl > All_Room.m_lsAllIrRoom.Count - 1) _m_inCurLvl = All_Room.m_lsAllIrRoom.Count - 1;
            PlayerPrefs.SetInt(PlayerPropertyRoomBought, _m_inCurLvl);

        }
    }


    public static event OnCoinCh m_evOnAlreadyBoughtChg;

    public static event OnCoinCh m_evOnVisitorsChg;

    public delegate void OnCoinCh(int coin);
    public static event OnCoinCh m_evOnCoinChg;

    public delegate void OnRoomCh(Room plane);
    public static event OnRoomCh m_evOnRoomChg;


    private static bool m_bInit = false;

    private static void Init()
    {
        if (m_bInit) return;
        m_bInit = true;
        m_inCoin = PlayerPrefs.GetInt(PlayerPropertyCoin, 0);
        m_inIdOfRoom = PlayerPrefs.GetInt(PlayerPropertyRoom, 0);
        m_inCurLvl = PlayerPrefs.GetInt(PlayerPropertyRoomBought, 0);
        m_inAlreadyBought = PlayerPrefs.GetInt(PlayerPropertyRoomUpgradeBought + m_inIdOfRoom, 0);
        m_inVisitors = PlayerPrefs.GetInt(PlayerPropertyVisitors, 0);


    }


}
