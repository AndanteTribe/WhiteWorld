using WhiteWorld.Domain;
using WhiteWorld.Domain.Entity;

namespace WhiteWorld.AppMain
{
    public interface ICardSelectionSequence : ILifeGameSequence
    {
        public void FinishCardSelect(SpaceAmount amount);
    }
}