# ASPNetRequestLogger
Simple to use request logger for Asp.Net.

## Usage
### Web.config

For classic:
```xml
<system.web>
  <httpModules>
    <add name="BH" type="Bharat.ASPNetRequestLogger.TextLoggerModule, Bharat.ASPNetRequestLogger" />
  </httpModules>
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

Use "asp_url_filter" environment variable or app settings for filtering urls by regex.

All logs are stored at "~/ASP_LOGS/".
