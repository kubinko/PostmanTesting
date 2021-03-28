# PostmanTesting
Sample project for demonstrating API testing with Postman.

## Contents
### Identity server
Project for authorization and authentication of custom API. Standalone [Identity server](https://identityserver4.readthedocs.io) is used.  
Default local address for identity server is _http://localhost:5001_.

Config:
- scope: _"api1"_
- client:
  - client_id: _"client"_
  - grant_type: _"password"_
  - allowed scopes: _"api1"_, _"openid"_, _"profile"_, _"role"_
- users:
  - alice/password (admin user)
  - bob/password (common user)
  
### Postman Testing
Sample API for manipulation of workshops and its attendees. Default local address for this API is _http://localhost:5000_.  
Complete API documentation can be found at _http://localhost:5000/swagger_ after staring the service locally.  

In order to use, you have to set following values in appconfig.json:  
```
ApiJwtAuthorization.Authority - URL of used identity server (use "http://localhost:5001" for default Identity Server)
ConnectionStrings.DefaultConnection - connection string for Microsoft SQL database that will hold the data
```

### Invoice Maker
Azure Function that mocks generating of invoices for workshop attendees. The function is run every 15 seconds, checks for workshop attendee records without generated invoices and marks invoice generated flag in them.
In order to use locally, you have to add file _local.settings.json_ with following contents:
```javascript
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "<Azure Storage connection string>",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "ConnectionString": "<Microsoft SQL database connection string>"
  }
}
```
Note: You can use [Azure Storage Emulator](https://docs.microsoft.com/en-us/azure/storage/common/storage-use-emulator).

### Tests
Exported Postman test collections for API.
