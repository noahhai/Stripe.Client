using System.Threading;
using System.Threading.Tasks;
using Stripe.Client.Sdk.Models;
using Stripe.Client.Sdk.Models.Filters;

namespace Stripe.Client.Sdk.Clients.Core
{
    public interface IEventClient
    {
        Task<StripeResponse<Event>> GetEvent(string id, CancellationToken cancellationToken = default(CancellationToken));

        Task<StripeResponse<Pagination<Event>>> GetEvents(EventListFilter filter,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}