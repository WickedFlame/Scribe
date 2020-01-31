---
title: Home
nav_order: 0
isHome: true
---

Scribe
===================

Scribe is a background Logger and Dispatcher component for creating, manipulating and dispatching of Logs.

## Installation

## Usage
All you need is the Interface Scribe.ILogger
```csharp
public class MyClass
{
    public MyClass(ILogger logger)
    {
        logger.Write("A message for the log", level: LogLevel.Information);
    }
}
```
