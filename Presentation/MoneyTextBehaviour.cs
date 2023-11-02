using Herb.Domain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herb.Presentation
{

    public class MoneyTextBehaviour : MonoBehaviour
    {
        [SerializeField] TMPro.TMP_Text moneyText;

        private void Start()
        {
            PlanterManager.Instance.OnMoneyChange += SetMoneyText;
        }

        private void SetMoneyText(int money)
        {
            moneyText.text = money.ToString();
        }
    }

}