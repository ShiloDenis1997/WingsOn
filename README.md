# WingsOn
Endpoints are available via Swagger ```https://localhost:7299/swagger/index.html```
1. Endpoint that returns a Person by Id. 
    1. ```'https://localhost:7299/people/69'```
2. Endpoint that returns all passengers on the flight ‘PZ696’.
    1. First call to find flight ```'https://localhost:7299/flights?number=PZ696'```
    2. Second call with some id from previous call will return all passengers related to flight ```''```
3. Endpoint that lists all the male passengers.
    1. ```'https://localhost:7299/people?gender=0'```
4. BONUS: Endpoint that creates a booking of an existing flight for a new passenger.
    1. POST to ```'https://localhost:7299/bookings'``` with the following body creates new booking for two new people and one existing
    ```
    {
      "number": "1231277",
      "flightId": 30,
      "customerId": 69,
      "passengers": [{
              "id": 0,
              "name": "Denis",
              "dateBirth": "1997-06-06",
              "gender": 0,
              "address": "Minsk",
              "email": "den@gmail.com"
          }, {
              "id": 0,
              "name": "Sofia",
              "dateBirth": "2007-02-16",
              "gender": 1,
              "address": "Paris",
              "email": "sof@gmail.com"
          }, {
              "id": 91,
              "name": "Kendall Velazquez",
              "dateBirth": "1980-09-24",
              "gender": 0,
              "address": "805-1408 Mi Rd.",
              "email": "egestas.a.dui@aliquet.ca"
          }
      ]
    }
5. BONUS: Endpoint that updates passenger’s address.
    1. PATCH to ```'https://localhost:7299/people/91'``` with the following body updates passenger's address
    ```
    {
      "address": "My new address"
    }
