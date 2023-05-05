# ToyWorld

Toyworld is an app for toy exchange. Here you can buy and sell preloved toys.
 
 ## Scenario
During my days in salt bootcamp, we had a personal hack day. We had to create a full-stack application in one day. I was brainstorming for ideas and decided to address a real life problem. Being a parent, one of the challenges I am facing is, my kid getting bored of his toys. Our house is filled with a million toys and he still want new ones. It was costing us money and space. Thats how i came up with this idea of toy swapping.
      
## Toyworld API
 This repo contains the backend part of this app.
 You can find the front-end part here: https://github.com/dhyanswathi/ToyWorld-Front-end.
 I used DB-first approach in the project. Tables were created in database and Entity data model is generated from it vis reverse engineering.

## Tech-stack & Tools
C#, ASP.Net WebAPI, Entity Framework, SQL Server, Docker for hosting DB, Visual Studio, Azure Data Studio  

## Setup 
 You need to have docker installed. Clone this repo.
 Run this command in the terminal.
 ```
 docker-compose up -d
 ```
 This will start the database.

The credentials for the `sa`-account is found in the `docker-compose.yml`-file.

Once the `docker-compose` command has finished you can use Azure Data Studio (should also be installed on your computers) to access the database with those credentials.
Note that the database is held in the container so when you shut it down the data is gone.

## Running the API

Cd into the ToyWorld.API folder. Run this command for starting the API.
```
dotnet run
```
## API Endpoints
  ## Toys

**GET** */Toys* - This endpoint **Gets** all toys stored in our database.<br>
**GET** */Toys/{id}* - This endpoint **Gets** one specific toy using the toy's id.<br>
**POST** */Toys* - This endpoint **Posts** allows to add a toy and store it in the database.<br>
**PUT** */Toys/{id}* - This endpoint **Edits** one specific toy using the toy's id.<br>
**DELETE** */Toys/{id}* - This endpoint **Deletes** one specific toy using the toy's id<br>
**POST** */Toys/sendEmail* - This endpoint allows to send email to the person who posted the toy showing the interest to exchange toy<br>

## Running the app
After you have the API and DB up and running, go to the frontend repo (https://github.com/dhyanswathi/ToyWorld-Front-end) and follow the instructions in readme file there.

Happy Browsing!
