using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class AppManager : MonoBehaviour
{
    public Dropdown dropdown;
    public InputField SumField, TermField, PerField, DateField;
    public DateTime date;
    public float sum, term, per, pay;
    public List<Payment> payments = new List<Payment>();
    public Text PayTxt, OverTxt, TotalTxt;
    void Awake()
    {
        SumField = GameObject.Find("SumField").GetComponent<InputField>();
        TermField = GameObject.Find("TermField").GetComponent<InputField>();
        PerField = GameObject.Find("PerField").GetComponent<InputField>();
        dropdown = GameObject.Find("Dropdown").GetComponent<Dropdown>();
    }
    public void OkButtonClick() 
    {
       // try
      //  {
            sum = float.Parse(SumField.text);
            term = float.Parse(TermField.text);
            per = float.Parse(PerField.text);
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
                payment.Left = sum - pay * i;
                payment.Pay = (sum - pay * i) * per / 12;
                payment.Total = pay + (sum - pay * i) * per / 12;
                   
                   overpay += payment.Total;
                   payments.Add(payment);
               }
               foreach(Payment payment in payments)
               {
               }
            Debug.Log(pay.ToString() + " ₽");
            PayTxt.text = pay.ToString() + " ₽";
               TotalTxt.text = overpay.ToString() + " ₽";
                overpay -= sum;
                OverTxt.text = overpay.ToString() + " ₽";
     //   }
      //  catch 
      //  {
      //      Debug.Log("error");
       // }
    }
}

public class Payment
{
    public float Left { get; set; }
    public float Pay { get; set; }
    public float Total { get; set; }
    public Payment() { }
}
