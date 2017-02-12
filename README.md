# Kramer

## Criando Usuário Administrador
Para criar um usuário administrador, algumas etapas são necessárias:

- Crie um usuário normalmente no site;
- Execute o comando:
```exec AddRoleToUser 'email do usuário do sistema', 'ADMIN'```
- Pronto, este usuário será admin.

## Adicionando SaleType para um Usuário
Execute a seguinte procedure:

```exec AddRoleToUser 'email do usuário do sistema', 'Canal de Venda par qual terá acesso'```

## Adcionando um SaleType
Execute a seguinte procedure:

``` exec AddSaleType 'Nome do canal de vendas' ```