
There are several API calls To access them through Postman:

1. ```https://localhost:44393/ships```
  It is **GET** request you will get all the details of ships in below format 

  ```
  [
    {
      Id : "",
      Name : "",
      Velocity : "",
      latitude : "",
      longitude : "",
      nearestportinkm : "",
      timeToReachNearestPortinhrs: ""
    }
  ]
  ```
2. ```https://localhost:44393/ships/{id}```
  It is **GET** request you will get the details of specific ship in below format 

  ```
    {
      Id : "",
      Name : "",
      Velocity : "",
      latitude : "",
      longitude : "",
      nearestportinkm : "",
      timeToReachNearestPortinhrs: ""
    }
  ```

3. ```http://localhost:44393/addship```

  It is **POST** request you need to pass data 
  ```
  {
      Id : "",
      Name : "",
      Velocity : "",
      latitude : "",
      longitude : "",
   }
  ```
   _res_ will be as string:
  ```
  succesfully added or not succesfully written
  ```

4. ```http://localhost:6999/updateshipvelocity/{id}```

  It is **PUT** request you need to pass data that you want to update :
  ```
    {
      Name : "",
      Velocity : "",
      latitude : "",
      longitude : "",
    }
 
  ```

The Security can be Imporved due to less time I have just added requirements.

If you get any error in postman then might be you need edit ``` Content -type : application/json ```
