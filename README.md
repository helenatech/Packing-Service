# PackingService API

API para empacotamento de produtos em caixas, usando .NET 9 e SQL Server.

---

## Pré-requisitos

- Docker instalado ([https://docs.docker.com/get-docker/](https://docs.docker.com/get-docker/))
- Docker Compose (já incluso nas versões recentes do Docker)

---

## Como rodar

1. Clone este repositório:

```bash
git clone https://github.com/seu-usuario/seu-repo.git
cd seu-repo
```

2. Suba os containers com Docker Compose:

```bash
docker-compose up --build
```

---

## Endpoints

- `POST /v1/empacotarPedidos` — Recebe uma lista de pedidos com produtos e retorna o empacotamento em caixas.

---

## Observações

- O banco SQL Server roda em container Docker, e os dados são persistidos no volume `sqlserverdata`.
- A aplicação aplica automaticamente as migrations no startup.
- Swagger está disponível no root da aplicação (`http://localhost:5203/` ou `https://localhost:7057/` dependendo da sua configuração).

---

## Sobre a lógica de empacotamento

- O algoritmo verifica se os produtos cabem na caixa somando seus volumes e validando as dimensões individuais.
- Considera rotações para que cada produto possa ser melhor acomodado.
- Agrupa produtos em pequenos conjuntos (até pares) para otimização do espaço dentro da caixa.
  
---
## Testes

Para rodar testes unitários (se houver), use:

```bash
dotnet test
```

---

## Configurações

A conexão com o banco pode ser configurada no arquivo `docker-compose.yml` na variável de ambiente:

```
ConnectionStrings__DefaultConnection=Server=db;Database=PackingDb;User=sa;Password=suasenha0;
```

Altere a senha conforme sua preferência, lembrando que deve bater com a senha do container do SQL Server (`SA_PASSWORD`).

---

## Licença

Este projeto é para fins de processo seletivo. =D

---

