CREATE database if not EXISTS vista_chic;
use vista_chic;
CREATE TABLE IF NOT EXISTS cliente (
            cod_cliente INT AUTO_INCREMENT PRIMARY KEY,
            nome VARCHAR(45) NOT NULL,
            email VARCHAR(45),
            senha VARCHAR(200)
        );
        

INSERT INTO `cliente` VALUES (1,'adm','aaa@gmail.com',a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3);
CREATE TABLE IF NOT EXISTS produto (
            cod_produto INT AUTO_INCREMENT PRIMARY KEY,
            nome VARCHAR(100) NOT NULL,
            tamanho int not null,
            valor INT NOT NULL,
            idcliente int not null,
            FOREIGN KEY(idcliente) REFERENCES cliente (cod_cliente)
            );
            
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insereCliente`(nome varchar(50), email varchar(20), senha varchar(50))
BEGIN
INSERT INTO `cliente`
(`nome`,
`email`,
`senha`)
VALUES
(nome,
email,
senha
);
END ;;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_insereProduto`(nome varchar(50), tamanho int, valor int, idcliente int)
BEGIN
INSERT INTO `produto`
(`nome`,
`tamanho`,
`valor`,
`idcliente`)
VALUES
(nome,
tamanho,
valor,
idcliente
);
END ;;
DELIMITER ;

DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_listaCliente`()
BEGIN
     SELECT 
        c.cod_cliente, 
        c.nome, 
        c.email, 
        p.nome
    FROM cliente c
    INNER JOIN produto p 
        ON p.idcliente = c.cod_cliente;
END ;;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_listaProdutos`()
BEGIN
   select * from produto;
END;;

DELIMITER ;

DELIMITER ;;
create procedure `sp_removeCliente`(idcliente int)
Begin
delete from cliente where cod_cliente = idcliente;
end;;

create procedure `sp_alteraCliente`(cod_cliente int, nome varchar(45), email varchar(45), senha int)
Begin
update `cliente` set 
`nome`= nome,
`email`= email,
`senha`= senha
where `cod_cliente`= cod_cliente;
end;;

create procedure `sp_removeProduto`(idproduto int)
Begin
delete from produto where cod_produto = idproduto;
end;;

create procedure `sp_alteraProduto`(cod_produto int, nome varchar(45), tamanho varchar(45), valor int, idcliente int)
Begin
update `produto` set 
`nome`= nome,
`tamanho`= tamanho,
`valor`= valor,
`idcliente`= idcliente
where `cod_produto`= cod_produto;
end;;


create procedure `sp_consultaLogin`(usuario varchar(100), senha varchar(100))
begin
select *from cliente where cliente.nome = usuario and cliente.senha =senha;
end;;
DELIMITER ;