# RSS Manager
Service updates the list of news from the feed and stores it in the database for later reading
### Features:
* User login and registration
* Adding RSS feed (by url)
* Getting all active RSS feeds
* Getting all unread news from some date (parameters: date)
* Setting news as read
### Technologies and requirements:
* ASP.NET Core REST API
* Three-layer architecture: Data access layer, Business logic, Web application as separate projects
* SQLite as a database, Entity Framework Core with code first approach
* Authentication/authorization using IdentityServer, JWT Bearer auth
* Global error handling
### Database schema:
![schema](https://drive.google.com/uc?export=view&id=1xY_lnekzk3QoTRs4VePyYe4QoZa2dKat)
