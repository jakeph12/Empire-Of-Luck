using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TypeWindow
{
    Main,
    Additional,
    NoClosed,
}

public class Main_Menu_Controller : MonoBehaviour
{
    public static Main_Menu_Controller m_sinThis; 


    [SerializeField]
    private GameObject m_gmMainPanel,m_gmAddintionPanel,m_gmNoClosedPanel;
    private GameObject m_gmCurMain,m_gmCurAdditional;
    [SerializeField]
    private WindowForUi m_gmMainMenu;


    public void Awake()
    {
        m_sinThis = this;
    }

    public void Start()
    {
        OpenWindow(m_gmMainMenu,TypeWindow.Main,true);
    }

    public GameObject OpenWindow(WindowForUi gm,TypeWindow typeW,bool skipAnim = false)
    {
        GameObject NewObj;
        float time = skipAnim ? 0 : 1;

        switch (typeW)
        {
            case TypeWindow.Main:
                if(m_gmCurMain)
                    m_gmCurMain.GetComponent<WindowForUi>().DellObj();
                NewObj = Instantiate(gm.gameObject, m_gmMainPanel.transform);
                m_gmCurMain = NewObj;
                break;

            case TypeWindow.Additional:
                if (m_gmCurAdditional)
                    m_gmCurAdditional.GetComponent<WindowForUi>().DellObj();
                NewObj = Instantiate(gm.gameObject, m_gmAddintionPanel.transform);
                m_gmCurAdditional = NewObj;
                break;

            case TypeWindow.NoClosed:
                NewObj = Instantiate(gm.gameObject, m_gmNoClosedPanel.transform);
                break;
            default:
                NewObj = Instantiate(gm.gameObject, m_gmCurMain.transform);
                break;

        }

        NewObj.GetComponent<WindowForUi>().m_vcStartPos = NewObj.transform.localPosition;
        NewObj.transform.DOLocalMove(Vector2.zero, time).OnComplete(()=> 
        {
            NewObj.GetComponent<WindowForUi>().Init();
        });

        return NewObj;
    }
    public void CloseAll(TypeWindow typeW)
    {

        switch (typeW)
        {
            case TypeWindow.Main:
                if (m_gmCurMain)
                    m_gmCurMain.GetComponent<WindowForUi>().DellObj();
                break;

            case TypeWindow.Additional:
                if (m_gmCurAdditional)
                    m_gmCurAdditional.GetComponent<WindowForUi>().DellObj();
                break;

            case TypeWindow.NoClosed:
                var c = m_gmNoClosedPanel.transform.childCount;

                if (c > 0)
                    for(int i = 0;i < c; i++)
                        m_gmNoClosedPanel.GetComponentInChildren<WindowForUi>().DellObj();

                break;
            default:
                break;

        }
    }

    public static void OpenMainMenu(bool skeepAnim = false) => m_sinThis.OpenWindow(m_sinThis.m_gmMainMenu,TypeWindow.Main,skeepAnim);

}
