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

        public void ThrowIfError()
        {
            if (State == BaseStatus.StateEnum.Error)
            {
                throw new Exception(Text, Exception);
            }
        }

        public override string ToString()
        {
            if (this.Exception != null)
            {
                return $"[Status {State}] {Text}" + Environment.NewLine + $"[Exception] {Exception.StackTrace}";
            }

            return $"[Status {State}] {Text}";
        }

        public enum StateEnum
        {
            Ok = 1,
            Error = 2
        }
    }
}
