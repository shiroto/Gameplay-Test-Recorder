namespace TwoGuyGames.GTR.Samples
{
    internal interface IThirdPersonInput
    {
        float GetRotation();

        bool IsDown();

        bool IsJump();

        bool IsLeft();

        bool IsRight();

        bool IsUp();
    }
}