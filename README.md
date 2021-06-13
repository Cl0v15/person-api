# person-api

### Setup local environment

- Clone the Git repo https://github.com/Cl0v15/person-api.git
- In appsettings.json, update the connection string by replacing ```[IPv4]``` by your IP.
- Run ```docker-compose up``` at the root of the solution to create the API and Database containers and run the DB migration

### API endpoints

- To call the Persons API, get a JWT by calling GET /api/identity
- Add the JWT in the header "Authorization" (Bearer "your token") of the request
- To view the API logs, run: ```docker logs [container name]```

```
- GET /api/identity
- GET /api/persons
- GET /api/persons/{personId}
- POST /api/persons
  {
    "firstName": "John",
    "lastName": "Doe",
    "dateOfBirth": "1975-06-13T00:00:00",
    "salutation": 0
  }
- PUT /api/persons/{personId}
  {
    "firstName": "Jon",
    "lastName": "Do",
    "dateOfBirth": "1975-06-13T00:00:00",
    "salutation": 3
  }
- DELETE /api/persons/{personId}
```
