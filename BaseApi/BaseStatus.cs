using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Api
{
    public class BaseStatus
    {
        public StateEnum State { get; set; }
        public string Text { get; set; }

        public Exception Exception { get; set; }

        public BaseStatus()
        {

        }

        public BaseStatus(StateEnum state, string text)
        {
            this.State = state;
            this.Text = text;
        }

        public BaseStatus(StateEnum state, string text, Exception exception)
        {
            this.State = state;
            this.Text = text;
            this.Exception = exception;
        }

        public enum StateEnum
        {
            Ok = 1,
            Error = 2
        }
    }
}
