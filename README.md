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
