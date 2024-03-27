# FinancialMonkey

If monkeys can throw darts, they can invest. This project is a system for managing financial products as administrators and registering investments wallets as customers.

## Architecture

Following is a digram that illustrates the whole picture.

![FinancialMonkey architecture](docs/architecture.jpg)

Internet traffic reaches an API Gateway or reverse proxy as a common entrypoint. This common entrypoint routes/redirects requests to the responsible service. Services are:

- **IdentityService**: a web API that uses [DuendeSoftware IdentityServer](https://duendesoftware.com/products/identityserver) to perform authentication of users and services. It signs and issues JWT tokens that are used as the main authorization mechanism elsewhere in the system. To store credentials a MariaDB SQL database is used.
- **AuthService**: a web API that is used as an authentication BFF to hide and abstract client credentials from users requesting tokens. It requests the IdentityService for tokens. Both admin and customer users use this web API to get an auth token.
- **FinancialProductsService**: this is the core service, is used by admins and customers. Customers can list financial products that are available to their profile, get more info about a product, register purchases and sell of financial products and get their wallet status.

## Running

There is a [docker compose file](./docker-compose.yaml) ready to be used. All that is needed is to run it from the repository root so that config files are correctly mounted inside the containers that need them.

The compose file spins up the needed containers for:

- MariaDB server;
- MongoDB server;
- IdentityServer;
- AuthService;
- FinancialProductsService;
- Database migrations (it executes the needed migrations then exits);
- Kong API Gateway;

It runs in network host mode, that is needed so that it is possible to use IdentityServer without HTTPS (believe me, configuring linux to trust .NET certs is a pain in the ass). Due to that some ports are needed in the host machine:

- 9900 for MariaDB server;
- 9901 for the migrations container;
- 9902 for IdentityServer;
- 9903 for AuthService;
- 9904 for MongoDB server;
- 9905 for FinancialProductsService;
- some ports in the range 8000 to 8444 for Kong API Gateway;

Once everything is up, one can access the API gateway in http://localhost:8000. Each service has its own swagger UI endpoint that is accessible from the API Gateway:

- /auth/swagger for the AuthService swagger;
- /financialproducts/swagger for the FinancialProductsService swagger;

## Disclaimer

This is not a real product, it is just a silly project that is not supposed to run in production.
