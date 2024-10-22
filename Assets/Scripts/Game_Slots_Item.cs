using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Game_Slots_Items : MonoBehaviour, IDropHandler, IEndDragHandler,IDragHandler,IBeginDragHandler
{
    private CanvasGroup m_cvMain;
    private Vector2 m_vcStartPos;
    public Image m_imMain;
    private Game_Slots_Main m_scParent;
    private Transform m_trParent;
    private int m_inIdPos;

    public int m_inId = -1;


    public void Awake()
    {
        m_cvMain = GetComponent<CanvasGroup>();
        m_vcStartPos = transform.localPosition;
        m_imMain = GetComponent<Image>();
        m_scParent = transform.parent.GetComponent<Game_Slots_Main>();
        m_trParent = transform.parent.transform;
        m_inIdPos = transform.GetSiblingIndex();
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        if (m_scParent.m_scrMain.m_bStop)
        {
            OnEndDrag(eventData);
            return;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        var curI = m_inId;
        var curS = m_imMain.sprite;
        var gm = eventData.pointerDrag.GetComponent<Game_Slots_Items>();
        var drI = gm.m_inId;
        var drS = gm.m_imMain.sprite;
        SetItem(drI, drS);
        gm.SetItem(curI, curS);
        gm.m_scParent.ChSlots();
        m_scParent.ChSlots();

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(m_trParent);
        transform.localPosition = m_vcStartPos;
        transform.SetSiblingIndex(m_inIdPos);
        m_cvMain.interactable = true;
        m_cvMain.blocksRaycasts = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(m_trParent.parent.transform);
        m_cvMain.interactable = false;
        m_cvMain.blocksRaycasts = false;
    }
    public void SetItem(int id,Sprite sp)
    {
        m_imMain.sprite = sp;
        m_inId = id;
    }
}
