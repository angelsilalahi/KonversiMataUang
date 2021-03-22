using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KonversiMataUang
{

	public partial class Form1 : Form
	{
		private List<Currency> currencies;
		public Form1()
		{
			InitializeComponent();
			InitializeCurrency();
			SetUpComponent();
			Convert();
		}
		private void CreateNewCurrency(string name, float rate)
		{
			currencies.Add(new Currency(name, rate));
		}
		private void InitializeCurrency()
		{
			// https://api.exchangeratesapi.io/latest?base=IDR
			currencies = new List<Currency>();
			CreateNewCurrency("IDR", 1.0f);
			CreateNewCurrency("HKD", 0.0005391561f);
			CreateNewCurrency("PHP", 0.0033741044f);
			CreateNewCurrency("GBP", 0.0000500689f);
			CreateNewCurrency("RUB", 0.0051418677f);
			CreateNewCurrency("JPY", 0.0075626175f);
			CreateNewCurrency("EUR", 0.0000583806f);
			CreateNewCurrency("MYR", 0.0002851423f);
			CreateNewCurrency("USD", 0.0000694203f);
			CreateNewCurrency("SGD", 0.0000932338f);
			CreateNewCurrency("AUD", 0.0000897017f);
		}
		
		private void SetUpComponent()
		{
			foreach (Currency currency in currencies)
			{
				comboBoxFrom.Items.Add(currency.currencySymbol);
				comboBoxTo.Items.Add(currency.currencySymbol);
			}
			numInput.Minimum = 0;
			numInput.Maximum = decimal.MaxValue;
			numInput.DecimalPlaces = 2;

			comboBoxFrom.SelectedIndex = 0;
			comboBoxTo.SelectedIndex = 0;

			comboBoxFrom.SelectedIndexChanged += comboBoxFrom_SelectedIndexChanged;
			comboBoxTo.SelectedIndexChanged += comboBoxTo_SelectedIndexChanged;

			numInput.ValueChanged += numInput_ValueChanged;
		}
		private void numInput_ValueChanged(object sender, EventArgs e)
		{
			Convert();
		}

		private void comboBoxTo_SelectedIndexChanged(object sender, EventArgs e)
		{
			Convert();
		}
		private void comboBoxFrom_SelectedIndexChanged(object sender, EventArgs e)
		{
			Convert();
		}
		private Currency GetCurrencyByName(string name)
		{
			for (int i = 0; i < currencies.Count; i++)
			{
				if (currencies[i].currencySymbol == name)
				{
					return currencies[i];
				}
			}
			return null;
		}
		private void Convert()
		{
			float money = (float)numInput.Value;
			Currency inCurrency = GetCurrencyByName(comboBoxFrom.Text);
			Currency outCurrency = GetCurrencyByName(comboBoxTo.Text);
            if (outCurrency != null)
            {
				float output = inCurrency.ConvertTo(money, outCurrency);
				textBoxOutput.Text = "" + output;
            }
		}

        private void labelTitle_Click(object sender, EventArgs e)
        {

        }

        private void labelFrom_Click(object sender, EventArgs e)
        {

        }
    }
    class Currency
	{
		public string currencySymbol { get; }
		private float rate;

		public Currency(string name, float rate)
		{
			this.currencySymbol = name;
			this.rate = rate;
		}

		public float ConvertTo(float money, Currency currency)
		{
			return money / rate * currency.rate;
		}
	}
}
