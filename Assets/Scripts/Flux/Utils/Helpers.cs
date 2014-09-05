using UnityEngine;

public static class Helpers {

  public static T[] Shuffle<T>(T[] input) {
    T[] output = new T[input.Length];
    input.CopyTo(output, 0);

    int j;
    T swap;

    for (int i = output.Length - 1; i > 0; i--) {
      j = Random.Range(0, output.Length);
      swap = output[i];
      output[i] = output[j];
      output[j] = swap;
    }

    return output;
  }
}