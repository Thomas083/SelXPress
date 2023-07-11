FROM node:16-alpine
EXPOSE 8080

WORKDIR /app

ENV PATH /app/node_modules/.bin:$PATH

COPY ../../sell_x_press_back_office/package.json /app/package.json

RUN npm install
RUN npm install @vue/cli@3.7.0 -g

COPY ../../sell_x_press_back_office/public /app/public
COPY ../../sell_x_press_back_office/src /app/src

CMD ["npm", "run", "serve"]