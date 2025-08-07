using System.Collections.Generic;
using CardSelectFlow.Interface;
using UnityEngine;
using WhiteWorld.Domain.Entity;

namespace WhiteWorld.Presentation
{
    /// <summary>
    /// 1~6までの値の内３つをランダムに返すアルゴリズムクラス
    /// </summary>
    public class RandomSelectAlgorithm : IAppearCardDecisionAlgorithm
    {
        private List<int> _numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
        /// <summary>
        /// 1~6までの値の内３つをランダムに返すクラス
        /// </summary>
        public List<SpaceAmount> GetAppearCards()
        {
            List<SpaceAmount> result = new();

            for (var i = 0; i < 3; i++)
            {
                var index = Random.Range(0, _numbers.Count);
                result.Add(new SpaceAmount(_numbers[index]));
                _numbers.RemoveAt(index);
            }

            return result;
        }
    }
}