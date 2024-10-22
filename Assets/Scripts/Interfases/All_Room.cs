using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class All_Room
{
    private static List<Room> _m_lsAllIrRooms = new List<Room>();
    public static List<Room> m_lsAllIrRoom
    {
        get{
            Init();
            return _m_lsAllIrRooms;
        }
        private set
        {
            Init();
            m_lsAllIrRoom = value;
        }
    }
    private static bool m_bInit = false;

    private static void Init()
    {
        if (m_bInit) return;
        m_bInit = true;
        m_lsAllIrRoom.AddRange(
            Resources.LoadAll("Room/", typeof(Room))
            .Cast<Room>()
            .OrderBy(Island => Island.m_inVisitorsNeed)

            );
    }


}
