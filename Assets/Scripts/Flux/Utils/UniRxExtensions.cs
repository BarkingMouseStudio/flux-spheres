using System;
using UnityEngine;
using UniRx;

public static class UniRxExtensions {

  public static IObservable<TResult> SelectWhere<TSource, TResult>(
    this IObservable<TSource> source,
    Func<TSource, Tuple<bool, TResult>> predicate) {
    return source.SelectMany(item => {
      Tuple<bool, TResult> result = predicate(item);
      if (result.Item1) {
        return Observable.Return(result.Item2);
      } else {
        return Observable.Empty<TResult>();
      }
    });
  }
}
