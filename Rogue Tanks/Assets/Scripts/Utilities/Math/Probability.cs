using UnityEngine;

public static class Probability
{
    public static float GenerateProbability() => Random.Range(1.0f, 100.0f);
    public static float GenerateRandomNumberBetween(float number1, float number2) => Random.Range(number1, number2);
    public static int GenerateRandomNumberBetween(int number1, int number2) => Random.Range(number1, number2);
}
