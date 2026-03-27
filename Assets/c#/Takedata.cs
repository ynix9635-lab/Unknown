using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Takedata : MonoBehaviour
{
    bool istakinghp = false;
    bool istakingstamina = false;
    [SerializeField] GameObject takedatapanel;
    [SerializeField] Button applybutton;
    [SerializeField] TMP_InputField inputfield;
    [SerializeField] GameObject negnumbernotice;
    [SerializeField] GameObject formatexception;
    [SerializeField] GameObject overflowexception;
    public void Awake()
    {
        applybutton.onClick.AddListener(Takedataapply);
    }
    public void Takedatastart(bool takinghp,bool takingstamina)
    {
        negnumbernotice.SetActive(false);
        formatexception.SetActive(false);
        istakinghp = takinghp;
        istakingstamina = takingstamina;
        takedatapanel.SetActive(true);
    }
    public void Takedataapply()
    {
        negnumbernotice.SetActive(false);
        formatexception.SetActive(false);
        overflowexception.SetActive(false);
        if(istakinghp)
        {
            try
            {
                float value = float.Parse(inputfield.text);
                if (value < 0f)
                {
                    negnumbernotice.SetActive(true);
                }
                else
                {
                    MCC.mcc.Setmaxhp(value);
                    takedatapanel.SetActive(false);
                }
            }
            catch (FormatException)
            {
                formatexception.SetActive(true);

            }
            catch (OverflowException)
            {
                overflowexception.SetActive(true);
            }
        }
        else if(istakingstamina)
        {
            try
            {
                float value = float.Parse(inputfield.text);
                if(value < 0f)
                {
                    negnumbernotice.SetActive(true);
                }
                else
                {
                    MCC.mcc.Setmaxstamina(value);
                    takedatapanel.SetActive(false);
                }
            }
            catch (FormatException)
            {
                formatexception.SetActive(true);
            }
            catch (OverflowException)
            {
                overflowexception.SetActive(true);
            }
        }
        else
        {
            try
            {
                float value = float.Parse(inputfield.text);
                if (value < 0f)
                {
                    negnumbernotice.SetActive(true);
                }
                else
                {
                    MCC.mcc.Setspeedmultiplier(value);
                    takedatapanel.SetActive(false);
                }
            }
            catch (FormatException)
            {
                formatexception.SetActive(true);
            }
            catch (OverflowException)
            {
                overflowexception.SetActive(true);
            }
        }
    }
}
