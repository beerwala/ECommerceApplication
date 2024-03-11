using Infrasturcture.shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrasturcture.shared.Services
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
