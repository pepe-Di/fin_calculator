using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class AppManager : MonoBehaviour
{
    public GameObject panel;
    public Button button;
    public GameObject Prefab;
    public Dropdown dropdown;
    public GameObject Content;
    public InputField SumField, TermField, PerField, DateField;
    public DateTime date;
    public float sum, term, per, pay;
    public List<Payment> payments = new List<Payment>();
    public Text PayTxt, OverTxt, TotalTxt;
    void Awake()
    {
        button.enabled = false;
        SumField = GameObject.Find("SumField").GetComponent<InputField>();
        TermField = GameObject.Find("TermField").GetComponent<InputField>();
        PerField = GameObject.Find("PerField").GetComponent<InputField>();
        dropdown = GameObject.Find("Dropdown").GetComponent<Dropdown>();
    }
    private void Update()
    {
        if (SumField.text != null&& TermField.text != null&& PerField.text != null) button.enabled = true;
    }
    public void OkButtonClick() 
    {
        try
        {
            try
            {
                sum = float.Parse(SumField.text);
                term = float.Parse(TermField.text);
                per = float.Parse(PerField.text);
            }
            catch 
            {
                SumField.text = "";
                TermField.text = ""; 
                PerField.text = "";
                return;
            }
            panel.SetActive(true);
            if (dropdown.value == 0) 
            {
                term *= 12;
            }
            per /= 100;
            pay = sum / term;
            float overpay = 0;
            for (float i = 0; i < term; i++)
            {
                Payment payment = new Payment();
                payment.values[0] = i + 1;
                payment.values[1] = sum - pay * i;
                payment.values[2] = pay;
                payment.values[3] = (sum - pay * i) * per / 12;
                payment.values[4] = pay + (sum - pay * i) * per / 12;
                   
                overpay += payment.values[4];
                payments.Add(payment);
            }
            foreach(Payment payment in payments)
            {
                 for (int i = 0; i<5; i++) 
                 {
                    GameObject gm = Instantiate(Prefab, new Vector3(0, 0, 0), Quaternion.identity);
                    gm.transform.SetParent(Content.transform);
                    gm.transform.localScale = new Vector3(1, 1, 1);
                    Text txt = gm.GetComponent<Text>();
                    txt.text = payment.values[i].ToString();
                }
            }
            Debug.Log(pay.ToString() + " ₽");
            PayTxt.text = pay.ToString() + " ₽";
            TotalTxt.text = overpay.ToString() + " ₽";
            overpay -= sum;
            OverTxt.text = overpay.ToString() + " ₽";
        }
        catch 
        {
        }
    }
}
public class Payment
{
    public float[] values;
    public Payment() 
    {
        values = new float[5];
    }
}
