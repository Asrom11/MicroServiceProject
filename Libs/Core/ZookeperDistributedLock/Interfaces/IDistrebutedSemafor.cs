namespace ExampleCore.ZookeperDistributedLock.Interfaces;

public interface IDistrebutedSemafor
{
    Task<LockHandler> TryAcquireAsync(TimeoutValue timeout, CancellationToken cancellationToken = default);
    Task ReleaseAsync(string nodePath);
}