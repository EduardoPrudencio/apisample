# Usando uma imagem oficial do Node.js como base, apropriada para construir o app
FROM node:18-alpine AS build

# Define o diretório de trabalho dentro do container
WORKDIR /app

# Copia o package.json e o package-lock.json para o contêiner
COPY package*.json ./

# Instala as dependências do projeto
RUN npm install

# Copia todo o código do projeto para o contêiner
COPY . .

# Compila a aplicação para produção
RUN npm run build

# Serve a aplicação usando uma imagem mais leve do nginx
FROM nginx:alpine

# Remove o arquivo de configuração padrão do nginx
RUN rm -rf /usr/share/nginx/html/*

# Copia os arquivos de build do React para o diretório padrão do nginx
COPY --from=build /app/build /usr/share/nginx/html

# Expõe a porta que o NGINX vai usar
EXPOSE 80

# Comando para rodar o servidor nginx
CMD ["nginx", "-g", "daemon off;"]
