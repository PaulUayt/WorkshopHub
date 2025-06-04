namespace WorkshopHub.Service
{
    public interface IRequestHandler<in TRequest, TResponse>
    {
        Task<TResponse> Handle(TRequest command, CancellationToken cancellationToken = default);
    }

    public interface IRequestHandler<TResponse>
    {
        Task<TResponse> Handle(CancellationToken cancellationToken = default);
    }
}
