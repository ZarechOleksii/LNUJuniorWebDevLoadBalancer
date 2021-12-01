FROM ubuntu:latest

RUN apt-get update \
    && apt-get install -y nginx \
    && apt-get clean \ 
    && rm -rf /var/li/apt/lists/* /tmp/* /var/tmp/*

ADD nginx.conf /etc/nginx/nginx.conf
ADD ./certificate/localhost.crt /etc/ssl/certs/localhost.crt
ADD ./certificate/localhost.key /etc/ssl/certs/localhost.key

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]