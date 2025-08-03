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
    }
}