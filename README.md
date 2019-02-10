Programador: Fernando Rangel



Configuração:

	-DesafioPartnerGroupBD.sql: Script de criação do BD e suas tabelas, bem como a população das mesmas.
	-Conexão com BD SQL Server localizado no parâmetro "ConexaoSQL" dentro de Web.config.

Estrutura da API:

	-Models: Objetos e seus atributos
    *ResponseModel: Objeto genérico utilizado para os retornos da API

	-Services: Serviços de consulta ao BD.
    *ConexaoBD: Classe Default para conexão com o BD

	-Controllers: Classes que recebem a requisição e fazem o roteamento utilizando ApiController
	
Qualquer dúvida estou a disposição: ferangelfe@gmail.com
