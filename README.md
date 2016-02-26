# _Hair Salon_

#### Database for Stylists and Clients using C#, Nancy, and Razor 2/26/2016

#### By _**Veronica Alley**_

## Description

_Web App and database that allows user to add stylists and assign clients to each stylist_

## Setup/Installation Requirements

* Download zip
* In SQLCMD:

  CREATE DATABASE hair_salon;
  
  GO
  
  USE hair_salon;
  
  GO
  
 CREATE TABLE stylists (id INT IDENTITY(1,1), name VARCHAR(255));
 
 CREATE TABLE clients (id INT IDENTITY(1,1), name VARCHAR(255), stylist_id VARCHAR(255));

 GO
* navigate to directory in command line
* Run dnu restore and dnx kestrel in unzipped directory
* navigate to localhost:5004 in browser
* enter text into the inputs

## Technologies Used

_C#, Nancy, Razor_

### License

Copyright (c) 2015 **_Veronica Alley_**
