namespace TwoGuyGames.GTR.Core
{
    public static class StateToRange
    {
        public static IValueSpace ConvertToRange(IObjectState state)
        {
            if (state is IObjectStateCollection hs)
            {
                return ConvertToSpace(hs);
            }
            else
            {
                return _ConvertToSpace(state);
            }
        }

        public static IValueSpaceCollection ConvertToSpace(IObjectStateCollection states)
        {
            ValueSpaceCollection res = new ValueSpaceCollection(states.Id);
            foreach (IObjectState s in states)
            {
                if (s is IObjectStateCollection hs)
                {
                    res.Add(ConvertToSpace(hs));
                }
                else
                {
                    res.Add(_ConvertToSpace(s));
                }
            }
            return res;
        }

        private static IValueSpace _ConvertToSpace(IObjectState state)
        {
            return RangeRecordFactory.CreateRange(state.Id, state.Record);
        }
    }
}