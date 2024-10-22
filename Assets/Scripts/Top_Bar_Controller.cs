using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Top_Bar_Controller : WindowForUi
{
    [SerializeField] private Text m_txCoin;
    public void Start()
    {
        if (m_txCoin)
        {
            OnCCh(Player_Menager.m_inCoin);
            Player_Menager.m_evOnCoinChg += OnCCh;
            
        }
    }
    private void OnCCh(int i)
    {
        m_txCoin.text = i.ToString();
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
    private void OnDestroy()
    {
        if(m_txCoin)
            Player_Menager.m_evOnCoinChg -= OnCCh;
    }
}
