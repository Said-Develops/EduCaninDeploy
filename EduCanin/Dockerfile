# �tape 1 : Compilation de l'application avec le SDK complet .NET
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
# ? Utilise une image contenant tous les outils n�cessaires pour compiler du code .NET (dotnet build/publish, nuget, etc.)
# ? Cette �tape est temporaire, elle ne sera pas incluse dans l'image finale

WORKDIR /src
# ? D�finit le dossier de travail dans le conteneur, ici on cr�e un dossier /src

COPY . .
# ? Copie tous les fichiers du projet local (Dockerfile, .csproj, .cs, etc.) dans /src du conteneur

WORKDIR /src/EduCanin
# ? Se d�place dans le dossier contenant le fichier .csproj de ton appli ASP.NET Core

RUN dotnet publish "EduCanin.csproj" -c Release -o /app/out
# ? Compile et publie l�application en mode Release dans le dossier /app/out
# ? R�sultat : fichiers DLL, views, wwwroot, config, etc.

# �tape 2 : Image finale propre (runtime seulement)
FROM mcr.microsoft.com/dotnet/aspnet:9.0
# ? Nouvelle image de base contenant uniquement le runtime ASP.NET Core (pas d�outils de build)
# ? Image plus l�g�re, plus s�curis�e pour l'ex�cution

WORKDIR /app
# ? Cr�e le dossier de travail /app dans cette nouvelle image

COPY --from=build /app/out .
# ? Copie le r�sultat de la compilation (de l'�tape 'build') dans le dossier actuel (/app)
# ? On n�embarque que les fichiers publi�s, pas les sources ni les outils

ENV ASPNETCORE_URLS=http://+:10000
# ? Configure ASP.NET Core pour �couter sur le port 10000, sur toutes les interfaces (IP)

EXPOSE 10000
# ? Indique � Docker que le conteneur expose le port 10000 (utile pour le mapping de ports)

ENTRYPOINT ["dotnet", "EduCanin.dll"]
# ? Commande ex�cut�e au d�marrage du conteneur
# ? Lance ton application ASP.NET Core avec la DLL compil�e
