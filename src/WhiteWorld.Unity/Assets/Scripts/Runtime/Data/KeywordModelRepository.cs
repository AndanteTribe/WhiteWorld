using WhiteWorld.Domain;
using WhiteWorld.Domain.Entity.Runtime.Domain.Entity;
using ZLinq;

namespace WhiteWorld.Data.Runtime.Data
{
    public class KeywordModelRepository : MasterDataRepository<KeywordModel>
    {
        private readonly IMasterDataRepository<DummyModel> _dummyRepository;

        public KeywordModelRepository(
            string binaryPath,
            string tableName,
            IMasterDataRepository<DummyModel> dummyRepository) : base(binaryPath, tableName)
        {
            _dummyRepository = dummyRepository;
            _entities = _entities
                .AsValueEnumerable()
                .Select(x =>
                {
                    x.DummyData = dummyRepository.Entities.ToArray();
                    return x;
                })
                .ToArray();
        }
    }
}