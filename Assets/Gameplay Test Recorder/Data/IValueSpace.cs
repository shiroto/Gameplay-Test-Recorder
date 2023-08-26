namespace TwoGuyGames.GTR.Core
{
    /// <summary>
    /// A set of values that define a "space". All possible values are either inside or outside this space.
    /// </summary>
    public interface IValueSpace : IRecord
    {
        string Id
        {
            get;
        }

        bool Contains(object value);

        void Extend(object value);
    }

    public interface IValueSpace<T> : IValueSpace
    {
        bool Contains(T value);

        void Extend(T value);
    }
}