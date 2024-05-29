<h1 align="center"> 🥇 ProductsAPI 🚀 </h1>

  
## 💻 Projeto 

- Desenvolvimento de API para um CRUD de produtos. A ideia é poder ser usada para qualquer produto, no caso, eu utilizei processadores como meus produtos iniciais.
- Esta solução contém dois projetos:
    1. **Products.API** - O projeto principal da API.
    2. **Products.API.Test** - O projeto de testes unitários.

### Products.API
- Este é o projeto principal da API que gerencia produtos. Utiliza .NET 8 e o Entity Framework Core com o banco de dados SQLite.
- O CRUD consiste em:
  - Criar um produto;
  - Atualizar o produto já existente selecionando pelo id;
  - Listar os produtos, podendo pesquisar pelo nome se desejar e ordenar por alguma das tabelas do banco (nome, valor, estoque). E também listar um produto específico procurando pelo id;
  - Deletar um produto através do id.
  - A coluna de "valor" não permite valor negativo, já a coluna de "estoque" permite.

### Products.API.Test
- Este é o projeto de testes unitários para a API. Utiliza o xUnit Test como framework de testes e in-memory database do Entity Framework Core para simular um banco de dados real.

### Banco de Dados
- O projeto está configurado para usar o Entity Framework no modo Code-First, onde o banco é gerado a partir das classes de modelo. E é populado ao rodar a aplicação conforme configurado no Data/ProductsDbContext.cs


## Para Rodar
Necessário ter/instalar:
- Clonar o projeto
- .Net 8
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Sqlite
  

<br>

## 🚀 Tecnologias

Este projeto foi desenvolvido utilizando:
- C#
- .Net 8
- Entity Framework Core
- SQLite
- xUnity Test
  
## Finalização ❤

Feito com ♥ por Mikael Sirqueira
