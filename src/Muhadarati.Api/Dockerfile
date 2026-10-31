# يُستخدم عند النشر على الاستضافة المجانية (Render/Railway/Fly.io) — غير مطلوب للتشغيل المحلي
# حيث يعمل مشروع Api مباشرة عبر dotnet watch (انظر القسم 13.2 من المواصفة)

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["src/Muhadarati.Domain/Muhadarati.Domain.csproj", "src/Muhadarati.Domain/"]
COPY ["src/Muhadarati.Application/Muhadarati.Application.csproj", "src/Muhadarati.Application/"]
COPY ["src/Muhadarati.Infrastructure/Muhadarati.Infrastructure.csproj", "src/Muhadarati.Infrastructure/"]
COPY ["src/Muhadarati.Shared/Muhadarati.Shared.csproj", "src/Muhadarati.Shared/"]
COPY ["src/Muhadarati.Api/Muhadarati.Api.csproj", "src/Muhadarati.Api/"]
RUN dotnet restore "src/Muhadarati.Api/Muhadarati.Api.csproj"

COPY . .
WORKDIR "/src/src/Muhadarati.Api"
RUN dotnet publish -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Muhadarati.Api.dll"]
