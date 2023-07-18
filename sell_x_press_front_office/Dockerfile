FROM node:16-alpine
EXPOSE 8080

WORKDIR /app

ENV PATH /app/node_modules/.bin:$PATH

COPY ./package.json /app/package.json

RUN npm install
RUN npm install @vue/cli@3.7.0 -g

COPY ./public /app/public
COPY ./src /app/src

CMD ["npm", "run", "serve"]