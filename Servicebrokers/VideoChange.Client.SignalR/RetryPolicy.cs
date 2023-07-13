using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trip.Client.SignalR
{
    class RetryPolicy : IRetryPolicy
    {
        private readonly Random _random = new Random();
        private int _timeLimit;

        public RetryPolicy(int timeLimit = 180)
        {
            _timeLimit = timeLimit;
        }

        public TimeSpan? NextRetryDelay(RetryContext retryContext)
        {
            // If we've been reconnecting for less than 60 seconds so far,
            // wait between 0 and 10 seconds before the next reconnect attempt.
            if (retryContext.ElapsedTime < TimeSpan.FromSeconds(_timeLimit))
            {
                return TimeSpan.FromSeconds(_random.NextDouble() * 10);
            }
            else
            {
                // If we've been reconnecting for more than timelimite in seconds so far, stop reconnecting.
                return null;
            }
        }
    }
}
