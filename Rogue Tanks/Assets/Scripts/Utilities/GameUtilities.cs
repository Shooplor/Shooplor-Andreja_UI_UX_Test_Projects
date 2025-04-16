using System;
using System.Collections;
using UnityEngine;

public class GameUtilities : MonoBehaviour
{
    internal void CountFor(int seconds, Action<int> onSecond) => StartCoroutine(Count(seconds, onSecond));
    internal Coroutine WaitFor(int seconds, Action onTimerExpired) => StartCoroutine(ExecuteAfter(seconds, onTimerExpired));
    public void RepeatActions(int numberOfTimes, float seconds, Action[] actions) => StartCoroutine(repeatActions(numberOfTimes, seconds, actions));
    public Coroutine InfiniteRepeat(float seconds, Action<int> onSecond, Action[] actions) => StartCoroutine(infiniteRepeatActions(seconds, onSecond, actions));

    private IEnumerator infiniteRepeatActions(float seconds, Action<int> onSecond, Action[] actions)
    {
        int second = 0;
        while (true)
        {
            foreach(var action in actions)
            {
                action();
                yield return new WaitForSeconds(seconds);
                second++;
                onSecond(second);
            }
        }
    }

    private IEnumerator repeatActions(int numberOfTimes, float seconds, Action[] actions)
    {
        for (int i = 1; i <= numberOfTimes; i++)
        {
            foreach (var action in actions)
            {
                action();
                yield return new WaitForSeconds(seconds);
            }
        }
    }

    private IEnumerator Count(int seconds, Action<int> onSecond)
    {
        int second = 1;
        while (true)
        {
            onSecond(second);
            if (second == seconds) break;
            yield return new WaitForSeconds(1);
            second++;
        }
    }

    private IEnumerator ExecuteAfter(float seconds, Action onTimerExpired)
    {
        yield return new WaitForSeconds(seconds);
        onTimerExpired();
    }

    public void ExecuteInTimeIntervalBetween(float minSeconds, float maxSeconds, Action action, Func<bool> stopPredicate) => StartCoroutine(ExecuteWhile(minSeconds, maxSeconds, action, stopPredicate));

    private IEnumerator ExecuteWhile(float minSeconds, float maxSeconds, Action action, Func<bool> stopPredicate)
    {
        while (!stopPredicate())
        {
            var waitFor = Probability.GenerateRandomNumberBetween(minSeconds, maxSeconds);
            yield return new WaitForSeconds(waitFor);
            action();
        }
    }
}
