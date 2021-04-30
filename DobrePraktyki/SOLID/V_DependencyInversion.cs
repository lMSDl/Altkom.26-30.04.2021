using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DobrePraktyki.SOLID
{
    interface IMessage
    {
        void Send();
    }

    internal class SMS : IMessage
    {
        public string Number { get; set; }
        public string Content { get; set; }

        public void Send()
        {
            SendSMS();
        }

        public void SendSMS()
        {
            Console.WriteLine("Sending SMS...");
        }
    }

    internal class MMS : IMessage
    {
        public string Number { get; set; }
        public byte[] Content { get; set; }

        public void Send()
        {
            SendMMS();
        }

        public void SendMMS()
        {
            Console.WriteLine("Sending MMS...");
        }
    }

    internal class Mail : IMessage
    {
        public string Address { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public void Send()
        {
            SendMail();
        }

        public void SendMail()
        {
            Console.WriteLine("Sending Mail...");
        }
    }

    internal class Messenger
    {
        public IEnumerable<IMessage> Messages { get; set; }

        public Messenger(IEnumerable<IMessage> messages)
        {
            Messages = messages;
        }

        public void SendMessage()
        {
            Messages.ToList().ForEach(x => x.Send());
        }


        public void SendMessage(IEnumerable<IMessage> messages)
        {
            messages.ToList().ForEach(x => x.Send());
        }
    }
}
