# LNUJuniorWebDevLoadBalancer
Load Balancer project for LNU Web development


This application transforms text from any file into another downloadable file, it supports regex and the file can become quite large, therefore there is a load balancer. 

Load balancer is working using nginx proxy, which is using ip-hash method (requests from one computer are responded by one server, another computer - another server).

Database used - Postgres which is in a separate docker container.

The site can be deployed on azure using docker compose as a container group (it would probably be better to change that to be a virtual machine or kubernetes)

Containers: 2 servers (asp.net core), proxy (nginx), database (postgres)

Server is using websockets (SignalR) to notify the client about the work done on server.

Nginx is using https, then sends requests directly to servers through http.

The backend logic is better to be redone, so that task (string transformation) is async and the client is only notified about the result, progres or failure.

The result page is done, but you cannot access data of each result, the page to access this data can be created.
