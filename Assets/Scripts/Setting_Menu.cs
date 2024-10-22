using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting_Menu : WindowForUi
{
    public void OpenOther(WindowForUi ws)
    {

        var n = Main_Menu_Controller.m_sinThis.OpenWindow(ws, TypeWindow.NoClosed);
        m_gmBlock.SetActive(true);

        //Bottom_Menu_controller.m_sinThis.Hide(false);
        n.GetComponent<WindowForUi>().m_acOnSDestroyObj += () =>
        {
           // Bottom_Menu_controller.m_sinThis.Hide(true);
            m_gmBlock.SetActive(false);

        };

    }
}
