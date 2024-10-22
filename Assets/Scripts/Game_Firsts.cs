using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading;
using Cysharp.Threading.Tasks;
using System.Threading;

public class Game_Firsts : WindowForUi
{
    public List<Sprite> m_splAll = new List<Sprite>();
    [SerializeField]
    private Text m_txScore,m_txTime;
    private CancellationTokenSource m_tkSource;
    [SerializeField]
    private WindowForUi m_wiAnswer;
    private int _m_inTime;
    private int m_inTime
    {
        get => _m_inTime;
        set
        {
            _m_inTime = value;
            m_txTime.text = _m_inTime.ToString();
        }
    }

    public bool m_bStop;

    private int _m_inScore;
    public int m_inScore
    {
        get => _m_inScore;
        set
        {
            _m_inScore = value;
            m_txScore.text = _m_inScore.ToString();
        }
    }

    [SerializeField]
    private List<Game_Slots_Main> m_gmAllSlots = new List<Game_Slots_Main>();

    public void Start()
    {
        StartPos();
        m_acOnDestroyObj += () =>
        {
            m_tkSource.Cancel();
        };
    }
    public override void Init()
    {
        base.Init();
        StartTime(m_tkSource).Forget();
    }

    public void StartPos()
    {
        m_inTime = 120;
        m_inScore = 0;
        if (m_tkSource != null) m_tkSource.Dispose();
        m_tkSource = new CancellationTokenSource();
        foreach (var slot in m_gmAllSlots)
        {
            slot.m_scrMain = this;
            slot.RandomSpawn();
        }
    }
    public async UniTask StartTime(CancellationTokenSource tk)
    {
        while(m_inTime > 0)
        {
            await UniTask.Delay(1000);
            if (tk == null || tk.Token.IsCancellationRequested)
            {
                if (tk != null)
                    m_tkSource.Dispose();
                return;
            }
            m_inTime--;
        }
        EndGame();
    }
    public void EndGame()
    {
        m_bStop = true;
        var w = Main_Menu_Controller.m_sinThis.OpenWindow(m_wiAnswer, TypeWindow.NoClosed).GetComponent<Game_answer>();
        w.Inits(false, () =>
        {
            Player_Menager.m_inCoin += m_inScore;
            DellObj();
        },() =>
        {
            Player_Menager.m_inCoin += m_inScore;
            StartPos();
            StartTime(m_tkSource).Forget();
        }, m_inScore);
    }
}
