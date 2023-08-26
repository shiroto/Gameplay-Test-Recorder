namespace TwoGuyGames.GTR.Core
{
    internal interface IStateComparer
    {
        float Compare(IValueSpace x, IRecord y, IStateDifferenceWheights wheights);
    }
}