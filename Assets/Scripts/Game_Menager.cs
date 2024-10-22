using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Menager : WindowForUi
{
   public void Open(WindowForUi wi)
   {
        var t = Main_Menu_Controller.m_sinThis.OpenWindow(wi, TypeWindow.NoClosed).GetComponent<WindowForUi>();
        Bottom_Menu_controller.m_sinThis.Hide(false);
        t.m_acOnSDestroyObj += () => Bottom_Menu_controller.m_sinThis.Hide(true);
    } 
}
