using DVT.Application.Interfaces;

namespace DVT.Application.Services;

public class InputService : IInputService
{
    /// <summary>
    /// Validates integer input within a specified range.
    /// </summary>
    /// <param name="prompt">The prompt message to display.</param>
    /// <param name="minValue">The minimum valid value.</param>
    /// <param name="maxValue">The maximum valid value.</param>
    /// <returns>The validated integer input.</returns>
    public int GetValidatedIntInput(string prompt, int? minValue, int? maxValue)
    {
        int value;

        if (maxValue == 0)
        {
            return default;
        }

        do
        {
            Console.Write($"{prompt} ({minValue} - {maxValue}): ");
            string? input = Console.ReadLine();
            if (int.TryParse(input, out value) && value >= minValue && value <= maxValue)
            {
                break;
            }
            else
            {
                Console.WriteLine($"Invalid input. Please enter a number between {minValue} and {maxValue}.");
            }
        } while (true);

        return value;
    }
}
