FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY /LoteTablas.Blazor.UI/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf