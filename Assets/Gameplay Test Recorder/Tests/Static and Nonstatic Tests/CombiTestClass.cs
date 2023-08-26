namespace TwoGuyGames.GTR.Core.Tests
{
    internal class CombiTestClass
    {
        public int int1;
        public int int2;
        public int mockInt;

        public void GetInts()
        {
            int1 = StaticClass.ReturnIntValue();
            int2 = mockInt;
        }
    }
}