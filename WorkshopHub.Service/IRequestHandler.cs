namespace WorkshopHub.Service
{
    class IRequestHandler
    {
        public interface IRequestHadler<in TRequest, TResponse>
        {
            Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken = default);
        }

        public interface IRequestHadler<TResponse>
        {
            Task<TResponse> Handle(CancellationToken cancellationToken = default);
        }

    }
}
