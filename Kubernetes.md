-COMANDOS RECRIAR TUDO NA AZURE

-----------RECRIAR BANCO E FILA 

1 - PRIMEIRO PASSO A SER EXECUTADO

# 1. Variáveis de Configuração
RG_NAME="RG-Fase5-dados-v3"
LOCATION="eastus2"
SQL_SERVER_NAME="sql-fiap-fase-5-001"
STORAGE_NAME="stfiapfase5001"

ADMIN_USER="usr_admin_fase_5"
ADMIN_PASS="<SQL_ADMIN_PASSWORD>"   # ❗ DEFINIR VIA VARIÁVEL SEGURA

echo "🚀 Iniciando a recriação do ambiente de DADOS..."

az group create --name $RG_NAME --location $LOCATION

az sql server create \
  --name $SQL_SERVER_NAME \
  --resource-group $RG_NAME \
  --location $LOCATION \
  --admin-user $ADMIN_USER \
  --admin-password $ADMIN_PASS

az sql server firewall-rule create \
  --resource-group $RG_NAME \
  --server $SQL_SERVER_NAME \
  --name AllowAzureServices \
  --start-ip-address 0.0.0.0 \
  --end-ip-address 0.0.0.0

az sql db create --resource-group $RG_NAME --server $SQL_SERVER_NAME --name pagamentos-db --service-objective Basic
az sql db create --resource-group $RG_NAME --server $SQL_SERVER_NAME --name usuarios-db --service-objective Basic
az sql db create --resource-group $RG_NAME --server $SQL_SERVER_NAME --name jogos-db --service-objective Basic
az sql db create --resource-group $RG_NAME --server $SQL_SERVER_NAME --name EventStore --service-objective Basic
az sql db create --resource-group $RG_NAME --server $SQL_SERVER_NAME --name ReadModelDb --service-objective Basic

az storage account create \
  --name $STORAGE_NAME \
  --resource-group $RG_NAME \
  --location $LOCATION \
  --sku Standard_LRS \
  --kind StorageV2

echo "⚠️ Capture a nova Connection String do Storage:"
echo "az storage account show-connection-string --name $STORAGE_NAME --resource-group $RG_NAME --output tsv"

-----------RECRIAR O CLUSTER

RG_NAME="RG-Fase5-Compute"
LOCATION="eastus"
CLUSTER_NAME="ClusterFiapCloudGames"
ACR_NAME="acrtechchallengefase5"

DEVOPS_SP_ID="<AZURE_DEVOPS_SP_ID>"   # ❗ NÃO VERSIONAR ID REAL

az group create --name $RG_NAME --location $LOCATION

az aks create \
  --resource-group $RG_NAME \
  --name $CLUSTER_NAME \
  --node-count 1 \
  --generate-ssh-keys \
  --node-vm-size Standard_B2s \
  --attach-acr $ACR_NAME \
  --location $LOCATION

SCOPE="/subscriptions/$(az account show --query id -o tsv)/resourceGroups/$RG_NAME"

az role assignment create \
  --assignee $DEVOPS_SP_ID \
  --role "Contributor" \
  --scope $SCOPE

-----------FUNCIONAMENTO DO CONFIGMAP / SECRETS

SERVER="tcp:sql-fiap-fase-5-001.database.windows.net,1433"
USER_ID="usr_admin_fase_5"
PASSWORD="<SQL_ADMIN_PASSWORD>"

DB_NAME_PAGAMENTO="pagamentos-db"
DB_NAME_USUARIO="usuarios-db"
DB_NAME_JOGO="jogos-db"
DB_NAME_EventStore="EventStore"
DB_NAME_ReadModel="ReadModelDb"

APP_INSIGHT="<APPLICATION_INSIGHTS_CONNECTION_STRING>"
STORAGE_CONN="<STORAGE_CONNECTION_STRING>"
JWT_SECRET="<JWT_SECRET_KEY>"

get_conn_string() {
  echo "Server=$SERVER;Initial Catalog=$1;User ID=$USER_ID;Password=$PASSWORD;Encrypt=True;"
}

kubectl create secret generic api-pagamento-secret \
  --from-literal=SqlConnectionString="$(get_conn_string $DB_NAME_PAGAMENTO)" \
  --from-literal=QueueConnectionString="$STORAGE_CONN" \
  --from-literal=AppInsights="$APP_INSIGHT"

kubectl create secret generic api-usuario-secret \
  --from-literal=SqlConnectionString="$(get_conn_string $DB_NAME_USUARIO)" \
  --from-literal=JwtKey="$JWT_SECRET"
