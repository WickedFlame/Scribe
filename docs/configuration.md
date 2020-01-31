---
title: Configuration
nav_order: 2
isHome: true
---

# Scribe
The easiest way to setup and configure a logger is with the help of the LoggerConfiguration.

## LoggerConfiguration

```csharp
var factory = new LoggerConfiguration()
    .AddFileWriter(fileName: "logfile.log", formatString: "[{LogTime:d}] [{Message}]")
    .SetMinimalLogLevel(LogLevel.Warning)
    .CreateLogger();

// log through the default logger
var logger = factory.GetLogger();
logger.Write("Some error from Logger", LogLevel.Error);
```