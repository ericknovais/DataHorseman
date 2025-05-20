# DataHorseman

DataHorseman é uma solução para gerenciamento de dados financeiros e pessoais, com suporte para upload de arquivos JSON, processamento de dados e integração com banco de dados.

## Índice

1. [Arquitetura](#arquitetura)
2. [Funcionalidades](#funcionalidades)
3. [Pré-requisitos](#pré-requisitos)
4. [Configuração](#configuração)
5. [Como Usar](#como-usar)
6. [Estrutura do Código](#estrutura-do-código)
7. [Problemas Conhecidos](#problemas-conhecidos)
8. [Contribuição](#contribuição)
9. [Licença](#licença)

## Arquitetura

A solução segue uma arquitetura em camadas para separar responsabilidades e melhorar a manutenção:

- **Camada de Apresentação**: Aplicação Windows Forms para interação com o usuário.
- **Camada de Aplicação**: Contém a lógica de negócios e serviços.
- **Camada de Domínio**: Define as entidades e regras de negócio.
- **Camada de Infraestrutura**: Gerencia a persistência de dados e interações com o banco de dados.

## Funcionalidades

- Upload de arquivos JSON contendo dados pessoais e financeiros.
- Processamento e validação de dados antes de salvar no banco.
- Geração automática de carteiras financeiras.
- Gerenciamento de pessoas, contatos, endereços e ativos.

## Pré-requisitos

- Visual Studio 2019 ou superior.
- .NET Framework ou .NET Core (dependendo do projeto).
- SQL Server ou outro banco de dados compatível.

## Configuração

1. Clone o repositório:
   git clone https://github.com/seu-usuario/seu-repositorio.git

2. Abra a solução no Visual Studio.

3. Configure a string de conexão com o banco de dados no arquivo `appsettings.json` ou `app.config`.

4. Restaure os pacotes NuGet:
   - Clique com o botão direito na solução e selecione **Gerenciar Pacotes NuGet**.

5. Compile a solução e execute o projeto principal.

## Como Usar

1. Inicie a aplicação.
2. Use o botão "Upload" para selecionar um arquivo JSON.
3. Clique em "Salvar" para processar e salvar os dados no banco de dados.

## Estrutura do Código

### Camada de Apresentação (DataHorseman.AppWin)

- **frmUpload.cs**: Gerencia o upload de arquivos e o salvamento de dados.
- **Program.cs**: Configura a injeção de dependência e inicia a aplicação.

### Camada de Aplicação (DataHorseman.Application)

- **Serviços**: Contém a lógica de negócios para processamento de dados.
- **Profiles**: Perfis do AutoMapper para mapeamento de entidades.

### Camada de Domínio (DataHorseman.Domain)

- **Entidades**: Define os objetos principais, como `Pessoa`, `Contato` e `Carteira`.
- **Interfaces**: Contratos para repositórios e serviços.

### Camada de Infraestrutura (DataHorseman.Infrastructure)

- **Repositórios**: Implementa a lógica de acesso a dados.
- **Migrations**: Gerencia alterações no esquema do banco de dados.

## Problemas Conhecidos

1. **Arquivos JSON Grandes**: O desempenho pode ser afetado ao processar arquivos muito grandes.
2. **Congelamento da Interface**: Operações demoradas podem tornar a interface do usuário não responsiva.
3. **Tratamento de Erros**: Alguns casos extremos podem não fornecer mensagens de erro detalhadas.

## Contribuição

1. Faça um fork do repositório.
2. Crie uma nova branch:
   git checkout -b minha-feature
3. Faça suas alterações e commit:
   git commit -m "Descrição das alterações"
4. Envie para a branch:
   git push origin minha-feature
5. Abra um pull request.

## Licença

Este projeto está licenciado sob a Licença MIT.
