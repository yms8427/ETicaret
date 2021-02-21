using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Yms.SignalR
{
    public class NotificationHub : Hub<IYmsHub>
    {
        public Task CreateGroup(string name)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, name);
        }

        public Task Introduce(string name)
        {
            var message = $"Hello, my name is {name}";
            return Clients.Others.SomeOneIntroducedItself(message);
        }

        public Task CartInsertion(string username, Guid productId)
        {
            return Clients.Group("admin").ProductAddedToCart(username, productId);
        }

        public Task CartDeletion(string username, Guid productId)
        {
            return Clients.Group("admin").ProductRemovedFromCart(username, productId);
        }
    }

    public interface IYmsHub
    {
        Task SomeOneIntroducedItself(string message);
        Task ProductAddedToCart(string username, Guid productId);
        Task ProductRemovedFromCart(string username, Guid productId);
    }
}