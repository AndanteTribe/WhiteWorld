using WhiteWorld.Domain;
using WhiteWorld.Domain.Entity;

namespace WhiteWorld.Data
{
    public class KeywordModelRepository : MasterDataRepository<KeywordModel>
    {
        public KeywordModelRepository(
            string binaryPath,
            string tableName,
            IMasterDataRepository<DummyModel> dummyRepository) : base(binaryPath, tableName)
        {
            foreach (var entity in Entities.Span)
            {
                entity.DummyData = dummyRepository.Entities;
            }
        }
    }
}