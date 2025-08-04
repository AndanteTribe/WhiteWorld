using System;

namespace CardSelectFlow
{
    /// <summary>
    /// カードの番号などの情報を保持するクラス
    /// </summary>
    public class CardInfo
    {
        /// <summary>
        /// カード番号
        /// </summary>
        public int Number => _number;
        private readonly int _number;

        public CardInfo(int number)
        {
            //とりあえず１～６
            if (number <= 0 || number > 6)
            {
                throw new ArgumentOutOfRangeException($"number {number} は不正値です");
            }
            _number = number;
        }
    }
}