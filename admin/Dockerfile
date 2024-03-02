FROM node:18
WORKDIR /usr/src/app
COPY ./admin /usr/src/app/

# We don't need to do this cache clean, I guess it wastes time / saves space: https://github.com/yarnpkg/rfcs/pull/53
RUN yarn install ; \
    yarn cache clean; \
    yarn run build:prod

FROM nginx:alpine
WORKDIR /usr/share/nginx/html
COPY --from=0 /usr/src/app/build/ /usr/share/nginx/html