#   **OperationLimiter**

![alt tag](https://raw.githubusercontent.com/turhany/OperationLimiter/main/img/operationlimiter.png)  

OperationLimiter is a simple time based operation limiter library.

[![NuGet version](https://badge.fury.io/nu/OperationLimiter.svg)](https://badge.fury.io/nu/OperationLimiter)  ![Nuget](https://img.shields.io/nuget/dt/OperationLimiter)

#### Features:
- Limit operation by Second (N times in one second)
- Limit operation by Minute (N times in one minute)
- Limit operation by Hour (N times in one hour)

#### Usages:
----- 
 
```cs
var operationLimiter = new OperationLimiter(2, OperationLimitType.Minute);

for (int i = 1; i <= 10; i++)
{
    Console.WriteLine($"Operation {i}: {DateTime.Now.ToLongTimeString()}");

    await operationLimiter.LimitAsync();
}
```
### Release Notes

#### 1.0.0
* Base Release
