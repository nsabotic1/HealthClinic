# HealthClinic
-------------------------
HealthClinic is a software system for a small clinic that records, reviews and writes medical findings for admissions of registered patients by registered doctors.
For this purpose, the application contains:
1. Records of patients
2. Records of doctors
3. Records of patient admissions. 

Each patient admission includes:
- Date and time of patient admission
- A patient
- A doctor
- Emergency patient admission (Yes/No)
4. Record of medical findings for recorded admission of the patient. 

Each medical report has:
- Textual description of medical findings
- Date and time of creation of the medical report

### Run and build

* Install .NET 6 SDK.
* Install the following packages:
  * Microsoft.EntityFrameworkCore (7.0.0)
  * Microsoft.EntityFrameworkCore.Tools (7.0.0)
  * Microsoft.EntityFrameworkCore.Design (7.0.0)
  * Microsoft.EntityFrameworkCore.SqlServer (7.0.0)
  * AutoMapper.Extensions.Microsoft.DependencyInjection (12.0.0)
  * AutoMapper (12.0.0)
  * Swashbuckle.AspNetCore
* Run `update-database` for a database to be made
* Note: Please check your server name (mine is (localdb)\MSSQLLocalDB)).
 In case that your name is different than mine, change the server name
 to your server name in DataContext.cs, line 23

