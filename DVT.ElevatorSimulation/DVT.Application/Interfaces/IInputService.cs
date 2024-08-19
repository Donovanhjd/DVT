namespace DVT.Application.Interfaces
{
    public interface IInputService
    {
        int GetValidatedIntInput(string prompt, int? minValue, int? maxValue);
    }
}