Explorador de publicaciones públicas en Mastodon


¿Qué hace esta aplicación?

Esta es una aplicación ASP.NET Core MVC que permite:

Consultar publicaciones públicas de Mastodon

-Buscar por palabra clave o hashtag

-Calcular un puntaje de interacción (engagement)

-Mostrar las publicaciones con mejor rendimiento (“Top interacción”)

Todo se hace sin autenticación, usando únicamente la API pública de Mastodon.


--Estructura general del proyecto--

Archivos principales:

-Program.cs → Configuración general de la app

-MastodonService.cs → Conexión con la API de Mastodon

-SocialController.cs → Lógica de búsqueda y filtrado

-MastodonPost.cs → Modelos de datos

-TextHelper.cs → Funciones de apoyo para texto (helpers)

-Index.cshtml → Interfaz web (vista)

-Program.cs – Arranque de la aplicación


"Program.cs"

Este archivo configura la aplicación y registra los servicios necesarios.

¿Qué hace?

-Habilita MVC (AddControllersWithViews)

-Registra MastodonService con HttpClient

-Configura rutas y middlewares básicos

-builder.Services.AddControllersWithViews();

-builder.Services.AddHttpClient<MastodonService>();

Importante:
Gracias a AddHttpClient<MastodonService>(), el servicio puede hacer peticiones HTTP sin tener que manejar conexiones manualmente.


"MastodonService – Consumo de la API"

MastodonService.cs

Este servicio se encarga de hablar con Mastodon.

Función principal

-GetPublicPostsAsync(int pages = 5)

¿Qué hace?

-Llama a la API pública:
https://mastodon.social/api/v1/timelines/public

-Obtiene hasta 40 posts por petición

-Usa paginación (max_id)

-Hace pausas (Task.Delay(300)) para no saturar la API

-Devuelve una lista de publicaciones

"MastodonPost – Modelo de datos"

MastodonPost.cs

Representa la estructura de una publicación de Mastodon.

Incluye:

-Contenido del post

-Usuario que lo publicó

-Métricas de interacción:

-Likes

-Reblogs

-Respuestas

-Puntaje de engagement calculado

public int EngagementScore { get; set; }

"SocialController – Lógica principal"

SocialController.cs

Este controlador coordina todo.

Flujo del método Index

-Obtiene publicaciones desde MastodonService

-Filtra por palabra o hashtag (si el usuario escribe algo), si se presiona buscar sin keyword, la api devuelve posts sin filtro

-Calcula el engagement

Si se activa “Top interacción”:

-Filtra posts con buen rendimiento

-Ordena por puntaje

-Muestra solo los mejores

--Envía los datos a la vista

"Index.cshtml – Interfaz de usuario"

Index.cshtml

Aquí se muestra todo al usuario.

Funcionalidades visibles

-Campo para buscar palabra o hashtag

-Checkbox “Top interacción”

-Lista de publicaciones

-Resaltado de palabras buscadas

-Indicador visual de engagement


Etiqueta 🔥 “En tendencia” si el score es alto

Ejemplo visual:

❤️ Likes

🔁 Reblogs

💬 Respuestas

⭐ Engagement

"TextHelper – Funciones de apoyo"

TextHelper.cs

Este archivo contiene utilidades reutilizables:

-Calcular engagement

-Limpiar HTML(Elimina todas las etiquetas HTML de un texto)

-Normalizar texto(eliminando acentos y caracteres especiales)

-Resaltar palabras


