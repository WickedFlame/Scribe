WickedFlame.Scribe
=================

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