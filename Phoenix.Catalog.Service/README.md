To run mongo on docker, this is to create a container, check on docker or with docker ps that is running.

docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db mongo

Install the MongoDb extension and check the DB

mongodb://localhost:27017

To create the conn string
Use a Post and check how the DB is created on the extension

