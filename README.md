WickedFlame.Scribe
=================
[![Build Status](https://travis-ci.org/WickedFlame/Scribe.svg?branch=master)](https://travis-ci.org/WickedFlame/Scribe)
[![Build status](https://ci.appveyor.com/api/projects/status/bxv7l0mb06wpej04/branch/master?svg=true)](https://ci.appveyor.com/project/chriswalpen/scribe/branch/master)

Scribe is a background Logger and Diagnostics component that collects and logs Trace messages. Scribe can be completely configured in the application configuration without leaving any traces in the client code. 
Default or custom Loggers can be applied through the configuration or at runtime.

```csharp
var manager = new LogManager();
manager.AddListener(new TraceLogListener());

// using the trace-system to log
Trace.Write("Test");
```
```csharp
var loggerFactory = new LoggerFactory();
loggerFactory.AddLogger(new TraceLogWriter(), "tracelogger");

// using the custom logger to log
var logger = loggerFactory.GetLogger();
logger.Write("test", LogLevel.Error);
```