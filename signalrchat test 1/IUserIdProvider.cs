using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace signalrchat_test_1
{
    interface IUserIdProvider
    {
        string GetUserId(IRequest request);
    }
}
