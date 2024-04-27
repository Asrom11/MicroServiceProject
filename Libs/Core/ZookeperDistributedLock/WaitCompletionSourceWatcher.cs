using org.apache.zookeeper;

namespace ExampleCore.ZookeperDistributedLock;

public class WaitCompletionSourceWatcher : Watcher
{
    private readonly TaskCompletionSource<bool> _waitCompletionSource;

    public WaitCompletionSourceWatcher(TaskCompletionSource<bool> waitCompletionSource)
    {
        _waitCompletionSource = waitCompletionSource;
    }

    public override Task process(WatchedEvent @event)
    {
        if (@event.getState() == Event.KeeperState.SyncConnected)
        {
            _waitCompletionSource.TrySetResult(true);
        }

        return Task.CompletedTask;
    }
}