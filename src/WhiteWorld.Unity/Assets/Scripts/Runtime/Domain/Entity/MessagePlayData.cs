using System;

namespace WhiteWorld.Domain.Entity
{
    /// <summary>
    /// メッセージをUIに表示する際に使用するデータ群
    /// </summary>
    /// <param name="Messages"></param>
    /// <param name="IsFadeout"></param>
    public record MessagePlayData(Memory<MessageModel> Messages, bool IsFadeout = false, bool IsFadein = false);
}