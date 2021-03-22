# Example Console Application

You may use the provided example console application for reference on how to set up the project.

Dependency injection is configured for the console application in order to provide examples that can be used in Web API, Azure Function or other projects.

## Setting Up Test Credentials

To setup test credentials in Visual Studio:

1. Sign up for Reloadly and obtain your test credentials.
1. Open Reloadly.Console.Example.csproj in Visual Studio.
1. Right click on the project and choose "Manage User Secrets".
1. A JSON configuration file names 'secrets.json' should automatically open.
   This file is stored on your local machine on a location outside of the project.
1. Paste the following JSON snippet and replace placeholders with your credentials:

```javascript
{
  "Credentials": {
    "ClientId": "<paste your client id here>",
    "ClientSecret": "<paste your client secret here>"
  }
}
```
1. Run the project.