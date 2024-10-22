using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;

public class Main_Tutor : WindowForUi
{
    [SerializeField]
    private Text m_txName, m_txDescr, m_txBt;
    [SerializeField]
    private Image m_mgMain;
    [SerializeField]
    private Sprite[] m_spMain;
    [SerializeField]
    private GameObject m_gmStartPos;


    private string[] m_strDescr =
    {
        "Play fun mini-games like Scratch Mania to earn coins. Use these coins to unlock new casino floors, purchase upgrades, and boost your visitor count!",
        "Upgrading your casino attracts more visitors! Each new improvement adds a unique feature to your casino, drawing more guests and increasing your revenue.",
        "Collect enough coins and visitors to unlock new, exciting casino levels. Expand your empire, and become the ultimate casino tycoon!",
    };

    private string[] m_strName =
    {
        "Earn Coins in Mini-Games",
        "Attract More Visitors",
        "Unlock New Levels",
    };

    void Start()
    {
        
    }
    private bool m_bPlay;
    private int m_inIndex = 0;
    public void Next()
    {
        if (m_bPlay) return;
        if (m_inIndex == m_strDescr.Length - 1)
        {
            DellObj();
            return;
        }

        if(m_inIndex == m_strDescr.Length - 2)
        {
            m_txBt.text = "LET’S GO";
        }
        else
        {
            m_txBt.text = "Continue";
        }

        var New = Instantiate(m_mgMain, m_mgMain.transform.parent.transform);

        New.GetComponent<Image>().sprite = m_spMain[m_inIndex];

        New.transform.localPosition = m_gmStartPos.transform.localPosition;

        New.transform.DOLocalMove(m_mgMain.transform.localPosition,1).OnComplete(() => 
        { 
            Destroy(m_mgMain);
            m_mgMain = New;
        });
        m_mgMain.transform.DOLocalMove(New.transform.localPosition,1).OnComplete(() => m_bPlay = false);
        GenerateTextAsy(m_strDescr[m_inIndex], m_txDescr, 0.5f).Forget();
        GenerateTextAsy(m_strName[m_inIndex], m_txName, 0.5f).Forget();

        m_inIndex++;
        m_bPlay = true;
    }





    public static async UniTask GenerateTextAsy(string st,Text tx,float second)
    {
        var c = st.ToCharArray();

        int t = (int)((second * 1000) / c.Length);

        tx.text = "";

        foreach (char cr in c)
        {

            tx.text += cr;
            await UniTask.Delay(t);

        }

    }


}
