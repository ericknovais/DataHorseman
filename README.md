##DataHorseman
Uma solução para gerenciamento de dados financeiros e pessoais, com suporte para upload de arquivos JSON, processamento de dados e integração com um banco de dados. A aplicação é composta por várias camadas, incluindo interface gráfica (Windows Forms), serviços de aplicação e repositórios de dados.
 
Arquitetura da Solução
A solução segue uma arquitetura em camadas, separando responsabilidades para facilitar a manutenção e escalabilidade:
1.	Camada de Apresentação (DataHorseman.AppWin):
•	Interface gráfica para upload e salvamento de dados.
•	Implementada com Windows Forms.
2.	Camada de Aplicação (DataHorseman.Application):
•	Contém os serviços de aplicação que orquestram as operações de negócio.
•	Implementa lógica de validação e transformação de dados.
3.	Camada de Domínio (DataHorseman.Domain):
•	Define as entidades e regras de negócio.
•	Contém interfaces para repositórios e serviços.
4.	Camada de Infraestrutura (DataHorseman.Infrastructure):
•	Gerencia a persistência de dados no banco de dados.
•	Implementa os repositórios definidos na camada de domínio.
 
Funcionalidades
•	Upload de Arquivos JSON:
•	Suporte para arquivos contendo dados de pessoas e ativos financeiros.
•	Processamento de Dados:
•	Validação e filtragem de dados antes de salvar no banco.
•	Geração automática de carteiras financeiras configuradas.
•	Persistência de Dados:
•	Salvamento de pessoas, contatos, endereços e ativos no banco de dados.
•	Gerenciamento de Carteiras:
•	Criação de carteiras financeiras com distribuição de valores entre ações e FIIs.
 
Pré-requisitos
Antes de executar a solução, certifique-se de ter os seguintes itens instalados:
•	Visual Studio (versão recomendada: 2019 ou superior).
•	.NET Framework (compatível com o projeto).
•	Um banco de dados configurado (ex.: SQL Server).
•	Dependências do projeto (gerenciadas pelo NuGet).
 
Configuração do Projeto
1.	Clone o repositório para sua máquina local:
