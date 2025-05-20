# 📈 DataHorseman

**DataHorseman** é uma aplicação desktop desenvolvida em C# (.NET) que permite o gerenciamento de dados financeiros e pessoais. Ela oferece funcionalidades para upload de arquivos JSON, processamento e validação de dados, e integração com um banco de dados relacional para armazenamento e análise de informações.

---

## 🧹 Arquitetura

O projeto segue uma arquitetura em camadas, promovendo a separação de responsabilidades e facilitando a manutenção e escalabilidade:

* **Camada de Apresentação (`DataHorseman.AppWin`)**: Interface gráfica construída com Windows Forms para interação com o usuário.
* **Camada de Aplicação (`DataHorseman.Application`)**: Contém a lógica de negócios e serviços que orquestram as operações da aplicação.
* **Camada de Domínio (`DataHorseman.Domain`)**: Define as entidades e regras de negócio fundamentais.
* **Camada de Infraestrutura (`DataHorseman.Infrastructure`)**: Responsável pela persistência de dados e comunicação com o banco de dados.

---

## ✅ Funcionalidades

* Upload de arquivos JSON contendo dados pessoais e financeiros.
* Processamento e validação dos dados antes de persistir no banco de dados.
* Geração automática de carteiras financeiras com base nos dados fornecidos.
* Gerenciamento de entidades como pessoas, contatos, endereços e ativos financeiros.

---

## 🛠️ Tecnologias Utilizadas

* **Linguagem**: C# (.NET)
* **Interface Gráfica**: Windows Forms
* **ORM**: Entity Framework Core
* **Banco de Dados**: SQL Server (ou outro compatível)
* **Serialização JSON**: Newtonsoft.Json
* **Mapeamento de Objetos**: AutoMapper

---

## 🚀 Como Executar o Projeto

1. **Clone o repositório**:

   ```bash
   git clone https://github.com/ericknovais/DataHorseman.git
   ```

2. **Abra a solução no Visual Studio**.

3. **Configure a string de conexão** com o banco de dados no arquivo `appsettings.json` ou `app.config`, conforme a estrutura do projeto.

4. **Restaure os pacotes NuGet**:

   * No Visual Studio, clique com o botão direito na solução e selecione **Gerenciar Pacotes NuGet** para restaurar as dependências.

5. **Compile e execute** o projeto principal (`DataHorseman.AppWin`).

---

## 📂 Estrutura do Projeto

```
DataHorseman/
├── src/
│   ├── DataHorseman.AppWin/         # Interface gráfica (Windows Forms)
│   ├── DataHorseman.Application/    # Serviços e lógica de negócios
│   ├── DataHorseman.Domain/         # Entidades e interfaces de domínio
│   └── DataHorseman.Infrastructure/ # Repositórios e acesso a dados
└── README.md
```

---

## ⚠️ Problemas Conhecidos

* **Desempenho com Arquivos Grandes**: O processamento de arquivos JSON muito grandes pode impactar a performance da aplicação.
* **Responsividade da Interface**: Operações demoradas podem tornar a interface do usuário não responsiva.
* **Tratamento de Erros**: Algumas exceções podem não fornecer mensagens de erro detalhadas, dificultando a identificação de problemas.

---

## 🤝 Contribuindo

Contribuições são bem-vindas! Para contribuir:

1. Faça um fork do projeto.

2. Crie uma nova branch:

   ```bash
   git checkout -b minha-feature
   ```

3. Faça suas alterações e commits:

   ```bash
   git commit -m "Descrição das alterações"
   ```

4. Envie para o seu fork:

   ```bash
   git push origin minha-feature
   ```

5. Abra um Pull Request neste repositório.

---

## 📄 Licença

Este projeto está licenciado sob a Licença MIT. Consulte o arquivo [LICENSE](LICENSE) para mais informações.
