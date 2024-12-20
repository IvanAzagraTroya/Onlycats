
# Onlycats

Onlycats es una mezcla entre Twitter e Instagram focalizada al contenido dedicado a gatos, de ahí el nombre.

En esta red social se pueden compartir fotos de gatos, comentar y valorar junto a otros amantes de los gatos.

## Characteristics
- Registro e inicio de sesión con autenticación JWT.
- Subida de imágenes y gestión de publicaciones.
- Interacciones con likes/dislikes en posts y comentarios.

## Used technologies
- Backend: .NET Framework 4.7.2, microservicios, autenticación JWT.
- Bases de datos: PostgreSQL (SQL), MongoDB (NoSQL).
- Gateway: Ocelot.
- Contenedores: Docker.
- Almacenamiento: Contenedores Docker con las bases de datos.

## Architecture
- Uso de microservicios: PostService, InteractionService, UserService, ImageService.
- Gateway central para acceder a los servicios usando Ocelot.
### Service Description:
- PostService: Este servicio se encarga de manejar tanto posts como comentarios, en él se crean los datos que serán publicados y se guardan las imágenes publicadas creando carpetas por usuario, fecha y post dando la posibildidad de mejorar los posts para aceptar varias imágenes.

- InteractionService: Se encarga de manejas las interacciones y enviar las notificaciones por tipos de interacción (comentario, like y post en este momento).

- UserService: Este servicio trabaja con una base de datos Postgre donde se guarda la información de los usuarios, también realiza el cifrado de la contraseña y maneja la authenticación para devolver los tokens necesarios.
### Architecture Diagram:
![Onlycats/Sin título-2024-12-08-1648.svg](https://github.com/IvanAzagraTroya/Onlycats/blob/master/Sin%20t%C3%ADtulo-2024-12-08-1648.svg)

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

