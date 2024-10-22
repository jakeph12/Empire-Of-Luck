using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Room", menuName = "Game/New Object/Room")]
public class Room : ScriptableObject, IsRoomInfo
{
    [SerializeField]
    private Sprite m_spMainIco;
    public int m_inVisitorsNeed;
    public int m_inCoinNeed;

    [SerializeField]
    private string m_strDescript;
    public List<RoomUpgrade> m_scUpgrades = new List<RoomUpgrade>();




    public Sprite m_spRoomIco { get => m_spMainIco; set => m_spMainIco = value; }
    public string m_strDescr { get=> m_strDescript; set => m_strDescript = value; }
}
public interface IsRoomInfo
{
    public Sprite m_spRoomIco { get; set; }
    public string m_strDescr { get; set; }
}

[System.Serializable]
public class RoomUpgrade
{
    public string m_strName, m_strDescript;
    public int m_inVisitorIncrease;
    public int m_inCost;
}