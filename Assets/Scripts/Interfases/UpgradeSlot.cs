using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSlot : MonoBehaviour
{
    [SerializeField]
    private Text m_txName,m_txDescr,m_txCoinPrise;
    public GameObject m_btBuy;


    public void SetLabel(string Name = "",string Descr = "", string Price = "")
    {
        if(Name != "")
            m_txName.text = Name;
        if(Descr != "")
            m_txDescr.text = Descr;
        if(Price != "")
        {
            m_txCoinPrise.text = Price;
            m_txCoinPrise.transform.parent.gameObject.SetActive(true);

        }
        else
        {
            m_txCoinPrise.transform.parent.gameObject.SetActive(false);
        }
        
    }

}
