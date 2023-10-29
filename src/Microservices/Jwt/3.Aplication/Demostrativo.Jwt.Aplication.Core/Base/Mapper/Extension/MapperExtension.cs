using AutoMapper;

namespace Demostrativo.Jwt.Aplication.Core.Base.Mapper.Extension;

public static class MapperExtensions
{
    public static Task<TResult> MapAsync<TSource, TResult>(this IMapper mapper, Task<TSource> task, CancellationToken token)
    {
        if (task == null) throw new ArgumentNullException(nameof(task));

        var tcs = new TaskCompletionSource<TResult>();

        task.ContinueWith(_ => tcs.TrySetCanceled(), token, TaskContinuationOptions.OnlyOnCanceled, TaskScheduler.Default);

        task.ContinueWith(t => tcs.TrySetResult(mapper.Map<TSource, TResult>(t.Result)), token,
            TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.Default);

        task.ContinueWith(t => tcs.TrySetException(t.Exception!), token, TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.Default);

        return tcs.Task;
    }
}