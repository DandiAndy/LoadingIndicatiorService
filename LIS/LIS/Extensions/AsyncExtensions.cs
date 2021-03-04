using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LIS.Extensions
{
    public static class AsyncExtensions
    {
        public static async Task WaitUntil(Func<bool> condition, int frequency = 25, int timeout = -1)
        {
            Task waitTask = Task.Run(async () =>
            {
                while (!condition())
                {
                    await Task.Delay(frequency);
                }
            });

            if(waitTask != await Task.WhenAny(waitTask, Task.Delay(timeout)))
            {
                throw new TimeoutException();
            }
        }
    }
}
