using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowForUi : MonoBehaviour
{
    public Action m_acOnDestroyObj;
    public Action m_acOnSDestroyObj;

    [SerializeField] 
    protected GameObject m_gmBlock;
    [HideInInspector]
    public Vector2 m_vcStartPos;


    public virtual void DellObj(bool skipAnim = false)
    {
        float times = skipAnim ? 0 : 1;
        m_acOnSDestroyObj?.Invoke();
        transform.DOLocalMove(m_vcStartPos, times).OnComplete(() =>
        {
            m_acOnDestroyObj?.Invoke();
            Destroy(gameObject);

        });

    }
    public virtual void Init()
    {
        m_gmBlock.SetActive(false);
    }
}
