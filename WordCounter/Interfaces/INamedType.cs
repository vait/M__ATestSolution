namespace WordCounter.Interfaces
{
    /// <summary>
    /// Добавляет имя для типа, чтобы можно было находить в коллекции
    /// </summary>
    internal interface INamedType
    {
        string LocalName { get; }
    }
}
