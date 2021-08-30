<center><h1>.Net Prototypes</h1></center>

- [Tips](#tips)
- [MVC](#mvc)
- [Web Api](#web-api)
  - [Get Files from WebApi](#get-files-from-webapi)
    - [Testing Links](#testing-links)
    - [References](#references)
  - [Self Host WebApi](#self-host-webapi)
    - [Notes](#notes)
    - [Nugets](#nugets)
    - [Testing links](#testing-links-1)
    - [References](#references-1)
- [Microservices - Shopping Cart](#microservices---shopping-cart)
  - [Notes](#notes-1)
  - [Steps](#steps)
  - [Nuget packages](#nuget-packages)
  - [Docker images](#docker-images)
  - [References](#references-2)

# Tips
1. Right-click and select Markup TOC Insert/update. Delete and update it
2. Each First heading is a solution

# MVC


# Web Api
## Get Files from WebApi
### Testing Links
- https://localhost:44307/ebook/doc
- https://localhost:44307/ebook/pdf
- https://localhost:44307/ebook/excel
- https://localhost:44307/ebook/zip
  

### References
1. https://www.c-sharpcorner.com/article/sending-files-from-web-api/


## Self Host WebApi
Hsoting a WebAPI project in a console application

### Notes
- Create a console application
- Install the nuget package
- Create Controller and Model classes
- Configure self host in program.cs
- Write Console application to get data from the WebAPI
  
### Nugets
-   Microsoft.AspNet.WebApi.SelfHost --> Web API
-   Microsoft.AspNet.WebApi.Client --> Client 

### Testing links
- http://localhost:8080/api/book?author=aroan
- http://localhost:8080/api/book/1
- http://localhost:8080/api/book
  
### References
1. https://www.c-sharpcorner.com/UploadFile/a6fd36/understand-self-host-of-a-web-apiC-Sharp/
2. https://www.c-sharpcorner.com/uploadfile/a6fd36/understanding-how-to-call-the-web-api-from-a-client-applica/

# Microservices - Shopping Cart 
## Notes
* The different layers are 
  * API / Application / Presentation Layer
    * Entry point into the application 
    * Exposes endpoints / UI and enforces validation
    * No Business Logic
  * Domain Model Layer
    * Heart of the software
    * Contains Business rules, logic and operations
  * Infrastructure Layer
    * Provides infrastructure plumbing
    * Primary responsibility is persistence of business state
* ProducesResponseType &rarr; used as an attribute which is used in documentation like swagger providing information like response http status code and the return object.
  ``` CSharp
  [ProducesResponseType((int)HttpStatusCode.NotFound)]
  [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
  ```
* ``depends_on`` in docker file does not make sure that the dependant container is running properly. It just ensures that the dependant containers are started first.
* Use mongoclient docker image to view the mongo data in GUI
    ``` powerShell
    docker run -d -p 3000:3000 mongoclient/mongoclient
* Redis &rarr; REmote DIctionary Server
* The below command is used to connect to redis in the bash
    ``` powerShell
    redis-cli
* Command Redis command
    ``` powerShell
    ping
    set
    get
    ```
* Docker commands
    ``` powerShell
    docker exec -it shoppingcart-redis /bin/bash
* Portainer is a container management tool.
  * credentials: admin/password
* pgadmin is used for postgresql administration.
  * credentials: admin@admin.com/password
* gRPC &rarr; Google Remote Procedure Call
* gRPc uses HTTP/2 protocol and it is non-browser at the moment.
* 




## Steps
1. Download the code from https://github.com/aspnetrun/run-aspnetcore-microservices
2. Extract and traverse to that path (src folder which has the docker file) in Powershell
3. Run the below command in Powershell to build and run the docker image
   ``` csharp
   docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
   ```
4. Create a new project Catalog.API with the template ASP.NET Core Web API.
5. Change the running port and url
   * Right-click on the project and go to properties.
   * Go to Debug tab.
   * Select Catalog.API from the profile.
   * Check the app url at the bottom.
   * The new profile gets added in the launchsettings.json in properties folder.
6. Setup Mongo docker database
    * Go to [hub.docker.com](https://hub.docker.com/)
    * Search for Mongo official database
    * Right-click the solution and click on Open in Terminal. This will open up the Dveloper Powershell in the visual studio
    * Run the below command to check whether any docker image is present and running
      ``` powerShell
      docker ps
    * Run the below command to pull the docker image
      ``` powerShell
      docker pull mongo
    * Run the below command to run the docker image
      ``` powerShell
      docker run -d -p 27017:27017 --name shopping-mongo mongo
      ```
      -d &rarr; runs the docker image in detached mode
      -p {image port in the container}:{forwarding port to the local computer}
    * Run the below command to check the logs
      ``` powerShell
      docker logs -f shopping-mongo
    * Run the below command to get into interactive mode to check the mongo db
      ``` powerShell
      docker exec -it shopping-mongo /bin/bash
      ```
      -it &rarr; interactive mode
      /bin/bash &rarr; runs bash
      * Run the below command to get the list of databases
        ``` bash
        show dbs
        ```
      * Run the below command to create the catalog database
        ``` bash
        use CatalogDb
        
        db.createCollection('Products')

        db.Products.insertMany([{ 'Name':'Asus Laptop','Category':'Computers', 'Summary':'Summary', 'Description':'Description', 'ImageFile':'ImageFile', 'Price':54.93 }, { 'Name':'HP Laptop','Category':'Computers', 'Summary':'Summary', 'Description':'Description', 'ImageFile':'ImageFile', 'Price':88.93 } ])

        db.Products.find({}).pretty()

        db.Products.remove({})

        show databases
        
        show collections
        
        db.Products.find({}).pretty()
        ```
7. Install MongoDB.Driver nuget package
8. Create docker compose file 
    * Right-click the project
    * Go to Add > Add container Orchestrator support 
      * Select Docker compose and click ok
      * Select Target OS as Linux
    * DockerFile will be created in the project
    * Docker compose will be created in the solution level
9. Execute the below command to run the docker container
    ``` powerShell
     docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up -d
     ```
10. Execute the below command to stop the docker container.
    ``` powerShell
    docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml down
    ```
11. Create a new project Basket.API with the template ASP.NET Core Web API.
12. Change the running port and url
     * Right-click on the project and go to properties.
     * Go to Debug tab.
     * Select Basket.API from the profile.
     * Check the app url at the bottom.
     * The new profile gets added in the launchsettings.json in properties folder.
13. Execute the below command to pull Redis
    ``` powerShell
    docker pull redis
14.  Run Redis container using the following command:
      ``` powerShell
      docker run -d -p 6379:6379 --name shoppingcart-redis redis
15.  Install the nuget package - Microsoft.Extensions.Caching.StackExchange
16.  Install the nuget package - Newtonsoft.Json
17.  Create docker compose file 
     * Right-click the project
     * Go to Add > Add container Orchestrator support 
       * Select Docker compose and click ok
       * Select Target OS as Linux
     * DockerFile will be created in the project
     * Docker compose will be updated
18. Execute the below command to run the docker container
    ``` powerShell
     docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up -d
     ```
19. Create a new project Discount.API with the template ASP.NET Core Web API.
20. Change the running port and url
    * Right-click on the project and go to properties.
    * Go to Debug tab.
    * Select Discount.API from the profile.
    * Check the app url at the bottom.
    * The new profile gets added in the launchsettings.json in properties folder. 
21. Execute the below command to pull postgresql
    ``` powerShell
    docker pull postgres
22. Execute the below command to pull pgadmin
    ``` powerShell 
    docker pull dpage/pgadmin4
23. Create coupon table in postgresql database using pgadmin
    ``` sql
    CREATE TABLE Coupon(
      ID SERIAL PRIMARY KEY NOT NULL,
      ProductName Varchar(24) NOT NULL,
      Description TEXT,
      Amount INT
    );

    INSERT INTO Coupon (ProductName, description, amount) VALUES ('IPhone X', 'IPhone Discount', 150);

    INSERT INTO Coupon (ProductName, description, amount) VALUES ('Samsung 10', 'Samsung Discount', 100);
24. Install the nuget package - Dapper
25. Install the nuget package - Npgsql
26. Create docker compose file 
     * Right-click the project
     * Go to Add > Add container Orchestrator support 
       * Select Docker compose and click ok
       * Select Target OS as Linux
     * DockerFile will be created in the project
     * Docker compose will be updated
27. Execute the below command to run the docker container
    ``` powerShell
     docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up -d
     ```
28. Create a new project Discount.Grpc with the template ASP.NET Core gRPC.
29. Change the running port and url
    * Right-click on the project and go to properties.
    * Go to Debug tab.
    * Select Discount.API from the profile.
    * Check the app url at the bottom.
    * The new profile gets added in the launchsettings.json in properties folder. 
30. Install the nuget package - Dapper
31. Install the nuget package - Npgsql
32. Go to properties of the Discount.proto file
    *  Change the build action to Protobuf compilers
    *  Change gRPC stub classes to Server Only
33. Open Discount.Grpc.csproj file
    * Remove the below lines if present
      ```xml      
      <ItemGroup>
        <None Remove="Protos\discount.proto" />
      </ItemGroup>
34. Build the project to generate the gRPC files in ~\Discount.Grpc\obj\Debug\net5.0\Protos\
35. Install the nuget package - AutoMapper.Extensions.Microsoft.DependencyInjection
36. 


## Nuget packages
1. MongoDB.Driver
2. Microsoft.Extensions.Caching.StackExchange
3. Newtonsoft.Json
4. Dapper
5. Npgsql
6. AutoMapper.Extensions.Microsoft.DependencyInjection

## Docker images
1. mongo
2. mongoclient
3. redis
4. portainer/portainer-ce
5. postgres
6. dpage/pgadmin4

## References
* https://github.com/aspnetrun
* https://github.com/aspnetrun/run-aspnetcore-microservices
* https://stackoverflow.com/questions/62441307/how-can-i-change-the-location-of-docker-images-when-using-docker-desktop-on-wsl2
* https://newbedev.com/how-can-i-change-the-location-of-docker-images-when-using-docker-desktop-on-wsl2-with-windows-10-home
* https://documentation.portainer.io/quickstart/
* https://www.pgadmin.org/