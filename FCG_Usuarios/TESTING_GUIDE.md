# ?? Guia de Teste da API - FIAP Cloud Games

Este documento fornece exemplos práticos de como testar a API de Usuários usando diferentes ferramentas.

## ?? API Key de Desenvolvimento

Para todos os testes, use a seguinte API Key:
```
e59eb473-6a3b-4c6d-81cc-8cb929d83399
```

## ?? Swagger UI (Recomendado)

### Como autenticar no Swagger:

1. **Acesse o Swagger UI**: `https://localhost:5001/swagger`
2. **Clique no botão "Authorize"** (cadeado verde no topo da página)
3. **Insira a API Key** no campo que aparecerá: `e59eb473-6a3b-4c6d-81cc-8cb929d83399`
4. **Clique em "Authorize"** para aplicar a autenticação
5. **Teste os endpoints** clicando em "Try it out"

### Vantagens do Swagger:
- ? Interface visual amigável
- ? Autenticação persistente
- ? Exemplos automáticos
- ? Validação em tempo real
- ? Documentação integrada

## ?? cURL (Terminal)

### 1. Criar Usuário
```bash
curl -X POST "https://localhost:5001/api/usuarios" \
  -H "Content-Type: application/json" \
  -H "X-API-Key: e59eb473-6a3b-4c6d-81cc-8cb929d83399" \
  -d '{
    "primeiroNome": "João",
    "sobrenome": "Silva",
    "apelido": "joaosilva",
    "email": "joao@email.com",
    "telefone": "(11) 99999-9999",
    "dataNascimento": "1990-05-15T00:00:00",
    "hashSenha": "senhaHasheada123"
  }'
```

### 2. Listar Usuários
```bash
curl -X GET "https://localhost:5001/api/usuarios" \
  -H "X-API-Key: e59eb473-6a3b-4c6d-81cc-8cb929d83399"
```

### 3. Obter Usuário por ID
```bash
curl -X GET "https://localhost:5001/api/usuarios/{id}" \
  -H "X-API-Key: e59eb473-6a3b-4c6d-81cc-8cb929d83399"
```

### 4. Atualizar Usuário
```bash
curl -X PUT "https://localhost:5001/api/usuarios" \
  -H "Content-Type: application/json" \
  -H "X-API-Key: e59eb473-6a3b-4c6d-81cc-8cb929d83399" \
  -d '{
    "id": "{id-do-usuario}",
    "primeiroNome": "João",
    "sobrenome": "Santos",
    "apelido": "joaosantos",
    "email": "joao.santos@email.com",
    "telefone": "(11) 98888-8888",
    "dataNascimento": "1990-05-15T00:00:00",
    "hashSenha": "novaSenhaHasheada456"
  }'
```

### 5. Deletar Usuário
```bash
curl -X DELETE "https://localhost:5001/api/usuarios/{id}" \
  -H "X-API-Key: e59eb473-6a3b-4c6d-81cc-8cb929d83399"
```

### 6. Health Check (sem autenticação)
```bash
curl -X GET "https://localhost:5001/api/health"
```

## ?? Postman

### Configuração da Collection:

1. **Criar nova Collection**: "FIAP Cloud Games API"
2. **Configurar Authorization**:
   - Type: `API Key`
   - Key: `X-API-Key`
   - Value: `e59eb473-6a3b-4c6d-81cc-8cb929d83399`
   - Add to: `Header`

### Variáveis de Environment:
```json
{
  "baseUrl": "https://localhost:5001",
  "apiKey": "e59eb473-6a3b-4c6d-81cc-8cb929d83399"
}
```

### Exemplo de Request:
- **Method**: POST
- **URL**: `{{baseUrl}}/api/usuarios`
- **Headers**: 
  - `Content-Type: application/json`
  - `X-API-Key: {{apiKey}}`
- **Body**: 
```json
{
  "primeiroNome": "Maria",
  "sobrenome": "Santos", 
  "apelido": "mariasantos",
  "email": "maria@email.com",
  "telefone": "(11) 97777-7777",
  "dataNascimento": "1992-08-20T00:00:00",
  "hashSenha": "senhaHasheada789"
}
```

## ?? Insomnia

### Workspace Setup:
1. **Create Workspace**: "FIAP Cloud Games"
2. **Add Environment**:
```json
{
  "base_url": "https://localhost:5001",
  "api_key": "e59eb473-6a3b-4c6d-81cc-8cb929d83399"
}
```

3. **Configure Global Headers**:
   - `X-API-Key`: `{{ _.api_key }}`

## ?? Python (Requests)

```python
import requests
import json

# Configurações
BASE_URL = "https://localhost:5001"
API_KEY = "e59eb473-6a3b-4c6d-81cc-8cb929d83399"

headers = {
    "Content-Type": "application/json",
    "X-API-Key": API_KEY
}

# Criar usuário
user_data = {
    "primeiroNome": "Ana",
    "sobrenome": "Costa",
    "apelido": "anacosta",
    "email": "ana@email.com", 
    "telefone": "(11) 96666-6666",
    "dataNascimento": "1988-12-10T00:00:00",
    "hashSenha": "senhaHasheada321"
}

response = requests.post(
    f"{BASE_URL}/api/usuarios",
    headers=headers,
    data=json.dumps(user_data)
)

print(f"Status: {response.status_code}")
print(f"Response: {response.json()}")

# Listar usuários
response = requests.get(
    f"{BASE_URL}/api/usuarios",
    headers={"X-API-Key": API_KEY}
)

print(f"Usuários: {response.json()}")
```

## ?? Node.js (Axios)

```javascript
const axios = require('axios');

const BASE_URL = 'https://localhost:5001';
const API_KEY = 'e59eb473-6a3b-4c6d-81cc-8cb929d83399';

const api = axios.create({
  baseURL: BASE_URL,
  headers: {
    'X-API-Key': API_KEY,
    'Content-Type': 'application/json'
  }
});

// Criar usuário
async function createUser() {
  try {
    const userData = {
      primeiroNome: "Carlos",
      sobrenome: "Oliveira",
      apelido: "carlosoliveira", 
      email: "carlos@email.com",
      telefone: "(11) 95555-5555",
      dataNascimento: "1985-03-25T00:00:00",
      hashSenha: "senhaHasheada654"
    };

    const response = await api.post('/api/usuarios', userData);
    console.log('Usuário criado:', response.data);
  } catch (error) {
    console.error('Erro:', error.response.data);
  }
}

// Listar usuários
async function listUsers() {
  try {
    const response = await api.get('/api/usuarios');
    console.log('Usuários:', response.data);
  } catch (error) {
    console.error('Erro:', error.response.data);
  }
}

createUser();
listUsers();
```

## ? Testes de Rate Limiting

Para testar o rate limiting (100 requisições/minuto):

```bash
# Bash script para testar rate limit
for i in {1..105}; do
  echo "Request $i:"
  curl -s -o /dev/null -w "%{http_code}\n" \
    -H "X-API-Key: e59eb473-6a3b-4c6d-81cc-8cb929d83399" \
    "https://localhost:5001/api/usuarios"
done
```

## ?? Verificar Headers de Rate Limiting

```bash
curl -I "https://localhost:5001/api/usuarios" \
  -H "X-API-Key: e59eb473-6a3b-4c6d-81cc-8cb929d83399"
```

Você verá headers como:
```
X-RateLimit-Limit: 100
X-RateLimit-Remaining: 99
X-RateLimit-Reset: 1640995200
```

## ? Testar Cenários de Erro

### 1. Sem API Key:
```bash
curl -X GET "https://localhost:5001/api/usuarios"
# Resultado: 401 Unauthorized
```

### 2. API Key inválida:
```bash
curl -X GET "https://localhost:5001/api/usuarios" \
  -H "X-API-Key: chave-invalida"
# Resultado: 401 Unauthorized  
```

### 3. Dados inválidos:
```bash
curl -X POST "https://localhost:5001/api/usuarios" \
  -H "Content-Type: application/json" \
  -H "X-API-Key: e59eb473-6a3b-4c6d-81cc-8cb929d83399" \
  -d '{"primeiroNome": ""}'
# Resultado: 400 Bad Request
```

## ?? Dicas de Teste

1. **Sempre use HTTPS** em produção
2. **Monitore os headers de rate limiting** para evitar bloqueios
3. **Use o Swagger UI** para testes interativos
4. **Implemente retry logic** para lidar com rate limiting
5. **Valide todos os campos obrigatórios** antes do envio
6. **Teste cenários de erro** para robustez da aplicação

## ?? Monitoramento

Acompanhe os logs da aplicação para:
- Tentativas de autenticação inválidas
- Rate limiting acionado
- Erros de validação
- Performance das requisições

---

**?? FIAP Cloud Games** - Documentação de Testes