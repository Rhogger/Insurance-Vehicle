<h1 align="center">Insurance Vehicle API</h1>

<div align="center">

  [Projeto](#projeto) 
  &nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  [Tecnologias](#tecnologias)
  &nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  [Como executar](#como_executar)
  &nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  [Endpoints](#endpoints)
  &nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  [Licença](#license)

</div>

<p align="center">
  <img alt="License" src="https://img.shields.io/static/v1?label=license&message=MIT&color=49AA26&labelColor=000000">
</p>

<br>

## 💻 Projeto <a name = "projeto"></a>

Este projeto consiste em uma API desenvolvida como parte de uma prova da disciplina de Técnicas Avançadas de Desenvolvimento de Software, do 7º período do curso de Engenharia de Software. A API foi tem como objetivo consultar dados de uma pessoa e retornar seu "CREDIT_SCORE" com base em um arquivo CSV hospedado na nuvem. 


### Requisitos Obrigatórios

- OpenAPI: A API deve possuir uma especificação OpenAPI (anteriormente conhecida como Swagger) para documentar sua interface e operações.
- Publicação em Servidor: A API deve estar publicada e acessível em um servidor até a data da entrega da prova.
- Linguagem de Programação Nativamente Web: A API deve ser desenvolvida utilizando uma linguagem de programação que seja nativamente adequada para desenvolvimento web.

### Metodologia de Avaliação
- Atendimento aos Requisitos: Serão atribuídos até 5 pontos com base no cumprimento dos requisitos obrigatórios.
- Boas Práticas de Programação: Serão atribuídos até 2 pontos com base no uso de boas práticas de programação, como legibilidade, organização e eficiência do código.
- Utilização Correta de Conceitos de Orientação a Objetos: Serão atribuídos até 2 pontos com base na utilização correta e eficaz de conceitos de orientação a objetos.
- Testes Unitários: Será atribuído até 1 ponto pela implementação de testes unitários adequados para a API.

### Obervações

A maioria dos testes unitários foram implementados, faltando apenas os testes do controller que será implementado posteriormente.

O dataset (csv) foi armazenado em nuvem pois o CsvHelper pegava o filepath do arquivo para poder realizar leitura, porém esse path era relacionado a máquina onde o código estava sendo executado, ou seja, quando hospedado em máquinas virtuais da Azure, o filepath era um que não batia com o esperado para buscar o csv.

O projeto foi hospedado na Microsoft Azure App Service, com integração ao Git Actions para realização do Build e Deployment a cada commit.

<br>
<br>

## 👨‍💻 Como Executar <a name = "como_executar"></a>

Para executar a API localmente, siga estas etapas:

- Clonar o repositório para o seu ambiente de desenvolvimento.
- Instalar as dependências necessárias para a execução da API.
- Executar a API localmente utilizando o comando ```dotnet run --project Backend``` na pasta raiz da Solution(Projeto).

Para executar a API já em deploy, apenas utilize o link:

https://insurance-api-rfs.azurewebsites.net/

Para utilizar o Swagger tanto local, quando a versão em produção, basta adicionar no sufixo o /swagger:

http://localhost:5260/swagger

https://insurance-api-rfs.azurewebsites.net/swagger


Os testes são executado com ```dotnet run --project Tests/Unit```

<br>
<br>

## 📌 Endpoints <a name = "como_executar"></a>

### /person/getscore

Parâmetros:

- Idade: Idade da pessoa em anos.
- Sexo: Gênero da pessoa (masculino ou feminino).
- Anos de experiência habilitado: Tempo em anos que a pessoa possui habilitação para dirigir.
- Nível de escolaridade: Nível de escolaridade da pessoa.
- Renda: Classe financeira da pessoa.
- Ano do Veículo: Ano de fabricação do veículo.
- Tipo do Veículo: Sedan ou Esportivo.
- Quilometragem Anual: Quilometragem anual percorrida pelo veículo em - quilômetros.

Retorna uma lista de Credit Scores e seus respectivos ID.

### /person/getcsvdata (Oculto)

Não tem passagem de parâmetros, apenas busca o csv que foi armazenado em nuvem no Storage Blob da Microsoft Azure.

<br>
<br>

## 🚀 Tecnologias <a name = "tecnologias"></a>

- C#
- ASP.NET Core
- CsvHelper
- xUnit
- OpenAPI
- Swagger
- Microsoft.Extensions.Caching.Memory
- Azure Storage Blob (Cloud Storage)
- Azure App Service (Deployment)

<br>
<br>

##  🔒 Licença

Esse projeto está sob a licença MIT.

<hr>
