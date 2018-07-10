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

Use "bharat_url_filter" environment variable or app settings for filtering urls by regex.

All logs are stored at "~/ASP_LOGS/".
