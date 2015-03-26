FROM microsoft/aspnet

COPY . /app
WORKDIR /app
RUN ["kpm", "restore"]
#WORKDIR .

EXPOSE 5004
ENTRYPOINT ["k", "kestrel"]
