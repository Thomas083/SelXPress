# List of Entity Codes and Error Messages

This table lists entity codes associated with error messages for various entities in the application.

| Entity            | Code      | Message                                                               |
| ----------------- | --------- | --------------------------------------------------------------------- |
| Attribute         | ATT-1101  | The model is wrong, a bad request occured                             |
|                   | ATT-1102  | There are missing fields, please try again with some data             |
|                   | ATT-1401  | There is no Attribute in the database, please try again               |
|                   | ATT-1402  | The Attribute with the ID : {id} doesn't exist                        |
|                   | ATT-2001  | You are not authorized to do this operation                           |
| AttributeData     | ATD-1101  | The model is wrong, a bad request occured                             |
|                   | ATD-1102  | There are missing fields, please try again with some data             |
|                   | ATD-1401  | There is no AttributeData in the database, please try again           |
|                   | ATD-1402  | The AttributeData with the ID : {id} doesn't exist                    |
|                   | ATD-2001  | You are not authorized to do this operation                           |
| Cart              | CRT-1101  | The model is wrong, a bad request occured                             |
|                   | CRT-1102  | There are missing fields, please try again with some data             |
|                   | CRT-1401  | There is no carts in the database, please try again                   |
|                   | CRT-1402  | The cart with the ID : {id} doesn't exist                             |
|                   | CRT-1403  | There are no shopping carts for the user with the ID : {id}           |
|                   | CRT-2001  | You are not authorized to do this operation                           |
| Category	        | CAT-1101	| The model is wrong, a bad request occured                             |
|	                | CAT-1102	| There are missing fields, please try again with some data             |
|	                | CAT-1401	| There is no categories in the database, please try again              |
|	                | CAT-1402	| The category with the ID : {id} doesn't exist                         |
|	                | CAT-2001	| You are not authorized to do this operation                           |
| Comment	        | COM-1101	| The model is wrong, a bad request occured                             |
|	                | COM-1102	| There is missing fields, please try again with some data              |
|	                | COM-1401	| There is no comments in the database, please try again                |
|	                | COM-1402	| The comment with the ID : {id} doesn't exist                          |
|	                | COM-1403	| There is no comments of the user with the ID : {id}                   |
|	                | COM-1404	| There is no comments of the product with the ID : {id}                |
|	                | COM-1405	| The user with the ID : {id} doesn't exist                             |
|	                | COM-1406	| The product with ID : {id} doesn't exist                              |
|	                | COM-2001	| You are not authorized to do this operation                           |
| Mark	            | MRK-1101	| The model is wrong, a bad request occured                             |
|	                | MRK-1102	| There is missing fields, please try again with some data              |
|	                | MRK-1401	| There is no marks in the database                                     |
|	                | MRK-1402	| The mark with the ID : {id} doesn't exist                             |
|	                | MRK-1403	| There is no marks for the user with the ID : {id}                     |
|	                | MRK-1404	| There is no marks for the product with the ID : {id}                  |
|	                | MRK-2001	| You are not authorized to do this operation                           |
| Order	            | ORD-1101	| The model is wrong, a bad request occurred                            |
|	                | ORD-1102	| There are missing fields, please try again with some data             |
|	                | ORD-1401	| There are no orders in the database, please try again                 |
|	                | ORD-1402	| The order with ID : {id} doesn't exist                                |
|	                | ORD-1403	| There is no orders for this user                                      |
|	                | ORD-2001	| You are not authorized to do this operation                           |
| OrderProduct	    | ODP-1101	| The model is wrong, a bad request occurred                            |
|	                | ODP-1102	| There are missing fields, please try again with some data             |
|	                | ODP-1104	| The email is missing in the request header                            |
|	                | ODP-1401	| There are no order products found in the database, please try again   |
|	                | ODP-1402	| The order product with the ID : {id} doesn't exist                    |
|	                | ODP-2001	| You are not authorized to do this operation                           |
| ProductAttribute	| PAT-1101	| The model is wrong, a bad request occurred                            |
|	                | PAT-1102	| There are missing fields, please try again with some data             |
|	                | PAT-1401	| There are no product attributes in the database                       |
|	                | PAT-1402	| The product attribute with the ID : {id} doesn't exist                |
|	                | PAT-2001	| You are not authorized to do this operation                           |
| Product	        | PRO-1101	| The model is wrong, a bad request occurred                            |
|	                | PRO-1102	| There are missing fields, please try again with some data             |
|	                | PRO-1103	| You are not connected                                                 |
|	                | PRO-1401	| There are no products in the database                                 |
|	                | PRO-1402	| The product with ID : {id} doesn't exist                              |
|	                | PRO-1403	| There is no products for the current user                             |
|	                | PDT-2001	| You are not authorized to do this operation                           |
| Role	            | RLE-1101	| The model is wrong, a bad request occured                             |
|	                | RLE-1102	| There are missing fields, please try again with some data             |
|	                | RLE-1401	| There is no roles in the database, please try again                   |
|	                | RLE-1402	| The role with the ID : {id} doesn't exist                             |
|	                | RLE-2001	| You are not authorized to do this operation                           |
| Tag	            | TAG-1101	| The model is invalid, a bad request occured                           |
|	                | TAG-1102	| There are missing fields, please try again with some data             |
|	                | TAG-1401	| There is no tags in the database, please try again                    |
|	                | TAG-1402	| The tag with ID : {id} doesn't exist                                  |
|	                | TAG-2001	| You are not authorized to do this operation                           |
| User	            | USR-1101	| The model is wrong, a bad request occured                             |
|	                | USR-1102	| There are missing fields, please try again with some data             |
|	                | USR-1104	| The user could not be created, please try again                       |
|	                | USR-1401	| There is no users in the database, please try again                   |
|	                | USR-1402	| The user with the id :{id} doesn't exist                              |
|	                | USR-1403	| The user with the email {email} doesn't exist                         |
|	                | USR-2001	| You are not authorized to do this operation                           |
| SRV (Service)     | SRV-1701  | An error occured while the decoding of the jwt token                  |
|                   | SRV-1401  | The email address is not in the database                              |
|                   | SRV-1702  | The token is not valid, please try again with another token           |


# Link to technical documentation
1. ### [Access to MCD](..\Documentation\Technical-Documentation\ModeleRelationnelleSellXPress.drawio)
2. ### [Access to Api Doc](..\Documentation\Technical-Documentation\Help\index.html) 
    - You can run index.hmtl tu access to the documentation of each method from the api