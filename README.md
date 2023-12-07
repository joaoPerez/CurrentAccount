# CurrentAccount

- The projects into this solution follow clean architecture pattern.
- This project have been built with one central Api the is the CurrentAccount and the other for Transactions.
- Transactions.API are deactivated to work with the Grpc project, they cannot work at same time because the in memoty database.
- In order to start the service, into 'src' directory run the command docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
- A very simple html is into the root folder, the is TestPage.html, you can run the operations there.
- You need to use an already created customer to make the operations. 
- The Guid is: 3fa85f64-5717-4562-b3fc-2c963f66afa6
- You can choose to use the initial credit.
- The system does not use the Business Customer in this example, I used it only to modelling the domain.
- Also not using data filters for the get transactions because it is not going to have much data in memory.
- The architecture are with microsservices approach with each service with their own database. 
- To read the data in a large scale system using relations that should be necessary to send the data to a data lake or data warehouse
or to use only one database and share between the services.
- Not all scenarios are tested, I'm focusing in ones that I think are the most important.
- I also should validate and handle the different currencies, but I'm not managing it now.
- The system uses hybrid architecture with gRPC and RabbitMQ messaging. To show different ways to work.

- Url: localhost:8000 for all operations
 localhost:8000/swagger, to use directly.

- Tools and extensions:

Entity Framework for the in memory database
RabbitMQ with MassTransit to event driven
Grpc for syncronous messaging


- RabbitMQ username: guest password: guest http://localhost:15672/
Using publish subscriber method because that allows multiple Api to get the same event.