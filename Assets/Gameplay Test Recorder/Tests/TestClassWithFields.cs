namespace TwoGuyGames.GTR.Core.Tests
{
    internal class TestClassWithFields
    {
        public TestClassWithMethods classField;
        public int myInt;
        public float result;

        public TestClassWithFields()
        {
            classField = new TestClassWithMethods();
        }

        public void Method1()
        {
            result = classField.GetInt();
        }

        public void Method2()
        {
            result = classField.GetFloat(1);
        }

        public void Method3()
        {
            result = myInt;
        }

        public void Method4()
        {
            result = ValueRecorder.Store(myInt, "MYKEY");
        }

        public void Method5()
        {
            result = ValueRecorder.NextInput<int>("MYKEY");
        }
    }
}