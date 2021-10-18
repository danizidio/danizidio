using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboCounter : MonoBehaviour
{
    public static ComboCounter instance;

    [SerializeField] string fixedText;
    [SerializeField] TMP_Text txtObj, txtObj2, lowHit, overHead, counter;
    [SerializeField] int _comboNumbers;

    [SerializeField] float timer,
                           tempTimer,
                           maxtimer;
    public int comboNumber { get { return _comboNumbers; } set { _comboNumbers = value; } }

    private void Start()
    {
        instance = this;

        tempTimer = maxtimer;

        timer = maxtimer;
    }

    private void Update()
    {
        if(comboNumber >= 1)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            txtObj.text = "";
            txtObj2.text = "";
        }

        if(timer <= 0)
        {
            timer = 0;

            comboNumber = 0;

            tempTimer = maxtimer;
        }
    }

    public void ComboTime()
    {
        tempTimer += (comboNumber / 3);

        if(tempTimer >= 2)
        {
            tempTimer = 2;
        }

        return;
    }

    public void HitCombo()
    {
        comboNumber++;

        if (comboNumber == 1)
        {
            txtObj.text = "";
            txtObj2.text = "";
        }
        else
        {
            txtObj.text = comboNumber.ToString();
            txtObj2.text = fixedText;
        }

        float s = (float)comboNumber / 5;

        if (s > 1.5f)
        {
            s = 1.5f;
        }

        txtObj2.gameObject.transform.localScale = new Vector3(s, s, 1);

        txtObj.GetComponent<Animator>().SetTrigger("Hit");
        txtObj2.GetComponent<Animator>().SetTrigger("Hit");

        timer = tempTimer;

        ComboTime();


        return;
    }

    public void UIOverhead()
    {
        overHead.gameObject.SetActive(true);
    }

    public void UILowHit()
    {
        lowHit.gameObject.SetActive(true);
    }
    public void UICounter()
    {
        counter.gameObject.SetActive(true);
    }
}
