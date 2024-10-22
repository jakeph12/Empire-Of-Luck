using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Game_Seconds : WindowForUi
{
    [SerializeField]
    private List<GameObject> m_gmAll = new List<GameObject>();
    [SerializeField]
    private Sprite m_spMain, m_spLose, m_spWin,m_spBlock;
    private List<GameObject> m_gmlLose = new List<GameObject>();
    [SerializeField]
    private WindowForUi m_wiAnswer;

    public void Start()
    {
        foreach (var gm in m_gmAll)
            gm.GetComponent<Button>().onClick.AddListener(() => Show(gm));

        Spawn(3, 8);
    }

    private void Spawn(int Block,int Lose)
    {
        m_gmlLose.Clear();
        m_gmlLose = new List<GameObject>();
        m_bPresed = false;

        foreach (var gm in m_gmAll)
        {
            gm.GetComponent<Image>().sprite = m_spMain;
            gm.GetComponent<Button>().interactable = true;
        }

        List<GameObject> cur = new List<GameObject>(m_gmAll);
        for(int i = 0; i < Block; i++)
        {
            var r = Random.Range(0,cur.Count);
            cur[r].GetComponent<Image>().sprite = m_spBlock;
            cur[r].GetComponent<Button>().interactable = false;
            cur.RemoveAt(r);
        }
        for (int i = 0; i < Lose; i++)
        {
            var r = Random.Range(0, cur.Count);
            m_gmlLose.Add(cur[r]);
            cur.RemoveAt(r);
        }
    }
    private bool m_bPresed = false;
    public void Show(GameObject i)
    {
        if(m_bPresed) return;
        m_bPresed = true;

        if (m_gmlLose.Contains(i))
        {
            i.GetComponent<Image>().sprite = m_spLose;
            Lose();
        }
        else
        {
            i.GetComponent<Image>().sprite = m_spWin;
            Win();
        }
    }

    public void Win()
    {
       var o = Main_Menu_Controller.m_sinThis.OpenWindow(m_wiAnswer,TypeWindow.NoClosed).GetComponent<Game_answer>();
        o.Inits(false, 
            () => {
                Player_Menager.m_inCoin += 1000;
                DellObj();
            }, 
            () =>
            {
                Spawn(3, 8);
                Player_Menager.m_inCoin += 1000;
            }, 1000);
    }

    public void Lose()
    {
        var o = Main_Menu_Controller.m_sinThis.OpenWindow(m_wiAnswer, TypeWindow.NoClosed).GetComponent<Game_answer>();
        o.Inits(true,() => DellObj(), () => Spawn(3, 8));
    }

}
