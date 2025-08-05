using System.Collections.Generic;
using CardSelectFlow.Interface;
using WhiteWorld.Domain.Entity;

namespace CardSelectFlow
{
    public class FixedValueAlgorithm : IAppearCardDecisionAlgorithm
    {
        private SpaceAmount _spaceAmount;

        public FixedValueAlgorithm(SpaceAmount spaceAmount)
        {
            _spaceAmount = spaceAmount;
        }

        public List<SpaceAmount> GetAppearCards()
        {
            return new List<SpaceAmount>() { _spaceAmount,_spaceAmount,_spaceAmount};
        }
    }
}