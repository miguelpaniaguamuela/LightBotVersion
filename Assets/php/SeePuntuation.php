<?php
mb_internal_encoding("UTF-8");
$enlace =  mysqli_connect("localhost", "id2386751_faherian", "fontanar", "id2386751_guybotdata");

if( (mysqli_connect_errno())){
	printf("Conexión fallida: %s\n", mysql_connect_error());
	exit();
}

mysqli_set_charset($enlace, "utf8");

$consulta = "SELECT * FROM PlayerRanking ORDER by Puntuation DESC LIMIT 50";
$resultado = mysqli_query($enlace, $consulta);

$num = mysqli_num_rows($resultado);

for($i = 0;$i < $num; $i++)
{

	$row = mysqli_fetch_array($resultado, MYSQLI_ASSOC);
	echo $row['Name'] . ";" . $row['Puntuation']. ";" ;
}
?>