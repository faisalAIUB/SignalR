using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Web.Mvc;
using signalrchat_test_1.Controllers;

namespace signalrchat_test_1
{
    public class ChatHub : Hub
    {
        public static Dictionary<string, string> LstAllConnections = new Dictionary<string, string>();
        public static List<User> Users = new List<User>();
        public static List<UserMessage> UserMessages = new List<UserMessage>();
        ChatController chatController = new ChatController();
        public override Task OnConnected()
        {
            LstAllConnections.Add(Context.ConnectionId, "");
            User user = new User { Id = Context.ConnectionId, UserId = chatController.GetId(), Name = "" };
            Users.Add(user);
            Clients.All.BroadcastConnections(LstAllConnections);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            LstAllConnections.Remove(Context.ConnectionId);
            Clients.All.BroadcastConnections(LstAllConnections);
            return base.OnDisconnected(stopCalled);
        }
        public void SetConnectionName(string connectionname,string UserId)
        {
            LstAllConnections[Context.ConnectionId] = connectionname;
            User user = new User();
            user = Users.First(x => x.UserId == Convert.ToInt32(UserId));
            user.Name = connectionname;
            Clients.All.BroadcastConnections(LstAllConnections);
        }
        public void Send(string partnerId,string UserId,string message)
        {
            User from = new User { Id = Context.ConnectionId,UserId=Convert.ToInt32(UserId), Name = LstAllConnections[Context.ConnectionId] };           
           
            Clients.Clients(new List<string> {Context.ConnectionId,partnerId }).BroadcastMessage(from, message);
            UserMessage userMessage = new UserMessage();
            userMessage.UserId = from.UserId;
            userMessage.ReceverId = Users.First(x => x.Id == partnerId).UserId;
            userMessage.Message = message;
            userMessage.DateTime = DateTime.Now;
            UserMessages.Add(userMessage);
        }
        public List<msgHead> GetMessageFromServer(string connectionId,string partnerConnectionId)
        {
            string uniquekey = connectionId + partnerConnectionId;
            List<msg> obj = new List<msg>()
            {
               new msg(){ user = "Faisal", message = "Hi" },
               new msg(){ user="iii",message="hello" }
            };
            msgHead msgHead = new msgHead();
            msgHead.title = uniquekey;
            msgHead.obj = obj;
            List<msgHead> lstMessages = new List<msgHead>();
            lstMessages.Add(msgHead);
            return lstMessages;
        }
    }
    public class User
    {
        
        public string Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
    }
    public class UserMessage
    {
        public int UserId { get; set; }
        public int ReceverId { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
    }
    public class msgHead
    {
        public string title { get; set; }
        public List<msg> obj { get; set; }
    }
    public class msg
    {
        public string user { get; set; }
        public string message { get; set; }
    }
}   