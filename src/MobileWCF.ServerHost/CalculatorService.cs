using MobileWCF.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MobileWCF.ServerHost
{
    public class CalculatorService : ICalculatorService
    {
        public IAsyncResult BeginGetSum(int a, int b, AsyncCallback callback, object state)
        {
            Console.WriteLine("BeginGetSum: {0}+{1}", a, b);
            Thread.Sleep(2000);

            var tcs = new TaskCompletionSource<string>(state);
            var task = Task.Factory.StartNew(() => ((long)a + b).ToString());
            task.ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    Console.WriteLine("BeginGetSum: t.IsFaulted");
                    tcs.TrySetException(t.Exception.InnerExceptions);
                }
                else if (t.IsCanceled)
                {
                    Console.WriteLine("BeginGetSum: t.IsCanceled");
                    tcs.TrySetCanceled();
                }
                else
                {
                    tcs.TrySetResult(t.Result);
                }

                if (callback != null)
                {
                    callback(tcs.Task);
                }
                else
                {
                    Console.WriteLine("BeginGetSum: callback is null!");
                }
            });
            return tcs.Task;
        }

        public string EndGetSum(IAsyncResult asyncResult)
        {
            try
            {
                string result = ((Task<string>)asyncResult).Result;
                Console.WriteLine("EndGetSum: {0}", result);
                return result;
            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex);
                throw ex.InnerException;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex.InnerException;
            }
        }
    }
}
