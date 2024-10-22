using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class Info_Room_Script : WindowForUi
{
    [SerializeField]
    private List<Room> m_gLsRoom = new List<Room>();
    [SerializeField]
    private Image m_spMain, m_spLeft, m_spRight;
    [SerializeField]
    private Text m_txName,m_txDescription;

    [SerializeField] 
    private Button m_btMain;

    [SerializeField] 
    private WindowForUi m_wiUpgrade;

    [SerializeField]
    private GameObject m_gmKey;

    private int _m_inIndex = 0;
    private int m_inIndex
    {
        get => _m_inIndex;
        set
        {
            var t = value - _m_inIndex;
            _m_inIndex = value;

            if(_m_inIndex <0) _m_inIndex = m_gLsRoom.Count-1;
            if(m_gLsRoom.Count <= _m_inIndex) _m_inIndex = 0;

            var im = GetImg(_m_inIndex);




            m_spLeft.sprite = im[0];
            m_spMain.sprite = im[1];
            m_spRight.sprite = im[2];


            if (m_txName) m_txName.text = m_gLsRoom[_m_inIndex].name.Split('.')[1];
            var ts = m_gLsRoom[_m_inIndex].name.Split('.')[1];
            var sv = PlayerPrefs.GetInt(ts,0);
            if (_m_inIndex == 0) sv = 1;
            if (sv == 1)
            {
                Player_Menager.m_inIdOfRoom = m_inIndex;
                if (!m_bUp)
                {
                    if (m_inUp > 0)
                    {
                        m_btMain.onClick.RemoveListener(Buy);
                    }
                    m_btMain.onClick.AddListener(GetUpdate);
                    m_bUp = true;
                    m_inUp = 1;
                    m_gmKey.SetActive(false);
                }
            }
            else
            {
                m_inUp = 1;
                m_btMain.onClick.RemoveListener(GetUpdate);
                m_btMain.onClick.AddListener(Buy);
                m_bUp = false;
                m_gmKey.SetActive(true);
            }

        }
    }
    private bool _m_bUp;
    private bool m_bUp
    {
        get => _m_bUp;
        set
        {
            _m_bUp = value;
            var tx = m_btMain.GetComponentInChildren<Text>();

            if (_m_bUp) 
            {
                tx.text = "Upgrade";
                m_btMain.interactable = true;
                m_gmKey.SetActive(false);
            }
            else
            {
                m_gmKey.SetActive(true);
                if (Player_Menager.m_inVisitors >= m_gLsRoom[m_inIndex].m_inVisitorsNeed)
                    m_btMain.interactable = true;
                else
                    m_btMain.interactable = false;

                tx.text = $"{Player_Menager.m_inVisitors}/{m_gLsRoom[m_inIndex].m_inVisitorsNeed}";

            }
        }
    }
    private int m_inUp;

    private void Start()
    {
        m_gLsRoom = All_Room.m_lsAllIrRoom;
        m_inIndex = Player_Menager.m_inIdOfRoom;

        if (m_txDescription) m_txDescription.text = m_gLsRoom[_m_inIndex].m_strDescr;
    }

    public void Buy()
    {
        Player_Menager.m_inVisitors -= m_gLsRoom[m_inIndex].m_inVisitorsNeed;
        var ts = m_gLsRoom[m_inIndex].name.Split('.')[1];
        PlayerPrefs.SetInt(ts, 1);
        m_inIndex = m_inIndex;
    }

    public void Next()
    {
        GameObject[] all = new GameObject[3];
        all[0] = m_spLeft.gameObject;
        all[1] = m_spMain.gameObject;
        all[2] = m_spRight.gameObject;
        
        Move(all,0.8f);
    }
    public void Previous() 
    {
        GameObject[] all = new GameObject[3];
        all[0] = m_spRight.gameObject;
        all[1] = m_spMain.gameObject;
        all[2] = m_spLeft.gameObject;

        Move(all, 0.8f,true);

    }
    public Sprite[] GetImg(int index)
    {
        Sprite[] Main = new Sprite[3];


        if (index == 0)
        {
            Main[0] = m_gLsRoom[m_gLsRoom.Count - 1].m_spRoomIco;
            Main[1] = m_gLsRoom[index].m_spRoomIco;
            Main[2] = m_gLsRoom[index+1].m_spRoomIco;
        }else if(index == m_gLsRoom.Count - 1)
        {
            Main[0] = m_gLsRoom[index -1].m_spRoomIco;
            Main[1] = m_gLsRoom[index].m_spRoomIco;
            Main[2] = m_gLsRoom[0].m_spRoomIco;
        }
        else
        {
            Main[0] = m_gLsRoom[index - 1].m_spRoomIco;
            Main[1] = m_gLsRoom[index].m_spRoomIco;
            Main[2] = m_gLsRoom[index+1].m_spRoomIco;
        }




        return Main;
    }

    private bool Playing = false;

    private bool m_bInit = false;
    public void Move(GameObject[] gm,float speed = 1,bool rev = false)
    {
        if (!m_bInit)
        {
            m_bInit = true;
            m_spRight.transform.parent.gameObject.SetActive(true);
            m_spLeft.transform.parent.gameObject.SetActive(true);

            m_acOnSDestroyObj += () =>
            {
                m_spRight.transform.parent.gameObject.SetActive(false);
                m_spLeft.transform.parent.gameObject.SetActive(false);
            };
        }
        if (Playing) return;
        Playing = true;

        var posleft = gm[0].transform.parent.localPosition;
        var posMain = gm[1].transform.parent.localPosition;
        var posRight = gm[2].transform.parent.localPosition;

        gm[0].transform.parent.localPosition = posRight;
        gm[1].transform.parent.DOLocalMoveX(posleft.x, speed).SetEase(Ease.Linear);
        gm[1].transform.parent.DOScale(0.5f, speed).SetEase(Ease.Linear);
        gm[2].transform.parent.DOScale(1f, speed).SetEase(Ease.Linear);
        gm[2].transform.parent.DOLocalMoveX(posMain.x, speed).SetEase(Ease.Linear).OnComplete(() => {
            Playing = false;
            if (rev)
            {
                m_spRight = gm[1].GetComponent<Image>();
                m_spLeft = gm[0].GetComponent<Image>();
                m_spMain = gm[2].GetComponent<Image>();




                m_inIndex--;
            }
            else
            {
                m_spRight = gm[0].GetComponent<Image>();
                m_spLeft = gm[1].GetComponent<Image>();
                m_spMain = gm[2].GetComponent<Image>();


                m_inIndex++;
            }

            });

        if (rev)
        {
            if (m_txDescription)
                if(_m_inIndex - 1 >= 0)
                    Main_Tutor.GenerateTextAsy(m_gLsRoom[_m_inIndex - 1].m_strDescr, m_txDescription, 0.5f).Forget();
                else
                    Main_Tutor.GenerateTextAsy(m_gLsRoom[m_gLsRoom.Count -1].m_strDescr, m_txDescription, 0.5f).Forget();

        }
        else
        {
            if (m_txDescription)
                if (_m_inIndex + 1 >= m_gLsRoom.Count)           
                    Main_Tutor.GenerateTextAsy(m_gLsRoom[0].m_strDescr, m_txDescription, 0.5f).Forget();
                else
                     Main_Tutor.GenerateTextAsy(m_gLsRoom[_m_inIndex + 1].m_strDescr, m_txDescription, 0.5f).Forget();

        }

    }
    private bool Ch(int indexss)
    {
        if(indexss < 0)
        {
            if (m_gLsRoom.Count - 1 <= Player_Menager.m_inCurLvl) return true;
            else return false;
        }else if(indexss > m_gLsRoom.Count - 1) 
        {
            if (0 <= Player_Menager.m_inCurLvl) return true;
            else return false;
        }
        else
        {
            if (indexss <= Player_Menager.m_inCurLvl) return true;
            else return false;

        }

    }
    public void OnDestroy()
    {
    }

    public void GetUpdate()
    {
        if (Playing) return;
        m_gmBlock.SetActive(true);
        var n = Main_Menu_Controller.m_sinThis.OpenWindow(m_wiUpgrade, TypeWindow.NoClosed).GetComponent<Main_Upgrade>();
        n.m_bLoad = true;
        n.SetP(m_gLsRoom[m_inIndex], true);
        n.m_acOnDestroyObj += () =>
        {
            m_gmBlock.SetActive(false);
        };

    }
}

