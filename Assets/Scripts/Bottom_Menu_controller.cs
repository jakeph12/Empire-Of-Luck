using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bottom_Menu_controller : MonoBehaviour
{
    public static Bottom_Menu_controller m_sinThis;

    [SerializeField]
    private List<GameObject> m_gmlAllPannel = new List<GameObject>();
    [SerializeField]
    private List<Sprite> m_gmsplMain = new List<Sprite>();
    [SerializeField]
    private GameObject m_gmMain;
    private Vector2 m_vcStartPos;
    // Start is called before the first frame update
    private int m_inCur = 0;
    [SerializeField]
    private GameObject m_gmBlock;
    private Vector2 m_vcStartPosMain;


    public delegate void ChPage(int index);
    public static event ChPage m_evChangePage;


    public void Awake()
    {
        m_sinThis = this;
    }

    void Start()
    {
        m_vcStartPos = m_gmMain.transform.localPosition;
        m_gmBlock.SetActive(false);
        m_vcStartPosMain = gameObject.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetTo(int id)
    {
        if(m_inCur == id) return;
        m_gmBlock.SetActive(true);
        m_evChangePage?.Invoke(id);
        var ch = m_gmMain.transform.GetChild(0).GetComponent<Image>();

        ch.gameObject.SetActive(false);
        m_gmlAllPannel[m_inCur].SetActive(true);
        m_gmMain.transform.DOLocalMoveX(m_gmlAllPannel[id].transform.localPosition.x, 1f).OnComplete(() =>
        {
            m_gmlAllPannel[id].SetActive(false);
            ch.gameObject.SetActive(true);
            ch.sprite = m_gmsplMain[id];
            m_inCur = id;
            m_gmBlock.SetActive(false);
        });
    }

    public void Hide(bool way)
    {
        m_gmBlock.SetActive(true);
        if (!way)
            gameObject.transform.DOLocalMoveY(m_vcStartPosMain.y - 800, 1f).SetEase(Ease.InFlash).OnComplete(() => m_gmBlock.SetActive(false));
        else
            gameObject.transform.DOLocalMoveY(m_vcStartPosMain.y, 1f).SetEase(Ease.InFlash).OnComplete(() => m_gmBlock.SetActive(false));


    }

}
