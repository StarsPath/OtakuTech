using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.UI;

namespace OtakuTech.Content.Currencies
{
    public class CrystalCurrency : CustomCurrencySingleCoin
    {
        public CrystalCurrency(int coinItemID, long currencyCap, string CurrencyTextKey) : base(coinItemID, currencyCap)
        {
            this.CurrencyTextKey = CurrencyTextKey;
            CurrencyTextColor = Color.DeepSkyBlue;
        }
    }
}
