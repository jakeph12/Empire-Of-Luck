using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class Main_Daily_Reward : WindowForUi
{
    [SerializeField]
    private GameObject m_gmListOfSots;
    [SerializeField]
    private Sprite m_spDone, m_SpClaim;

    private Button m_btMain;

    private int[] m_inPrice =
    {
        500,
        1000,
        2500,
        5000,
        15000,
        25000,
        100000,
        500000,
        1000000,
        5000000,
    };


    void Start()
    {
        var dateCurrentH = DateTime.Now.Hour;
        var dateCurrentD = DateTime.Now.Day;
        var dateCurrentM = DateTime.Now.Month;
        var curlvl = PlayerPrefs.GetInt("PlayerDailyR", 0);
        var dateAldH = PlayerPrefs.GetInt("PlayerH", dateCurrentH);
        var dateAldD = PlayerPrefs.GetInt("PlayerD", dateCurrentD -1);
        var dateAldM = PlayerPrefs.GetInt("PlayerM", dateCurrentM);

        if(curlvl != 0)
            for (int i = 0;i < curlvl;i++)
            {
                var t = i;
                m_gmListOfSots.transform.GetChild(t).GetComponent<Image>().sprite = m_spDone;
            }
        var rr = dateCurrentD - dateAldD;

        if (rr < 1) return;
        var bt = m_gmListOfSots.transform.GetChild(curlvl);

        bt.GetComponent<Image>().sprite = m_SpClaim;
       

        m_btMain = bt.GetComponent<Button>();

        m_btMain.interactable = true;
        m_btMain.onClick.AddListener(() =>
        {

            Player_Menager.m_inCoin += m_inPrice[curlvl];
            m_gmListOfSots.transform.GetChild(curlvl).GetComponent<Image>().sprite = m_spDone;
            curlvl++;
            m_gmListOfSots.transform.GetChild(curlvl).GetComponent<Image>().sprite = m_SpClaim;
            PlayerPrefs.SetInt("PlayerDailyR", curlvl);
            PlayerPrefs.SetInt("PlayerD", dateCurrentD);
            m_btMain.interactable = false;

        });



    }
}
