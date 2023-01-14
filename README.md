# ToyWorld

Toyworld is an app for toy exchange. Here you can buy and sell preloved toys.
 
 ## Scenario
During my days in salt bootcamp, we had a personal hack day. We had to create a full-stack application in one day. I was brainstorming for ideas and decided to address a real life problem. Being a parent, one of the challenges I am facing is, my kid getting bored of his toys. Our house is filled with a million toys and he still want new ones. It was costing us money and space. Thats how i came up with this idea of toy swapping.
      
## Toyworld API
 This repo contains the backend part of this app.
 You can find the front-end part here: https://github.com/dhyanswathi/ToyWorld-Front-end
 
 ## Setup and tools
 You need to have docker installed. Clone this repo.
 Run this command in the terminal.
 ```
 docker-compose up -d
 ```
 This will start the database. The first time you run this command it will take about 1-5 minutes. But then it will be lightning fast.

The credentials for the `sa`-account is found in the `docker-compose.yml`-file.

Once the `docker-compose` command has finished you can use Azure Data Studio (should also be installed on your computers) to access the database with those credentials.

### Shutting the database down

Note the `-d` in the command above. This means that the docker container will run in the background. You can see it through the Docker client but other than that it's hidden.

But you want to shut the database down. This can be done through:

```bash
docker stop sql-server-db
```

Note that the database is held in the container so when you shut it down the data is gone.

## Running the API

Cd into the ToyWorld.API folder. Run this command for starting the API.
```
dotnet run
```
## Running the app
After you have the API and DB up and running, go to the frontend repo (https://github.com/dhyanswathi/ToyWorld-Front-end) and follow the instructions in readme file there.

Happy Browsing!
