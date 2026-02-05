📘 Explorador de publicaciones públicas de Mastodon

Este proyecto es una aplicación web sencilla hecha en ASP.NET Core MVC que consume una API pública de Mastodon para mostrar publicaciones recientes de una red social federada.

La aplicación obtiene publicaciones públicas y permite analizarlas de forma básica.

🚀 ¿Qué hace la aplicación?

Obtiene publicaciones públicas desde Mastodon

Muestra el contenido y el autor de cada publicación

Permite buscar por palabra o hashtag

Calcula un puntaje de interacción (engagement)

Muestra las publicaciones con mayor interacción

🌐 ¿Qué API se usa?

Se usa la API pública de Mastodon, específicamente el feed de publicaciones públicas (public timeline) de una instancia sin restricciones.

No requiere login ni autenticación del usuario.

🧠 ¿Cómo se calcula la interacción?

Cada publicación tiene un puntaje basado en:

❤️ Favoritos

🔁 Reblogs

💬 Respuestas

Entre más interacción tenga, mayor será su puntaje.

🛠 Tecnologías usadas

ASP.NET Core MVC

C#

HttpClient

API pública de Mastodon

📌 Objetivo del proyecto

Aprender a:

Consumir una API externa

Procesar datos reales

Mostrar información en una página web

Analizar interacción en redes sociales

