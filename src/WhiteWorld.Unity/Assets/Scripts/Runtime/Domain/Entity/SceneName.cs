using System.Runtime.Serialization;
using RapidEnum;

namespace WhiteWorld.Domain.Entity
{
    /// <summary>
    /// シーン名列挙体.
    /// </summary>
    [System.Flags]
    [RapidEnum]
    public enum SceneName
    {
        /// <summary> 無効なシーン. </summary>
        Invalid = 0,

        /// <summary> タイトルシーン. </summary>
        [EnumMember(Value = "Title")]
        Title = 1 << 0,

        /// <summary> オープニングシーン. </summary>
        [EnumMember(Value = "Opening")]
        Opening = 1 << 1,

        /// <summary> カード選択編集シーン. </summary>
        [EnumMember(Value = "CardSelectEdit")]
        CardSelectEdit = 1 << 2,

        /// <summary> メッセージウィンドウUIシーン. </summary>
        [EnumMember(Value = "Message Window UI")]
        MessageWindow = 1 << 3,

        /// <summary> ライフゲームシーン. </summary>
        [EnumMember(Value = "LifeGame")]
        LifeGame = 1 << 4,

        /// <summary> テキストアニメーションシーン. </summary>
        [EnumMember(Value = "LifeGame")]
        TextAnimation = 1 << 5,
    }
}