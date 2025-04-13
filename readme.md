📦 ZooApi - API para Gerenciamento de Animais e Cuidados
Este projeto é uma API REST desenvolvida em .NET Core 9 com Entity Framework Core para gerenciar o cadastro de animais e seus cuidados.

🚀 Tecnologias Utilizadas

ASP.NET Core 9
Entity Framework Core
SQL Server (LocalDB ou Server)
AutoMapper
Swagger

🖥️ Pré-requisitos
Antes de rodar o projeto, certifique-se de ter instalado:

.NET SDK 9.0

SQL Server (LocalDB ou completo)

Visual Studio 2022

EF Core CLI
(instalar com dotnet tool install --global dotnet-ef)

⚙️ Como Rodar o Projeto
1. Clone o repositório
git clone 
cd zoo-api

2. Configure o appsettings.json
Verifique se a string de conexão no arquivo appsettings.json está assim:

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=ZooDb;Trusted_Connection=True;TrustServerCertificate=True;"
}


3. Restaurar os pacotes NuGet

dotnet restore

4. Criar e atualizar o banco de dados

dotnet ef database update

5. Rodar o projeto

dotnet run
A API estará disponível em:
https://localhost:7270

Para rodar novos comandos de migration:

dotnet ef migrations add NomeDaMigration
dotnet ef database update

Para limpar e compilar:

dotnet clean
dotnet build