Scribe
=================
[![Build Status](https://travis-ci.org/WickedFlame/Scribe.svg?branch=master)](https://travis-ci.org/WickedFlame/Scribe)
[![Build status](https://ci.appveyor.com/api/projects/status/bxv7l0mb06wpej04/branch/master?svg=true)](https://ci.appveyor.com/project/chriswalpen/scribe/branch/master)

Scribe is a background Logger and Diagnostics component that collects, logs or delegates Log messages. Scribe can be completely configured in the application configuration without leaving any traces in the client code. 
Default or custom Loggers or Listeners can be applied through the configuration or at runtime.

```csharp
var loggerFactory = new LoggerFactory();

// add a log writer that Trace all Messages
loggerFactory.AddWriter(new TraceLogWriter());

// using the logger to log
var logger = loggerFactory.GetLogger();
logger.Write("test", LogLevel.Error);
```

Scribe can handle multiple Log Sources. For example all that is Traced to the Output to can be Logged with the help of the Scribe.TraceListener.  
All that has to be implemented for a Listener is the IListener interface.
```csharp
var manager = new LogManager();
manager.AddListener(new Scribe.TraceListener());

// using the trace-system to log
Trace.Write("Test");
Trace.TraceError("Error message");
```

The Listeners and Writers are configured per LoggerFactory instance.  
The Loggers that are created from the LoggerFactory all use the configuration defined in the LoggerFactory and can be created as many as desired.
  
The easiest way to setup and configure a logger is with the help of the LoggerConfiguration.
```csharp
var factory = new LoggerConfiguration()
    .AddTraceListener()
    .AddFileWriter(fileName: "logfile.log", formatString: "[{LogTime:d}] [{Message}]")
    .SetMinimalLogLevel(LogLevel.Warning)
    .CreateLogger();

// log the output from trace
System.Diagnostics.Trace.TraceError("Some error from trace");

// log through the default logger
var logger = factory.GetLogger();
logger.Write("Some error from Logger", LogLevel.Error);
```