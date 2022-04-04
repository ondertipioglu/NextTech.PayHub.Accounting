Developer Guide
===============

 
# 1. Intro
....

# 2. Endpoints
* http://localhost:5000/accounting-api/v1/Accounting

# 3. Methods
## 3.1 AuthorizeCreditCard
### 3.1.1 Valid Request
```
POST /accounting-api/v1/Accounting/authorize-card HTTP/1.1
Host: localhost:5000
Content-Type: application/json
Cache-Control: no-cache
Postman-Token: 6ad0bab4-7652-644d-2eef-d244288005ef

{
  "cardOwner": "Larry Loe",
  "cardNumber": "345742344802146",
  "cardExpireDate": "02/2023",
  "cvcNumber": "785"
}
```
### 3.1.2 Valid Response
```
{
    "validationErrorMessage": null,
    "cartType": "NextTech.PayHub.Accounting.Domain.AmericanExpress"
}
```
### 3.2.1 Invalid Request
```
POST /accounting-api/v1/Accounting/authorize-card HTTP/1.1
Host: localhost:5000
Content-Type: application/json
Cache-Control: no-cache
Postman-Token: 59fc529c-95a7-83d6-0c63-476f78cd1c1b

{
  "cardOwner": "Larry Loe",
  "cardNumber": "345742344802146",
  "cardExpireDate": "02/20223",
  "cvcNumber": "785"
}
```
### 3.2.2 Invalid Respone
```
{
    "StatusCode": 400,
    "Error": "Validation Error(s): The credit card used has expired or invalid credit card expiration date"
}
```
  
# 4. Test Credit Cards
| CARDTYPE        | CARDNUMBER       | CARDOWNER     | EXPIREDATE | CVC   |
|-----------------|------------------|---------------|------------|-------|
| AmericanExpress | 375991270307470  | John Doe      | 06/2024    | 1258  |
| AmericanExpress | 343899323972191  | Gordon Norman | 01/2025    | 8597  |
| AmericanExpress | 347425294152997  | Marta Moe     | 06/2025    | 528   |
| AmericanExpress | 345742344802146  | Larry Loe     | 02/2023    | 785   |
| AmericanExpress | 343876755249611  | Karren Koe    | 01/2023    | 4526  |
| MasterCard      | 5573611704017326 | John Doe      | 06/2025    | 123   |
| MasterCard      | 5241491523617826 | Gordon Norman | 06/2023    | 325   |
| MasterCard      | 5110616165365783 | Marta Moe     | 06/2026    | 386   |
| MasterCard      | 5321674746107807 | Larry Loe     | 06/2021    | 453   |
| MasterCard      | 5199046253286366 | Karren Koe    | 12/2024    | 796   |
| Visa            | 4532996986610937 | John Doe      | 06/2026    | 325   |
| Visa            | 4916198834172618 | Gordon Norman | 09/2022    | 379   |
| Visa            | 4916891903488481 | Marta Moe     | 06/2027    | 958   |
| Visa            | 4228420255793299 | Larry Loe     | 12/2026    | 755   |
| Visa            | 4556810326147131 | Karren Koe    | 07/2027    | 458   |

