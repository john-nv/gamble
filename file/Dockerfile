FROM node:18
# Create app directory
WORKDIR /app 
# Install app dependencies
# A wildcard is used to ensure both package.json AND package-lock.json are copied
# where available (npm@5+)
RUN apt-get -y update

COPY ./file /app  
RUN ls -la
RUN yarn install
# If you are building your code for production
# RUN npm ci --omit=dev
# Bundle app source

EXPOSE 8082
CMD [ "node", "server.js" ]