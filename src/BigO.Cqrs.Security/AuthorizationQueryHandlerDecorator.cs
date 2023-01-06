using BigO.Security;

// ReSharper disable UnusedMember.Global

namespace BigO.Cqrs.Security;

/// <summary>
///     Provides a decorator for authorizing queries. This class cannot be inherited.
/// </summary>
/// <typeparam name="TQuery">The type of the query.</typeparam>
/// <typeparam name="TResult">The type of the result.</typeparam>
/// <seealso cref="IQueryDecorator{TQuery,TResult}" />
public sealed class AuthorizationQueryHandlerDecorator<TQuery, TResult> : IQueryDecorator<TQuery, TResult>
    where TQuery : class
{
    private readonly IAuthorizationManager _authorizationManager;
    private readonly IQueryHandler<TQuery, TResult> _decorated;

    /// <summary>
    ///     Initializes a new instance of the <see cref="AuthorizationQueryHandlerDecorator{TQuery, TResult}" /> class.
    /// </summary>
    /// <param name="decorated">The decorated query handler.</param>
    /// <param name="authorizationManager">The authorization manager.</param>
    public AuthorizationQueryHandlerDecorator(IQueryHandler<TQuery, TResult> decorated,
        IAuthorizationManager authorizationManager)
    {
        _decorated = decorated;
        _authorizationManager = authorizationManager;
    }

    /// <summary>
    ///     Runs the authorization rules for the specified query and returns the result of the decorated
    ///     <see cref="Read(TQuery)" /> method.
    /// </summary>
    /// <param name="query">
    ///     The query to be authorized and passed to the decorated handler to execute
    ///     <see cref="Read(TQuery)" /> method.
    /// </param>
    /// <returns>The result of the decorated <see cref="Read(TQuery)" /> method.</returns>
    /// <exception cref="UnauthorizedAccessException">
    ///     Thrown when the authorization rules specified in
    ///     <paramref name="query" /> are not met.
    /// </exception>
    /// <remarks>
    ///     This method first runs the authorization rules specified in <paramref name="query" /> using the
    ///     <see cref="AuthorizationManager" />.
    ///     If the authorization rules are met, the decorated <see cref="Read(TQuery)" /> method is called and its result is
    ///     returned.
    /// </remarks>
    public async Task<TResult> Read(TQuery query)
    {
        await _authorizationManager.Authorize(query);
        return await _decorated.Read(query);
    }
}