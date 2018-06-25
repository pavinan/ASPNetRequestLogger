# ASPNetRequestLogger
Simple to use request logger for Asp.Net.

## Usage
### Web.config

For classic:
```xml
<system.web>
  <modules>
    <add name="BH" type="Bharat.ASPNetRequestLogger.TextLoggerModule, Bharat.ASPNetRequestLogger" />
  </modules>
</system.web>
```
For integrated:
```xml
<system.webServer>
    <modules>
      <add name="BH" type="Bharat.ASPNetRequestLogger.TextLoggerModule, Bharat.ASPNetRequestLogger" />
    </modules>
</system.webServer>
```