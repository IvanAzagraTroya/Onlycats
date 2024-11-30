
# Onlycats

Onlycats es una mezcla entre Twitter e Instagram focalizada al contenido dedicado a gatos, de ahí el nombre.

En esta red social se pueden compartir fotos de gatos, comentar y valorar junto a otros amantes de los gatos.




## API Reference

### Posts Endpoints

| **Upstream Path**                     | **Downstream Path**          | **Method** | **Authentication** | **Rate Limiting**                |
|---------------------------------------|------------------------------|------------|--------------------|----------------------------------|
| `/onlycats/posts`                     | `/api/posts`                 | `GET`      | None               | None                             |
| `/onlycats/posts/image`               | `/api/posts/image`           | `GET`      | None               | None                             |
| `/onlycats/posts/{id}`                | `/api/posts/{id}`            | `GET`      | Bearer             | 5000 requests per 10 seconds     |
| `/onlycats/posts/ids`                 | `/api/posts/ids`             | `GET`      | Bearer             | 5000 requests per 10 seconds     |
| `/onlycats/posts/create_post`         | `/api/posts/insert`          | `POST`     | Bearer             | 5000 requests per 10 seconds     |
| `/onlycats/posts/update_post/{id}`    | `/api/posts/update/{id}`     | `PUT`      | Bearer             | 5000 requests per 10 seconds     |
| `/onlycats/posts/update_likes/{id}`   | `/api/posts/update_likes/{id}`| `PUT`     | Bearer             | 5000 requests per 10 seconds     |
| `/onlycats/posts/delete_post/{id}`    | `/api/posts/delete/{id}`     | `DELETE`   | Bearer             | 5000 requests per 10 seconds     |

### Comments Endpoints

| **Upstream Path**                     | **Downstream Path**          | **Method** | **Authentication** | **Rate Limiting**                |
|---------------------------------------|------------------------------|------------|--------------------|----------------------------------|
| `/onlycats/comments`                  | `/api/comments`              | `GET`      | None               | None                             |
| `/onlycats/comments/post/{id}`        | `/api/comments/post/{id}`    | `GET`      | None               | 5000 requests per 10 seconds     |
| `/onlycats/comments/{id}`             | `/api/comments/{id}`         | `GET`      | Bearer             | 5000 requests per 10 seconds     |
| `/onlycats/comments/insert`           | `/api/comments/insert`       | `POST`     | Bearer             | 5000 requests per 10 seconds     |
| `/onlycats/comments/delete/{id}`      | `/api/comments/delete/{id}`  | `DELETE`   | Bearer             | 5000 requests per 10 seconds     |

### Interactions Endpoints

| **Upstream Path**                     | **Downstream Path**          | **Method** | **Authentication** | **Rate Limiting**                |
|---------------------------------------|------------------------------|------------|--------------------|----------------------------------|
| `/onlycats/interactions`              | `/api/interactions`          | `GET`      | None               | None                             |
| `/onlycats/interactions/{id}`         | `/api/interactions/{id}`     | `GET`      | Bearer             | 5000 requests per 10 seconds     |
| `/onlycats/interactions/post/id`      | `/api/interactions/post/id`  | `GET`      | Bearer             | 5000 requests per 10 seconds     |
| `/onlycats/interactions/insert`       | `/api/interactions/insert`   | `POST`     | Bearer             | 5000 requests per 10 seconds     |
| `/onlycats/interactions/update/{id}`  | `/api/interactions/update/{entity.ActivityId}` | `PUT` | Bearer | 5000 requests per 10 seconds     |
| `/onlycats/interactions/delete/{id}`  | `/api/interacions/delete/{id}` | `DELETE` | Bearer | 5000 requests per 10 seconds     |
| `/onlycats/interactions/user/{userId}`| `/api/interactions/user/{userId}` | `GET` | Bearer | 5000 requests per 10 seconds     |
| `/onlycats/interactions/date/{id}`    | `/api/interactions/date/{postId}` | `GET` | Bearer | 5000 requests per 10 seconds     |
| `/onlycats/interactions/{type}`       | `/api/interactions/type/{type}` | `GET` | Bearer | 5000 requests per 10 seconds     |

### Users Endpoints

| **Upstream Path**                     | **Downstream Path**          | **Method** | **Authentication** | **Rate Limiting**                |
|---------------------------------------|------------------------------|------------|--------------------|----------------------------------|
| `/onlycats/users`                     | `/api/users`                 | `GET`      | None               | None                             |
| `/onlycats/update/{id}`               | `/api/users/update/{id}`     | `PUT`      | Bearer             | None                             |
| `/onlycats/user/{id}`                 | `/api/users/user/{id}`       | `GET`      | None               | None                             |
| `/onlycats/user_email/{email}`        | `/api/users/email/{email}`   | `GET`      | None               | None                             |
| `/onlycats/login`                     | `/api/login`                 | `POST`     | None               | None                             |
| `/onlycats/register`                  | `/api/register`              | `POST`     | None               | None                             |

## Demo

Future vercel test link


## Deployment

To deploy this project run

```bash
  docker-compose up or docker-compose up -d
```
Esto levanta todos los contenedores que componen el proyecto
 - postservice
 - interactionservice
 - userservice
 - postgresdb
 - mongodb


## Documentation

[Documentation](https://linktodocumentation)


## Authors

- [Iván Azagra Troya](https://github.com/IvanAzagraTroya)


![Logo](https://dev-to-uploads.s3.amazonaws.com/uploads/articles/th5xamgrr6se0x5ro4g6.png)

