using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_answer : WindowForUi
{

    [SerializeField]
    private Text m_txCoin;
    [SerializeField]
    private Button m_btHome,m_btRestart;
    [SerializeField]
    private Image m_imMain;
    [SerializeField]
    private Sprite m_spLose;


    public void Inits(bool loose, Action OnHome, Action OnRestart, int coin = 0)
    {
        if (loose)
        {
            m_txCoin.transform.parent.gameObject.SetActive(false);
            m_imMain.sprite = m_spLose;
        }
        else
        {
            m_txCoin.text = coin.ToString();

        }

        m_btHome.onClick.AddListener(() => 
        {
            OnHome?.Invoke();
            DellObj();
        });
        m_btRestart.onClick.AddListener(() => 
        {
            OnRestart?.Invoke();
            DellObj();
        });
    }

}
