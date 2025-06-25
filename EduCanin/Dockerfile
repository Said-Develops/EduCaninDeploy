# Étape 1 : Compilation de l'application avec le SDK complet .NET
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
# ? Utilise une image contenant tous les outils nécessaires pour compiler du code .NET (dotnet build/publish, nuget, etc.)
# ? Cette étape est temporaire, elle ne sera pas incluse dans l'image finale

WORKDIR /src
# ? Définit le dossier de travail dans le conteneur, ici on crée un dossier /src

COPY . .
# ? Copie tous les fichiers du projet local (Dockerfile, .csproj, .cs, etc.) dans /src du conteneur

WORKDIR /src/EduCanin
# ? Se déplace dans le dossier contenant le fichier .csproj de ton appli ASP.NET Core

RUN dotnet publish "EduCanin.csproj" -c Release -o /app/out
# ? Compile et publie l’application en mode Release dans le dossier /app/out
# ? Résultat : fichiers DLL, views, wwwroot, config, etc.

# Étape 2 : Image finale propre (runtime seulement)
FROM mcr.microsoft.com/dotnet/aspnet:9.0
# ? Nouvelle image de base contenant uniquement le runtime ASP.NET Core (pas d’outils de build)
# ? Image plus légère, plus sécurisée pour l'exécution

WORKDIR /app
# ? Crée le dossier de travail /app dans cette nouvelle image

COPY --from=build /app/out .
# ? Copie le résultat de la compilation (de l'étape 'build') dans le dossier actuel (/app)
# ? On n’embarque que les fichiers publiés, pas les sources ni les outils

ENV ASPNETCORE_URLS=http://+:10000
# ? Configure ASP.NET Core pour écouter sur le port 10000, sur toutes les interfaces (IP)

EXPOSE 10000
# ? Indique à Docker que le conteneur expose le port 10000 (utile pour le mapping de ports)

ENTRYPOINT ["dotnet", "EduCanin.dll"]
# ? Commande exécutée au démarrage du conteneur
# ? Lance ton application ASP.NET Core avec la DLL compilée
