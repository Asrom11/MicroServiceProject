namespace ExampleCore.ZookeperDistributedLock;

public class LockHandler : IAsyncDisposable
{
    private readonly ZooKeeperDistributedSemaphore _semaphore;
    private readonly string _nodePath;

    public LockHandler(ZooKeeperDistributedSemaphore semaphore, string nodePath)
    {
        _semaphore = semaphore;
        _nodePath = nodePath;
    }
    
    public async ValueTask DisposeAsync()
    {
        await _semaphore.ReleaseAsync(_nodePath)
            .ConfigureAwait(false);
    }
}