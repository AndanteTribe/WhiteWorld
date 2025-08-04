using System.Collections.Generic;
using CardSelectFlow.Interface;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CardSelectFlow
{
    /// <summary>
    /// 1~6までの値の内３つをランダムに返すクラス
    /// </summary>
    public class RandomSelectAlgorithm : IAppearCardDecisionAlgorithm
    {
        /// <summary>
        /// 1~6までの値の内３つをランダムに返すクラス
        /// </summary>
        public List<int> GetAppearCards()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
            List<int> result = new();

            for (var i = 0; i < 3; i++)
            {
                var index = Random.Range(0, numbers.Count);
                result.Add(numbers[index]);
                numbers.RemoveAt(index);
            }

            return result;
        }
    }
}