FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY . .

RUN dotnet publish EmailSender.WebApi/EmailSender.WebApi.csproj -c Release -o /publish


FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
EXPOSE 5000


ARG UID=2345
ARG GID=2345
ARG USERNAME=emailsender


RUN groupadd -g $GID $USERNAME && useradd -u $UID -g $GID -m -s /bin/bash $USERNAME
USER $USERNAME

WORKDIR /app

COPY --from=build  --chown=$UID:$GID /publish/ .

ENTRYPOINT [ "dotnet", "EmailSender.WebApi.dll" ]
CMD [ "--urls", "http://0.0.0.0:5000" ]

