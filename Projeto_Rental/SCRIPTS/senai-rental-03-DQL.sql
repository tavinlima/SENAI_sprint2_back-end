-- DQL

USE T_Rental;
GO

SELECT * FROM EMPRESA
SELECT * FROM MARCA
SELECT * FROM CLIENTE
SELECT * FROM MODELO
SELECT * FROM VEICULO
SELECT * FROM ALUGUEL

SELECT dataEmpresetimo, dataDevolucao, nomeCliente, nomeModelo FROM ALUGUEL
INNER JOIN CLIENTE
ON ALUGUEL.idCliente = CLIENTE.idCliente
INNER JOIN MODELO
ON ALUGUEL.idVeiculo = MODELO.idModelo;

SELECT nomeCliente, dataEmpresetimo, dataDevolucao, nomeModelo FROM ALUGUEL
INNER JOIN CLIENTE
ON ALUGUEL.idCliente = Cliente.idCliente
INNER JOIN MODELO
ON ALUGUEL.idVeiculo = MODELO.idModelo
WHERE nomeCliente = 'EDSON'

SELECT idCliente, nomeCliente, sobrenomeCliente, CNH FROM CLIENTE; 
GO

SELECT idAluguel, idVeiculo, dataEmpresetimo, dataDevolucao, nomeCliente, nomeModelo, nomeMarca 
FROM ALUGUEL
INNER JOIN CLIENTE
ON ALUGUEL.idCliente = CLIENTE.idCliente
LEFT JOIN MODELO
ON ALUGUEL.idVeiculo = MODELO.idModelo
LEFT JOIN MARCA
ON MODELO.idMarca = MARCA.idMarca