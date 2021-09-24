using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TimeCounter;

public class EnemyLifeBar : MonoBehaviour
{
    [SerializeField] Image _frontLifeBar;

    [SerializeField] Image _enemyPortait;

    [SerializeField] Color32 colorBar;
    [SerializeField] float maxTime;

    Timer t;
    public Image FrontLifeBar { get { return _frontLifeBar; } set { _enemyPortait = value; } }
    public Image EnemyPortrait { get { return _enemyPortait; } set { _enemyPortait = value; } }

    void Start()
    {
        FrontLifeBar.color = colorBar;
        FrontLifeBar.fillAmount = 1;

        t = GetComponent<Timer>();
    }

    private void LateUpdate()
    {
        if(maxTime > 0)
        {
            t.CountDown();
        }
    }

    public void StayActive()
    {
        t.GetComponent<Timer>().SetTimer(maxTime, () => this.gameObject.SetActive(false));
    }


}
