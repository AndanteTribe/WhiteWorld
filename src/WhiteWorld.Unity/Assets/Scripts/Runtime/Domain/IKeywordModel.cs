namespace WhiteWorld.Domain
{
    public interface IKeywordModel
    {
        public string KeywordText { get; }
        public IDummyModel DummyModel { get; }
    }
}