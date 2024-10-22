using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Slots_Main : MonoBehaviour
{
    [SerializeField]
    private GameObject m_gmBlock;
    [HideInInspector]
    public Game_Firsts m_scrMain;
    [SerializeField]
    private List<Game_Slots_Items> m_items = new List<Game_Slots_Items>();




    public void RandomSpawn()
    {
        if (!m_scrMain) return;
        m_gmBlock.SetActive(false);
        var p = -1;
        for (int i =0; i < m_items.Count;i++)
        {
            var r = Random.Range(0,m_scrMain.m_splAll.Count);
            if (p == r)
            {
                i--;
                continue;
            }
            p = r;
            m_items[i].SetItem(r, m_scrMain.m_splAll[r]);
        }
    }
    public void ChSlots()
    {
        int i = m_items[0].m_inId;
        int c = 0;
        foreach(var item in m_items)
            if (i == item.m_inId) c++;

        if(c >= 3)
        {
            m_scrMain.m_inScore += 100;
            var s = m_items[0].transform.localPosition;
            var l = m_items[2].transform.localPosition;
            m_gmBlock.SetActive(true);
            m_items[0].transform.DOLocalMoveX(m_items[1].transform.localPosition.x, 0.25f).OnComplete(() => m_items[0].transform.localPosition = s);
            m_items[2].transform.DOLocalMoveX(m_items[1].transform.localPosition.x, 0.25f).OnComplete(() => 
            {
                m_items[2].transform.localPosition = l;
                RandomSpawn();
                m_gmBlock.SetActive(false);

            });
        }
    }
}
