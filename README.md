# Fetch

This project is built on .net core and as such will require the .net sdk to build project and for the cli provied by this distribution.

Steps to run:

1. Pull the git repo down.
2. Install [dotnet sdk](https://dotnet.microsoft.com/en-us/download).
3. Once dotnet has been installed navigate to the repository and use `dotnet build` followed by `dotnet run` in your terminal of choice.
4. Now that the server is running you can navigate to `localhost:5000/swagger`. This will bring you to a swagger page that will display all the endpoints and schema for you use and to run the various endpoints.

Endpoint Descriptions:

`http:localhost:5000/Transactions/AddTransaction` can be used to add point transactions. Note transactions with the same timestamp will cause collision errors.

`http:localhost:5000/transactions/Spend` can be used to send a spend request. This will return a list of the payers and the points that were used.

`http:localhost:5000/transactions/GetPayerPointTotals` will return a list of payers and there total unspent points.
