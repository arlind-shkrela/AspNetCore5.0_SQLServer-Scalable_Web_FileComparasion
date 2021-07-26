## AspNetCore5.0_SQLServer-Scalable_Web_FileComparasion

Scalable Web FileComparasion is the asp.net core 5.0 web api solution, with an SQL Server Database with the purpose to expose endpoints for comparing base64 json encoded in two different endpoints and providing the result in separate endpoint. <br /> 
The Solution uses Entity Framework Core code first for continuing schema generation and updates. Generic interfaces, inheritance etc.<br />
Other Library included: AutoMapper

Hosted in Azure Web App service together with SQL Server db: https://scalableweb.azurewebsites.net/swagger/index.html

To Set the environment locally: 
- Just clone it, you can access the Azure SQL publicly. You can also use the credentials in appsetting.json to connect through ssms.
- If you insist to use you own db: Update the appsetting.json pointing you db and from Package manager Console execute update-databases 

Included in the solution: 
- Unit Tests
- Integration Tests

Functional Description:
Imagine you have the following image,
![alt text](https://arlindsite.blob.core.windows.net/$web/FileCompare.png)<br />
The user write/paste on the left part his text and the right side the text he wants to compare with. A JS make async calls to api, sending base64 file for "left" and "right", and with id created by the client can GET the differences. 
- Id is set by the client
- Update on that Id happens continually if the user continues to update the "left" or the "right" textboxes.  

Example: <br />
POST v1/diff/left: "Hello there" <br />
POST v1/diff/left: "Hello  therW" <br />
Result: GET: "Hello**ther<i>W<i>e" <br />
  
Feel Free to use it/ or parts of it in your project. <br />
Arlind
