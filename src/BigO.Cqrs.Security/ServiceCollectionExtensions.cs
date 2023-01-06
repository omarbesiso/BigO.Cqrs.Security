using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace BigO.Cqrs.Security;

[PublicAPI]
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Decorates the command handler for the specified command type with authorization logic.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command to be decorated.</typeparam>
    /// <param name="serviceCollection">The service collection to contain the registration.</param>
    /// <returns>A reference to this service collection instance after the operation has completed.</returns>
    /// <remarks>
    ///     This method decorates the command handler for the specified command type with the
    ///     <see cref="AuthorizationCommandDecorator{TCommand}" /> class, which runs authorization rules before handling the
    ///     command.
    /// </remarks>
    public static IServiceCollection DecorateCommandHandlerWithAuthorization<TCommand>(
        this IServiceCollection serviceCollection)
        where TCommand : class
    {
        return serviceCollection.DecorateCommandHandler<TCommand, AuthorizationCommandDecorator<TCommand>>();
    }

    /// <summary>
    ///     Decorates the query handler for the specified query and result types with authorization logic.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query to be decorated.</typeparam>
    /// <typeparam name="TResult">The type of the result of the query.</typeparam>
    /// <param name="serviceCollection">The service collection to contain the registration.</param>
    /// <returns>A reference to this service collection instance after the operation has completed.</returns>
    /// <remarks>
    ///     This method decorates the query handler for the specified query and result types with the
    ///     <see cref="AuthorizationQueryHandlerDecorator{TQuery, TResult}" /> class, which runs authorization rules before
    ///     handling the query.
    /// </remarks>
    public static IServiceCollection DecorateQueryHandlerWithAuthorization<TQuery, TResult>(
        this IServiceCollection serviceCollection)
        where TQuery : class
    {
        return serviceCollection
            .DecorateQueryHandler<TQuery, TResult, AuthorizationQueryHandlerDecorator<TQuery, TResult>>();
    }
}