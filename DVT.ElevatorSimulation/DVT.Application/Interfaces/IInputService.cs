namespace DVT.Application.Interfaces
{
    public interface IInputService
    {
        int GetValidatedIntInput(string prompt, int? minValue, int? maxValue);
        bool GetValidatedIntInput(string prompt, bool condition = false);
    }
}