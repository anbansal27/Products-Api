# Products API

Products Api is a simple ASP.Net Core Web API built to support CRUD operations for Products and Product Options.

The Api utilizes [Swagger for Api Specification](https://swagger.io/) and exposes following endpoints :
![alt text](https://github.com/anbansal27/Products-Api/blob/master/images/SwaggerApiSpecification.jpg)

## Design
The API is designed with the following considerations :
* Separation Of Concerns
* Request Validations using [Fluent Validation Rules](https://fluentvalidation.net/).
* Consolidated Error Handling for Clean Code using [Exception Filters](https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters?view=aspnetcore-3.1)
* Structured Logging using [SeriLog](https://serilog.net/)
* [Docker for Containerization](https://www.docker.com/)
* Object mapping using [AutoMapper](https://automapper.org/)
* Entity Framework Core - Code First
	* For clean code 
	* Greater control
	* Faster development speeds 
	* Database versioning
* Indexes for Database Query Performance
* Unit and Integration Testing using [Xunit](https://xunit.net/) and [Api based Integration Tests](https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-3.1)
* Test Assertions using [Fluent Assertions](https://fluentassertions.com/)
* Code Coverage reports using Coverlet(https://dotnetfoundation.org/projects/coverlet)
* Api design and documentation using [Swagger](https://swagger.io/)
* SqlLite database is used for Simplicity

## Deployment - AWS
Application is deployed on AWS and can be accessed at - 
http://products-2010248864.ap-southeast-2.elb.amazonaws.com/swagger/index.html

## Some uplifting that can be done going forward :
* [Api Authentication and Authorization](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/secure-net-microservices-web-applications/)
* Cloud Provisioning using IAC tools like [CloudFormation](https://aws.amazon.com/cloudformation/) or similar.
* CI/CD DevOps Pipeline using tools like [Jenkins](https://www.jenkins.io/) or similar.
* Monitoring and Dashboards for Observability using tools like [CloudWatch](https://aws.amazon.com/cloudwatch/), [Grafana](https://grafana.com/) or similar.
* Database Migration on Cloud.

## Solution Structure 
![alt text](https://github.com/anbansal27/Products-Api/blob/master/images/SolutionStructure.jpg)

* Products.Api - This is the host project and contains the following :
	* Api Configuration files
	* Startup files 
	* Extensions for setting up Swagger
	* Products Controller to handle requests
	* Log files - generated when the app executes

* Products.Api.Application - Core project containing the Business Logic for :
	* Product Service to handle and process requests from the Products Controller 
	* Request Validations using Fluent Validation Rules
	* Custom Validation Exceptions thrown from the Product Service
	* Validation and Exception Filters to handle the validation failures and thrown exceptions.
	* AutoMapper Profiles to map database objects with the api requests/responses.
	* Api Responses/DTOs

* Products.Api.Entities - .Net Standard Library for Product and ProductOption Entity Models.

* Products.Api.Data - Project for Database Context to allow accessing and updating the data. 	
	* EF Migrations - To create the database
	* Database Extensions - Used to migrate the database when the app starts.
	* Repositories - To provide access to the data

* Products.Api.UnitTests - Tests project for Unit Tests and Behavior driven Tests
	* This project test the Request Validators using Xunit Facts and Theories.
	* Code coverage reports can be generated using Coverlet

* Products.Api.IntegrationTests - 
	* This project tests various behaviors for all controller actions. 
	* The project uses a InMemory Database to perform end to end Integration Testing.
	* Code coverage reports can be generated using Coverlet
	
* Dockerfile - Facilitate running the API on Docker containers

## Code Flow
* When the app starts (for the first time) the database is created using Entity framework migrations.   
* The incoming request is first validated by the `ProductValidator` and `ProductOptionValidator` wherever applicable.
![alt text](https://github.com/anbansal27/Products-Api/blob/master/images/Validation.jpg)
* `ProductsController` then receives the request and forwards it to the `ProductService`.
* The `ProductService` then performs validations based on business logic and throws appropriate exceptions (in case of validation failures) or processes the request.
![alt text](https://github.com/anbansal27/Products-Api/blob/master/images/ServiceBehavior.jpg)
* In case of exceptions, the `ExceptionFilter` handles the exceptions, logs them and returns appropriate error results.
![alt text](https://github.com/anbansal27/Products-Api/blob/master/images/ExceptionHandling.jpg)
* If there are no exceptions, the `ProductService` utilizes the CRUD operations exposed by `ProductRepository` to manage Products and Product Options.
* Finally the formatted response is returned by the `ProductService` to the Controller which then returns it back to the user.

## Steps to Build and Run

* Clone the repository from - [GitHub](https://github.com/anbansal27/Products-Api.git)
* Start the Api from `\src\Products.Api` using the below command : 
```
dotnet run
```
![alt text](https://github.com/anbansal27/Products-Api/blob/master/images/ExecuteApp.jpg)
* Alternatively run the `Products.Api` from Visual Studio 
* Once up, the Api can be tested using Swagger
```
http://localhost:5000/swagger
```
## Running on Containers
### Prerequisites
* Install Docker from https://docs.docker.com/get-docker/

### Run
* Open a command prompt and navigate to the project directory `\Products-Api`
* Execute the following command to build the docker image
```
docker build -t products .

```
![alt text](https://github.com/anbansal27/Products-Api/blob/master/images/BuildDockerImage.jpg)

* Once the image is successfully built, execute the following command to run the container
```
docker run -d -p 5000:80 --name productsApi products

```
![alt text](https://github.com/anbansal27/Products-Api/blob/master/images/RunDockerContainer.jpg)

Once up, the Api can be tested using Swagger

```
http://localhost:5000/swagger
```
## Running Tests and Generating Coverage Report
* Install Report Generator tool by running `dotnet tool install -g dotnet-reportgenerator-globaltool`
* Run the Unit or Integration Tests from their respective project directory - `\tests\Products.Api.IntegrationTests` using the following command
```
dotnet test /p:CollectCoverage=true /p:CoverletOutput=TestResults/ /p:CoverletOutputFormat=cobertura
```
![alt text](https://github.com/anbansal27/Products-Api/blob/master/images/IntegrationTests.jpg)

* This will generate `TestResults` folder and coverage file.
* Now run the following command to generate coverage report in `\tests\Products.Api.IntegrationTests\coveragereport` folder (Please update the command below with appropriate path)

```
reportgenerator "-reports:C:\git\Products-Api\tests\Products.Api.IntegrationTests\TestResults\coverage.cobertura.xml" "-targetdir:coveragereport" -reporttypes:Html
```

![alt text](https://github.com/anbansal27/Products-Api/blob/master/images/IntegrationTestsCoverage.jpg)

* The Coverage report can be viewed by opening the `index.html` file in the browser from `\tests\Products.Api.IntegrationTests\coveragereport` folder.

Generated Reports - 
* Integration Tests Coverage
![alt text](https://github.com/anbansal27/Products-Api/blob/master/images/IntegrationTestsReport.jpg)

* Unit Tests Coverage (Unit tests cover parts of code not covered by Integration tests)
![alt text](https://github.com/anbansal27/Products-Api/blob/master/images/UnitTestReport.png)
