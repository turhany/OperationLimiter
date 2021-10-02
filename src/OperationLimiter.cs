using System; 
using System.Threading.Tasks;
using OperationLimiter.Extensions;

namespace OperationLimiter
{
    public class OperationLimiter
    {
        private long _rateLimitedSecond = DateTimeOffset.Now.ToUnixTimeSeconds();
        private long _counter = 1;
        private readonly int _requestLimit;
        private readonly OperationLimitType _operationLimitType;

        public OperationLimiter(int requestLimit, OperationLimitType operationLimitType)
        {
            _requestLimit = requestLimit;
            _operationLimitType = operationLimitType;
        }
        
        public async Task LimitAsync()
        {
            var delay = TryToIncreaseCounter(DateTimeOffset.Now, _requestLimit);
            
            if (_counter == _requestLimit)
                await Task.Delay(TimeSpan.FromMilliseconds(Math.Abs(delay)));
        }
        
        private long TryToIncreaseCounter(DateTimeOffset now, long currentLimit)
        {
            if (_rateLimitedSecond == now.ToUnixTimeSeconds())
            {
                _counter++;
                
                if (_counter < currentLimit) 
                    return 0L;
                
                var nextSecondStart = new DateTimeOffset();
                switch (_operationLimitType)
                {
                    case OperationLimitType.Second:
                        nextSecondStart = now.Truncate(TimeSpan.FromSeconds(1)).AddSeconds(1);
                        break;
                    case OperationLimitType.Minute:
                        nextSecondStart = now.Truncate(TimeSpan.FromMinutes(1)).AddMinutes(1);
                        break;
                    case OperationLimitType.Hour:
                        nextSecondStart = now.Truncate(TimeSpan.FromHours(1)).AddHours(1);
                        break;
                }                
                
                return nextSecondStart.ToUnixTimeMilliseconds() - now.ToUnixTimeMilliseconds();
            }
            _rateLimitedSecond = now.ToUnixTimeSeconds();
            _counter = 1;
            return 0L;
        }
    }
}