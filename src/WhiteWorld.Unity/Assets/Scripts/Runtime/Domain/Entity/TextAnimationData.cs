using System;

namespace WhiteWorld.Domain.Entity
{
    /// <summary>
    /// 人生ゲームのテキストアニメーションデータ.
    /// </summary>
    /// <param name="Keyword">表示するキーワード.</param>
    /// <param name="DummyModels">ダミーテキストのモデル.</param>
    public sealed record TextAnimationData(KeywordModel Keyword, ReadOnlyMemory<DummyModel> DummyModels);
}