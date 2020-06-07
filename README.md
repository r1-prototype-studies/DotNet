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

